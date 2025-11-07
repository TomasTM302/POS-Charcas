using System.Drawing;
using System.Windows.Forms;
using POSCharcas.Business.Models;

namespace POSCharcas.Presentation.Forms
{
    /// <summary>
    /// Collects product information from the user for create/update operations.
    /// </summary>
    public sealed class ProductDetailDialog : Form
    {
        private readonly TextBox _nameTextBox = new();
        private readonly NumericUpDown _priceUpDown = new();
        private readonly NumericUpDown _stockUpDown = new();
        private readonly TextBox _descriptionTextBox = new();

        public ProductDetailDialog(ProductModel? product = null)
        {
            Product = product?.Clone() ?? new ProductModel();
            InitializeComponent();
            BindData();
        }

        public ProductModel Product { get; }

        private void InitializeComponent()
        {
            Text = "Detalle de producto";
            Size = new Size(460, 420);
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            BackColor = System.Drawing.Color.FromArgb(245, 246, 250);

            var layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(20),
                ColumnCount = 2,
                RowCount = 5,
                AutoSize = true
            };
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70));

            _nameTextBox.Width = 240;
            _descriptionTextBox.Multiline = true;
            _descriptionTextBox.Height = 120;

            _priceUpDown.DecimalPlaces = 2;
            _priceUpDown.Maximum = 100000;
            _priceUpDown.Increment = 0.5M;

            _stockUpDown.Maximum = 100000;

            layout.Controls.Add(new Label { Text = "Nombre", AutoSize = true }, 0, 0);
            layout.Controls.Add(_nameTextBox, 1, 0);
            layout.Controls.Add(new Label { Text = "Precio", AutoSize = true }, 0, 1);
            layout.Controls.Add(_priceUpDown, 1, 1);
            layout.Controls.Add(new Label { Text = "Stock", AutoSize = true }, 0, 2);
            layout.Controls.Add(_stockUpDown, 1, 2);
            layout.Controls.Add(new Label { Text = "Descripción", AutoSize = true }, 0, 3);
            layout.Controls.Add(_descriptionTextBox, 1, 3);

            var saveButton = new Button
            {
                Text = "Guardar",
                Dock = DockStyle.Fill,
                Height = 36,
                FlatStyle = FlatStyle.Flat,
                BackColor = System.Drawing.Color.FromArgb(87, 153, 255),
                ForeColor = System.Drawing.Color.White
            };
            saveButton.FlatAppearance.BorderSize = 0;
            saveButton.Click += (_, _) => SaveAndClose();
            layout.Controls.Add(saveButton, 0, 4);
            layout.SetColumnSpan(saveButton, 2);

            Controls.Add(layout);
        }

        private void BindData()
        {
            _nameTextBox.Text = Product.Name;
            _priceUpDown.Value = Product.Price;
            _stockUpDown.Value = Product.Stock;
            _descriptionTextBox.Text = Product.Description;
        }

        private void SaveAndClose()
        {
            Product.Name = _nameTextBox.Text;
            Product.Price = _priceUpDown.Value;
            Product.Stock = (int)_stockUpDown.Value;
            Product.Description = _descriptionTextBox.Text;

            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
