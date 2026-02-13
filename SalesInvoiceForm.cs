using WinFormsApp1.Data;
using WinFormsApp1.Models;

namespace WinFormsApp1;

public partial class SalesInvoiceForm : Form
{
    private readonly Invoice? _existingInvoice;
    private readonly List<InvoiceLineViewModel> _lines = new();
    private readonly List<Customer> _customers = new();
    private readonly List<Product> _products = new();
    private bool _isEditMode => _existingInvoice != null;

    public SalesInvoiceForm(Invoice? invoice = null)
    {
        _existingInvoice = invoice;
        InitializeComponent();
    }

    private void SalesInvoiceForm_Load(object sender, EventArgs e)
    {
        _customers.AddRange(CustomerRepository.GetAll());
        _products.AddRange(ProductRepository.GetAll());

        comboCustomer.DisplayMember = "Name";
        comboCustomer.ValueMember = "Id";
        // Empty placeholder so user must explicitly choose a customer
        var customerList = new List<Customer>
        {
            new Customer { Id = 0, Name = "-- Select Customer --" }
        };
        customerList.AddRange(_customers);
        comboCustomer.DataSource = customerList;

        if (_isEditMode && _existingInvoice != null)
        {
            Text = "Edit Sales Invoice";
            txtInvoiceNumber.Text = _existingInvoice.InvoiceNumber;
            txtInvoiceNumber.ReadOnly = true;
            comboCustomer.SelectedValue = _existingInvoice.CustomerId;
            dateInvoiceDate.Value = _existingInvoice.InvoiceDate;
            numTaxPercent.Value = _existingInvoice.TaxPercent;
            txtNotes.Text = _existingInvoice.Notes ?? "";
            foreach (var line in _existingInvoice.Lines)
                _lines.Add(new InvoiceLineViewModel
                {
                    ProductId = line.ProductId,
                    ProductName = line.ProductName,
                    UnitPrice = line.UnitPrice,
                    Quantity = line.Quantity,
                    LineTotal = line.LineTotal
                });
        }
        else
        {
            txtInvoiceNumber.Text = InvoiceRepository.GetNextInvoiceNumber();
            txtInvoiceNumber.ReadOnly = true;
            dateInvoiceDate.Value = DateTime.Today;
            comboCustomer.SelectedValue = 0; // Keep "Select Customer" selected (empty)
        }

        BindGrid();
        RecalculateTotals();
    }

    private void BindGrid()
    {
        dgvLines.DataSource = null;
        dgvLines.DataSource = _lines.Select(x => new
        {
            Product = x.ProductName,
            x.Quantity,
            UnitPrice = x.UnitPrice,
            LineTotal = x.LineTotal
        }).ToList();
        dgvLines.Columns["Product"].HeaderText = "Product";
        dgvLines.Columns["Quantity"].HeaderText = "Qty";
        dgvLines.Columns["UnitPrice"].HeaderText = "Unit Price";
        dgvLines.Columns["LineTotal"].HeaderText = "Line Total";
        if (dgvLines.Columns["UnitPrice"].DefaultCellStyle.Format == null)
            dgvLines.Columns["UnitPrice"].DefaultCellStyle.Format = dgvLines.Columns["LineTotal"].DefaultCellStyle.Format = "N2";
    }

    private void RecalculateTotals()
    {
        var subTotal = _lines.Sum(x => x.LineTotal);
        var taxPct = numTaxPercent.Value;
        var taxAmt = subTotal * taxPct / 100;
        var total = subTotal + taxAmt;
        lblSubTotalValue.Text = subTotal.ToString("N2");
        lblTaxAmountValue.Text = taxAmt.ToString("N2");
        lblTotalValue.Text = total.ToString("N2");
    }

    private void btnAddLine_Click(object sender, EventArgs e)
    {
        using var dlg = new AddLineDialog(_products, _lines.Select(x => x.ProductId).ToList());
        if (dlg.ShowDialog() != DialogResult.OK || dlg.SelectedProduct == null) return;
        var p = dlg.SelectedProduct;
        var qty = dlg.Quantity;
        var lineTotal = p.UnitPrice * qty;
        _lines.Add(new InvoiceLineViewModel
        {
            ProductId = p.Id,
            ProductName = p.Name,
            UnitPrice = p.UnitPrice,
            Quantity = qty,
            LineTotal = lineTotal
        });
        BindGrid();
        RecalculateTotals();
    }

    private void btnRemoveLine_Click(object sender, EventArgs e)
    {
        var row = dgvLines.CurrentRow;
        if (row?.Index < 0) return;
        var idx = row!.Index;
        if (idx >= 0 && idx < _lines.Count)
        {
            _lines.RemoveAt(idx);
            BindGrid();
            RecalculateTotals();
        }
    }

    private void numTaxPercent_ValueChanged(object sender, EventArgs e) => RecalculateTotals();

    private void btnSave_Click(object sender, EventArgs e)
    {
        if (comboCustomer.SelectedValue == null || (int)comboCustomer.SelectedValue <= 0)
        {
            MessageBox.Show("You must choose a customer.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            comboCustomer.Focus();
            return;
        }
        if (_lines.Count == 0)
        {
            MessageBox.Show("Please add at least one item to the invoice.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        var subTotal = _lines.Sum(x => x.LineTotal);
        var taxPct = numTaxPercent.Value;
        var taxAmt = subTotal * taxPct / 100;
        var total = subTotal + taxAmt;

        var invoice = new Invoice
        {
            InvoiceNumber = txtInvoiceNumber.Text.Trim(),
            CustomerId = (int)comboCustomer.SelectedValue!,
            InvoiceDate = dateInvoiceDate.Value.Date,
            SubTotal = subTotal,
            TaxPercent = taxPct,
            TaxAmount = taxAmt,
            Total = total,
            Notes = string.IsNullOrWhiteSpace(txtNotes.Text) ? null : txtNotes.Text.Trim(),
            Lines = _lines.Select(x => new InvoiceLine
            {
                ProductId = x.ProductId,
                ProductName = x.ProductName,
                UnitPrice = x.UnitPrice,
                Quantity = x.Quantity,
                LineTotal = x.LineTotal
            }).ToList()
        };

        try
        {
            if (_isEditMode && _existingInvoice != null)
            {
                invoice.Id = _existingInvoice.Id;
                InvoiceRepository.Update(invoice);
                MessageBox.Show("Invoice updated successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                InvoiceRepository.Insert(invoice);
                MessageBox.Show("Invoice saved successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            DialogResult = DialogResult.OK;
            Close();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error saving invoice: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
        DialogResult = DialogResult.Cancel;
        Close();
    }

    private class InvoiceLineViewModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = "";
        public decimal UnitPrice { get; set; }
        public decimal Quantity { get; set; }
        public decimal LineTotal { get; set; }
    }

    private void comboCustomer_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    private void dgvLines_CellContentClick(object sender, DataGridViewCellEventArgs e)
    {

    }
}
