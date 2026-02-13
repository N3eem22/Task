using WinFormsApp1.Models;

namespace WinFormsApp1;

public partial class AddLineDialog : Form
{
    private readonly List<Product> _products;
    private readonly List<int> _alreadyAddedIds;

    public Product? SelectedProduct { get; private set; }
    public decimal Quantity => numQuantity.Value;

    public AddLineDialog(List<Product> products, List<int> alreadyAddedIds)
    {
        _products = products;
        _alreadyAddedIds = alreadyAddedIds;
        InitializeComponent();
    }

    private void AddLineDialog_Load(object sender, EventArgs e)
    {
        var available = _products.Where(p => !_alreadyAddedIds.Contains(p.Id)).ToList();
        comboProduct.DisplayMember = "Name";
        comboProduct.ValueMember = "Id";
        comboProduct.DataSource = available;
        numQuantity.Minimum = 0.01m;
        numQuantity.Value = 1;
        if (available.Count == 0)
        {
            MessageBox.Show("All products are already on this invoice.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            DialogResult = DialogResult.Cancel;
            Close();
            return;
        }
        UpdateUnitPrice();
    }

    private void comboProduct_SelectedValueChanged(object sender, EventArgs e)
    {
        UpdateUnitPrice();
    }

    private void UpdateUnitPrice()
    {
        if (comboProduct.SelectedItem is Product p)
        {
            lblUnitPrice.Text = p.UnitPrice.ToString("N2");
        }
    }

    private void btnOK_Click(object sender, EventArgs e)
    {
        if (comboProduct.SelectedItem is not Product p)
        {
            MessageBox.Show("Please select a product.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }
        if (numQuantity.Value <= 0)
        {
            MessageBox.Show("Quantity must be greater than 0.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }
        SelectedProduct = p;
        DialogResult = DialogResult.OK;
        Close();
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
        DialogResult = DialogResult.Cancel;
        Close();
    }
}
