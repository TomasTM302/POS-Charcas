using System;
using System.Drawing;
using System.Windows.Forms;
using POSCharcas.Business.Models;
using POSCharcas.Business.Services;

namespace POSCharcas.Presentation.Forms
{
    /// <summary>
    /// Dialog window that displays available products so the cashier can select one and set the
    /// quantity to add to the sale cart.
    /// </summary>
    public sealed class ProductPickerDialog : Form
    {
        private readonly SalesService _salesService;
        private ComboBox _productComboBox = null!;
        private NumericUpDown _quantityUpDown = null!;
        private NumericUpDown _priceUpDown = null!;

        public ProductPickerDialog(SalesService salesService)
        {
            _salesService = salesService;
            InitializeComponent();
        }

        public SaleItemModel SelectedItem { get; private set; } = null!;

        private void InitializeComponent()
        {
            Text = "Agregar producto";
            Size = new Size(420, 260);
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            BackColor = Color.FromArgb(245, 246, 250);

            _productComboBox = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Width = 320,
                Location = new Point(40, 40)
            };
            _productComboBox.DataSource = _salesService.GetAvailableProducts();
            _productComboBox.DisplayMember = nameof(ProductModel.Name);
            _productComboBox.ValueMember = nameof(ProductModel.Id);

            _quantityUpDown = new NumericUpDown
            {
                Location = new Point(40, 100),
                Width = 150,
                Minimum = 1,
                Maximum = 1000,
                Value = 1
            };

            _priceUpDown = new NumericUpDown
            {
                Location = new Point(210, 100),
                Width = 150,
                Minimum = 0,
                Maximum = 100000,
                DecimalPlaces = 2,
                Increment = 0.5M
            };
            _priceUpDown.DataBindings.Add("Value", _productComboBox.DataSource, nameof(ProductModel.Price));

            var acceptButton = new Button
            {
                Text = "Agregar",
                Location = new Point(40, 160),
                Width = 320,
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(87, 153, 255),
                ForeColor = Color.White
            };
            acceptButton.FlatAppearance.BorderSize = 0;
            acceptButton.Click += (_, _) => AcceptSelection();

            Controls.Add(_productComboBox);
            Controls.Add(_quantityUpDown);
            Controls.Add(_priceUpDown);
            Controls.Add(acceptButton);
        }

        private void AcceptSelection()
        {
            if (_productComboBox.SelectedItem is not ProductModel product)
            {
                MessageBox.Show(this, "Selecciona un producto válido.", "Productos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SelectedItem = new SaleItemModel
            {
                ProductId = product.Id,
                ProductName = product.Name,
                Quantity = (int)_quantityUpDown.Value,
                Price = _priceUpDown.Value
            };

            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
