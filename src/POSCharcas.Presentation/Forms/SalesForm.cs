using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using POSCharcas.Business.Models;
using POSCharcas.Business.Services;

namespace POSCharcas.Presentation.Forms
{
    /// <summary>
    /// User interface that orchestrates a new sale workflow. It allows adding products to a cart,
    /// calculates totals and persists the transaction through the business layer.
    /// </summary>
    public partial class SalesForm : Form
    {
        private readonly SalesService _salesService;
        private readonly BindingSource _itemsBindingSource = new();
        private readonly List<SaleItemModel> _items = new();

        private DataGridView _itemsGrid = null!;
        private Label _totalLabel = null!;

        public SalesForm(SalesService salesService)
        {
            _salesService = salesService;
            InitializeComponent();
            RefreshBinding();
        }

        private void InitializeComponent()
        {
            Text = "Registrar Venta";
            Size = new Size(900, 600);
            StartPosition = FormStartPosition.CenterParent;
            BackColor = Color.FromArgb(250, 251, 255);

            _itemsGrid = new DataGridView
            {
                Dock = DockStyle.Top,
                Height = 360,
                AutoGenerateColumns = false,
                BackgroundColor = Color.White,
                AllowUserToAddRows = false,
                ReadOnly = true
            };

            _itemsGrid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Producto", DataPropertyName = nameof(SaleItemModel.ProductName), Width = 200 });
            _itemsGrid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Cantidad", DataPropertyName = nameof(SaleItemModel.Quantity), Width = 80 });
            _itemsGrid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Precio", DataPropertyName = nameof(SaleItemModel.Price), Width = 80 });
            _itemsGrid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Subtotal", DataPropertyName = nameof(SaleItemModel.Subtotal), Width = 80 });

            _itemsGrid.DataSource = _itemsBindingSource;

            var addButton = CreateActionButton("Agregar producto", (_, _) => AddProduct());
            var removeButton = CreateActionButton("Quitar seleccionado", (_, _) => RemoveSelected());
            var confirmButton = CreateActionButton("Confirmar venta", async (_, _) => await ConfirmSaleAsync());

            _totalLabel = new Label
            {
                Text = "Total: $0.00",
                Font = new Font("Segoe UI", 16F, FontStyle.Bold, GraphicsUnit.Point),
                AutoSize = true,
                Dock = DockStyle.Bottom,
                Padding = new Padding(20)
            };

            var buttonsPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Bottom,
                Height = 80,
                FlowDirection = FlowDirection.LeftToRight,
                Padding = new Padding(20),
                BackColor = Color.FromArgb(240, 243, 250)
            };

            buttonsPanel.Controls.Add(addButton);
            buttonsPanel.Controls.Add(removeButton);
            buttonsPanel.Controls.Add(confirmButton);

            Controls.Add(_itemsGrid);
            Controls.Add(buttonsPanel);
            Controls.Add(_totalLabel);
        }

        private Button CreateActionButton(string text, EventHandler handler)
        {
            var button = new Button
            {
                Text = text,
                Width = 180,
                Height = 40,
                Margin = new Padding(10),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(87, 153, 255),
                ForeColor = Color.White
            };

            button.FlatAppearance.BorderSize = 0;
            button.Click += handler;
            return button;
        }

        private void AddProduct()
        {
            using var dialog = new ProductPickerDialog(_salesService);
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                _items.Add(dialog.SelectedItem);
                RefreshBinding();
            }
        }

        private void RemoveSelected()
        {
            if (_itemsGrid.CurrentRow?.DataBoundItem is SaleItemModel item)
            {
                _items.Remove(item);
                RefreshBinding();
            }
        }

        private async System.Threading.Tasks.Task ConfirmSaleAsync()
        {
            try
            {
                if (!_items.Any())
                {
                    MessageBox.Show(this, "Agrega al menos un producto antes de registrar la venta.", "Venta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var sale = await _salesService.RegisterSaleAsync(_items.ToList());
                MessageBox.Show(this, $"Venta registrada con folio #{sale.Id}.", "Venta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _items.Clear();
                RefreshBinding();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, $"Error al registrar la venta: {ex.Message}", "Venta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RefreshBinding()
        {
            _itemsBindingSource.DataSource = null;
            _itemsBindingSource.DataSource = _items.Select(i => i.Clone()).ToList();
            UpdateTotal();
        }

        private void UpdateTotal()
        {
            var total = _items.Sum(i => i.Subtotal);
            _totalLabel.Text = $"Total: {total:C2}";
        }
    }
}
