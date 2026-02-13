using WinFormsApp1.Data;
using WinFormsApp1.Models;

namespace WinFormsApp1;

public partial class Form1 : Form
{
    private List<Invoice> _invoices = new();

    public Form1()
    {
        InitializeComponent();
    }

    private void Form1_Load(object sender, EventArgs e)
    {
        AppDbContext.EnsureCreated();
        RefreshInvoiceList();
    }

    private void RefreshInvoiceList()
    {
        _invoices = InvoiceRepository.GetAll();
        dgvInvoices.DataSource = _invoices.Select(x => new
        {
            x.InvoiceNumber,
            x.CustomerName,
            Date = x.InvoiceDate.ToString("d"),
            SubTotal = x.SubTotal.ToString("N2"),
            Tax = x.TaxAmount.ToString("N2"),
            Total = x.Total.ToString("N2")
        }).ToList();
        dgvInvoices.Columns["InvoiceNumber"].HeaderText = "Invoice #";
        dgvInvoices.Columns["CustomerName"].HeaderText = "Customer";
        dgvInvoices.Columns["Date"].HeaderText = "Date";
        dgvInvoices.Columns["SubTotal"].HeaderText = "Subtotal";
        dgvInvoices.Columns["Tax"].HeaderText = "Tax";
        dgvInvoices.Columns["Total"].HeaderText = "Total";
    }

    private Invoice? GetSelectedInvoice()
    {
        var row = dgvInvoices.CurrentRow;
        if (row?.Index < 0) return null;
        var idx = row!.Index;
        if (idx >= 0 && idx < _invoices.Count)
            return _invoices[idx];
        return null;
    }

    private void btnNew_Click(object sender, EventArgs e)
    {
        using var f = new SalesInvoiceForm();
        if (f.ShowDialog() == DialogResult.OK)
            RefreshInvoiceList();
    }

    private void btnEdit_Click(object sender, EventArgs e)
    {
        var inv = GetSelectedInvoice();
        if (inv == null)
        {
            MessageBox.Show("Please select an invoice to edit.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }
        var full = InvoiceRepository.GetById(inv.Id);
        if (full == null) return;
        using var f = new SalesInvoiceForm(full);
        if (f.ShowDialog() == DialogResult.OK)
            RefreshInvoiceList();
    }

    private void btnDelete_Click(object sender, EventArgs e)
    {
        var inv = GetSelectedInvoice();
        if (inv == null)
        {
            MessageBox.Show("Please select an invoice to delete.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }
        if (MessageBox.Show($"Delete invoice {inv.InvoiceNumber}?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            return;
        try
        {
            InvoiceRepository.Delete(inv.Id);
            RefreshInvoiceList();
            MessageBox.Show("Invoice deleted.", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
