namespace WinFormsApp1
{
    partial class SalesInvoiceForm
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
            lblInvoiceNumber = new Label();
            txtInvoiceNumber = new TextBox();
            lblCustomer = new Label();
            comboCustomer = new ComboBox();
            lblDate = new Label();
            dateInvoiceDate = new DateTimePicker();
            lblNotes = new Label();
            txtNotes = new TextBox();
            grpLines = new GroupBox();
            btnRemoveLine = new Button();
            btnAddLine = new Button();
            dgvLines = new DataGridView();
            grpTotals = new GroupBox();
            lblTotalValue = new Label();
            lblTotal = new Label();
            lblTaxAmountValue = new Label();
            lblTaxAmount = new Label();
            lblSubTotalValue = new Label();
            lblSubTotal = new Label();
            numTaxPercent = new NumericUpDown();
            lblTaxPercent = new Label();
            btnSave = new Button();
            btnCancel = new Button();
            grpLines.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvLines).BeginInit();
            grpTotals.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numTaxPercent).BeginInit();
            SuspendLayout();
            // 
            // lblInvoiceNumber
            // 
            lblInvoiceNumber.AutoSize = true;
            lblInvoiceNumber.Location = new Point(14, 20);
            lblInvoiceNumber.Name = "lblInvoiceNumber";
            lblInvoiceNumber.Size = new Size(114, 20);
            lblInvoiceNumber.TabIndex = 0;
            lblInvoiceNumber.Text = "Invoice Number";
            // 
            // txtInvoiceNumber
            // 
            txtInvoiceNumber.Location = new Point(137, 16);
            txtInvoiceNumber.Margin = new Padding(3, 4, 3, 4);
            txtInvoiceNumber.Name = "txtInvoiceNumber";
            txtInvoiceNumber.Size = new Size(137, 27);
            txtInvoiceNumber.TabIndex = 1;
            // 
            // lblCustomer
            // 
            lblCustomer.AutoSize = true;
            lblCustomer.Location = new Point(14, 64);
            lblCustomer.Name = "lblCustomer";
            lblCustomer.Size = new Size(72, 20);
            lblCustomer.TabIndex = 2;
            lblCustomer.Text = "Customer";
            // 
            // comboCustomer
            // 
            comboCustomer.DropDownStyle = ComboBoxStyle.DropDownList;
            comboCustomer.FormattingEnabled = true;
            comboCustomer.ImeMode = ImeMode.Disable;
            comboCustomer.Location = new Point(137, 60);
            comboCustomer.Margin = new Padding(3, 4, 3, 4);
            comboCustomer.Name = "comboCustomer";
            comboCustomer.Size = new Size(365, 28);
            comboCustomer.TabIndex = 3;
            comboCustomer.SelectedIndexChanged += comboCustomer_SelectedIndexChanged;
            // 
            // lblDate
            // 
            lblDate.AutoSize = true;
            lblDate.Location = new Point(14, 107);
            lblDate.Name = "lblDate";
            lblDate.Size = new Size(41, 20);
            lblDate.TabIndex = 4;
            lblDate.Text = "Date";
            // 
            // dateInvoiceDate
            // 
            dateInvoiceDate.Format = DateTimePickerFormat.Short;
            dateInvoiceDate.Location = new Point(137, 103);
            dateInvoiceDate.Margin = new Padding(3, 4, 3, 4);
            dateInvoiceDate.Name = "dateInvoiceDate";
            dateInvoiceDate.Size = new Size(137, 27);
            dateInvoiceDate.TabIndex = 5;
            // 
            // lblNotes
            // 
            lblNotes.AutoSize = true;
            lblNotes.Location = new Point(14, 149);
            lblNotes.Name = "lblNotes";
            lblNotes.Size = new Size(48, 20);
            lblNotes.TabIndex = 6;
            lblNotes.Text = "Notes";
            // 
            // txtNotes
            // 
            txtNotes.Location = new Point(137, 145);
            txtNotes.Margin = new Padding(3, 4, 3, 4);
            txtNotes.Multiline = true;
            txtNotes.Name = "txtNotes";
            txtNotes.ScrollBars = ScrollBars.Vertical;
            txtNotes.Size = new Size(365, 65);
            txtNotes.TabIndex = 7;
            // 
            // grpLines
            // 
            grpLines.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            grpLines.Controls.Add(btnRemoveLine);
            grpLines.Controls.Add(btnAddLine);
            grpLines.Controls.Add(dgvLines);
            grpLines.Location = new Point(14, 220);
            grpLines.Margin = new Padding(3, 4, 3, 4);
            grpLines.Name = "grpLines";
            grpLines.Padding = new Padding(3, 4, 3, 4);
            grpLines.Size = new Size(754, 293);
            grpLines.TabIndex = 8;
            grpLines.TabStop = false;
            grpLines.Text = "Line Items";
            // 
            // btnRemoveLine
            // 
            btnRemoveLine.Location = new Point(107, 29);
            btnRemoveLine.Margin = new Padding(3, 4, 3, 4);
            btnRemoveLine.Name = "btnRemoveLine";
            btnRemoveLine.Size = new Size(86, 33);
            btnRemoveLine.TabIndex = 2;
            btnRemoveLine.Text = "Remove";
            btnRemoveLine.UseVisualStyleBackColor = true;
            btnRemoveLine.Click += btnRemoveLine_Click;
            // 
            // btnAddLine
            // 
            btnAddLine.Location = new Point(15, 29);
            btnAddLine.Margin = new Padding(3, 4, 3, 4);
            btnAddLine.Name = "btnAddLine";
            btnAddLine.Size = new Size(86, 33);
            btnAddLine.TabIndex = 1;
            btnAddLine.Text = "Add Item";
            btnAddLine.UseVisualStyleBackColor = true;
            btnAddLine.Click += btnAddLine_Click;
            // 
            // dgvLines
            // 
            dgvLines.AllowUserToAddRows = false;
            dgvLines.AllowUserToDeleteRows = false;
            dgvLines.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvLines.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvLines.Location = new Point(15, 71);
            dgvLines.Margin = new Padding(3, 4, 3, 4);
            dgvLines.MultiSelect = false;
            dgvLines.Name = "dgvLines";
            dgvLines.ReadOnly = true;
            dgvLines.RowHeadersWidth = 51;
            dgvLines.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvLines.Size = new Size(725, 207);
            dgvLines.TabIndex = 0;
            dgvLines.CellContentClick += dgvLines_CellContentClick;
            // 
            // grpTotals
            // 
            grpTotals.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            grpTotals.Controls.Add(lblTotalValue);
            grpTotals.Controls.Add(lblTotal);
            grpTotals.Controls.Add(lblTaxAmountValue);
            grpTotals.Controls.Add(lblTaxAmount);
            grpTotals.Controls.Add(lblSubTotalValue);
            grpTotals.Controls.Add(lblSubTotal);
            grpTotals.Controls.Add(numTaxPercent);
            grpTotals.Controls.Add(lblTaxPercent);
            grpTotals.Location = new Point(514, 16);
            grpTotals.Margin = new Padding(3, 4, 3, 4);
            grpTotals.Name = "grpTotals";
            grpTotals.Padding = new Padding(3, 4, 3, 4);
            grpTotals.Size = new Size(254, 196);
            grpTotals.TabIndex = 9;
            grpTotals.TabStop = false;
            grpTotals.Text = "Totals";
            // 
            // lblTotalValue
            // 
            lblTotalValue.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblTotalValue.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblTotalValue.Location = new Point(114, 140);
            lblTotalValue.Name = "lblTotalValue";
            lblTotalValue.Size = new Size(126, 27);
            lblTotalValue.TabIndex = 7;
            lblTotalValue.Text = "0.00";
            lblTotalValue.TextAlign = ContentAlignment.TopRight;
            // 
            // lblTotal
            // 
            lblTotal.AutoSize = true;
            lblTotal.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblTotal.Location = new Point(17, 140);
            lblTotal.Name = "lblTotal";
            lblTotal.Size = new Size(44, 20);
            lblTotal.TabIndex = 6;
            lblTotal.Text = "Total";
            // 
            // lblTaxAmountValue
            // 
            lblTaxAmountValue.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblTaxAmountValue.Location = new Point(114, 104);
            lblTaxAmountValue.Name = "lblTaxAmountValue";
            lblTaxAmountValue.Size = new Size(126, 27);
            lblTaxAmountValue.TabIndex = 5;
            lblTaxAmountValue.Text = "0.00";
            lblTaxAmountValue.TextAlign = ContentAlignment.TopRight;
            // 
            // lblTaxAmount
            // 
            lblTaxAmount.AutoSize = true;
            lblTaxAmount.Location = new Point(17, 104);
            lblTaxAmount.Name = "lblTaxAmount";
            lblTaxAmount.Size = new Size(87, 20);
            lblTaxAmount.TabIndex = 4;
            lblTaxAmount.Text = "Tax Amount";
            // 
            // lblSubTotalValue
            // 
            lblSubTotalValue.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblSubTotalValue.Location = new Point(114, 68);
            lblSubTotalValue.Name = "lblSubTotalValue";
            lblSubTotalValue.Size = new Size(126, 27);
            lblSubTotalValue.TabIndex = 3;
            lblSubTotalValue.Text = "0.00";
            lblSubTotalValue.TextAlign = ContentAlignment.TopRight;
            // 
            // lblSubTotal
            // 
            lblSubTotal.AutoSize = true;
            lblSubTotal.Location = new Point(17, 68);
            lblSubTotal.Name = "lblSubTotal";
            lblSubTotal.Size = new Size(65, 20);
            lblSubTotal.TabIndex = 2;
            lblSubTotal.Text = "Subtotal";
            // 
            // numTaxPercent
            // 
            numTaxPercent.DecimalPlaces = 2;
            numTaxPercent.Location = new Point(114, 29);
            numTaxPercent.Margin = new Padding(3, 4, 3, 4);
            numTaxPercent.Name = "numTaxPercent";
            numTaxPercent.Size = new Size(80, 27);
            numTaxPercent.TabIndex = 1;
            numTaxPercent.ValueChanged += numTaxPercent_ValueChanged;
            // 
            // lblTaxPercent
            // 
            lblTaxPercent.AutoSize = true;
            lblTaxPercent.Location = new Point(17, 32);
            lblTaxPercent.Name = "lblTaxPercent";
            lblTaxPercent.Size = new Size(56, 20);
            lblTaxPercent.TabIndex = 0;
            lblTaxPercent.Text = "Tax (%)";
            // 
            // btnSave
            // 
            btnSave.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnSave.Location = new Point(590, 531);
            btnSave.Margin = new Padding(3, 4, 3, 4);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(86, 37);
            btnSave.TabIndex = 10;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // btnCancel
            // 
            btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnCancel.Location = new Point(682, 531);
            btnCancel.Margin = new Padding(3, 4, 3, 4);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(86, 37);
            btnCancel.TabIndex = 11;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // SalesInvoiceForm
            // 
            AllowDrop = true;
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(782, 584);
            Controls.Add(btnCancel);
            Controls.Add(btnSave);
            Controls.Add(grpTotals);
            Controls.Add(grpLines);
            Controls.Add(txtNotes);
            Controls.Add(lblNotes);
            Controls.Add(dateInvoiceDate);
            Controls.Add(lblDate);
            Controls.Add(comboCustomer);
            Controls.Add(lblCustomer);
            Controls.Add(txtInvoiceNumber);
            Controls.Add(lblInvoiceNumber);
            Margin = new Padding(3, 4, 3, 4);
            MinimumSize = new Size(797, 584);
            Name = "SalesInvoiceForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "New Sales Invoice";
            Load += SalesInvoiceForm_Load;
            grpLines.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvLines).EndInit();
            grpTotals.ResumeLayout(false);
            grpTotals.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numTaxPercent).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblInvoiceNumber;
        private TextBox txtInvoiceNumber;
        private Label lblCustomer;
        private ComboBox comboCustomer;
        private Label lblDate;
        private DateTimePicker dateInvoiceDate;
        private Label lblNotes;
        private TextBox txtNotes;
        private GroupBox grpLines;
        private DataGridView dgvLines;
        private Button btnAddLine;
        private Button btnRemoveLine;
        private GroupBox grpTotals;
        private Label lblTaxPercent;
        private NumericUpDown numTaxPercent;
        private Label lblSubTotal;
        private Label lblSubTotalValue;
        private Label lblTaxAmount;
        private Label lblTaxAmountValue;
        private Label lblTotal;
        private Label lblTotalValue;
        private Button btnSave;
        private Button btnCancel;
    }
}
