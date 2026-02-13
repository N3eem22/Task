namespace WinFormsApp1
{
    partial class AddLineDialog
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            lblProduct = new Label();
            comboProduct = new ComboBox();
            lblQuantity = new Label();
            numQuantity = new NumericUpDown();
            lblUnitPriceLabel = new Label();
            lblUnitPrice = new Label();
            btnOK = new Button();
            btnCancel = new Button();
            ((System.ComponentModel.ISupportInitialize)numQuantity).BeginInit();
            SuspendLayout();
            //
            // lblProduct
            //
            lblProduct.AutoSize = true;
            lblProduct.Location = new Point(12, 18);
            lblProduct.Name = "lblProduct";
            lblProduct.Size = new Size(49, 15);
            lblProduct.TabIndex = 0;
            lblProduct.Text = "Product";
            //
            // comboProduct
            //
            comboProduct.DropDownStyle = ComboBoxStyle.DropDownList;
            comboProduct.FormattingEnabled = true;
            comboProduct.Location = new Point(90, 15);
            comboProduct.Name = "comboProduct";
            comboProduct.Size = new Size(250, 23);
            comboProduct.TabIndex = 1;
            comboProduct.SelectedValueChanged += comboProduct_SelectedValueChanged;
            //
            // lblQuantity
            //
            lblQuantity.AutoSize = true;
            lblQuantity.Location = new Point(12, 52);
            lblQuantity.Name = "lblQuantity";
            lblQuantity.Size = new Size(53, 15);
            lblQuantity.TabIndex = 2;
            lblQuantity.Text = "Quantity";
            //
            // numQuantity
            //
            numQuantity.DecimalPlaces = 2;
            numQuantity.Location = new Point(90, 49);
            numQuantity.Maximum = new decimal(new int[] { 999999, 0, 0, 0 });
            numQuantity.Name = "numQuantity";
            numQuantity.Size = new Size(100, 23);
            numQuantity.TabIndex = 3;
            //
            // lblUnitPriceLabel
            //
            lblUnitPriceLabel.AutoSize = true;
            lblUnitPriceLabel.Location = new Point(12, 86);
            lblUnitPriceLabel.Name = "lblUnitPriceLabel";
            lblUnitPriceLabel.Size = new Size(58, 15);
            lblUnitPriceLabel.TabIndex = 4;
            lblUnitPriceLabel.Text = "Unit Price";
            //
            // lblUnitPrice
            //
            lblUnitPrice.AutoSize = true;
            lblUnitPrice.Location = new Point(90, 86);
            lblUnitPrice.Name = "lblUnitPrice";
            lblUnitPrice.Size = new Size(28, 15);
            lblUnitPrice.TabIndex = 5;
            lblUnitPrice.Text = "0.00";
            //
            // btnOK
            //
            btnOK.Location = new Point(184, 125);
            btnOK.Name = "btnOK";
            btnOK.Size = new Size(75, 28);
            btnOK.TabIndex = 6;
            btnOK.Text = "OK";
            btnOK.UseVisualStyleBackColor = true;
            btnOK.Click += btnOK_Click;
            //
            // btnCancel
            //
            btnCancel.Location = new Point(265, 125);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(75, 28);
            btnCancel.TabIndex = 7;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            //
            // AddLineDialog
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(354, 165);
            Controls.Add(btnCancel);
            Controls.Add(btnOK);
            Controls.Add(lblUnitPrice);
            Controls.Add(lblUnitPriceLabel);
            Controls.Add(numQuantity);
            Controls.Add(lblQuantity);
            Controls.Add(comboProduct);
            Controls.Add(lblProduct);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "AddLineDialog";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Add Line Item";
            Load += AddLineDialog_Load;
            ((System.ComponentModel.ISupportInitialize)numQuantity).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblProduct;
        private ComboBox comboProduct;
        private Label lblQuantity;
        private NumericUpDown numQuantity;
        private Label lblUnitPriceLabel;
        private Label lblUnitPrice;
        private Button btnOK;
        private Button btnCancel;
    }
}
