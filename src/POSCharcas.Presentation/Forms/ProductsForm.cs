using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using POSCharcas.Business.Models;
using POSCharcas.Business.Services;

namespace POSCharcas.Presentation.Forms
{
    /// <summary>
    /// Provides a CRUD interface for managing products in the catalog.
    /// </summary>
    public sealed class ProductsForm : Form
    {
        private readonly ProductService _productService;
        private readonly BindingSource _bindingSource = new();
        private DataGridView _grid = null!;

        public ProductsForm(ProductService productService)
        {
            _productService = productService;
            InitializeComponent();
            _ = LoadDataAsync();
        }

        private void InitializeComponent()
        {
            Text = "Productos";
            Size = new Size(800, 500);
            StartPosition = FormStartPosition.CenterParent;
            BackColor = Color.FromArgb(250, 251, 255);

            _grid = new DataGridView
            {
                Dock = DockStyle.Fill,
                AutoGenerateColumns = false,
                BackgroundColor = Color.White,
                AllowUserToAddRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect
            };

            _grid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Nombre", DataPropertyName = nameof(ProductModel.Name), Width = 200 });
            _grid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Precio", DataPropertyName = nameof(ProductModel.Price), Width = 100 });
            _grid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Stock", DataPropertyName = nameof(ProductModel.Stock), Width = 80 });
            _grid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Descripción", DataPropertyName = nameof(ProductModel.Description), AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            _grid.DataSource = _bindingSource;

            var toolbar = new FlowLayoutPanel
            {
                Dock = DockStyle.Top,
                Height = 60,
                FlowDirection = FlowDirection.LeftToRight,
                Padding = new Padding(20),
                BackColor = Color.FromArgb(240, 243, 250)
            };

            toolbar.Controls.Add(CreateButton("Nuevo", async (_, _) => await CreateAsync()));
            toolbar.Controls.Add(CreateButton("Editar", async (_, _) => await EditAsync()));
            toolbar.Controls.Add(CreateButton("Eliminar", async (_, _) => await DeleteAsync()));

            Controls.Add(_grid);
            Controls.Add(toolbar);
        }

        private Button CreateButton(string text, EventHandler handler)
        {
            var button = new Button
            {
                Text = text,
                Width = 120,
                Height = 36,
                Margin = new Padding(10),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(87, 153, 255),
                ForeColor = Color.White
            };

            button.FlatAppearance.BorderSize = 0;
            button.Click += handler;
            return button;
        }

        private async Task LoadDataAsync()
        {
            try
            {
                var products = await _productService.GetAllAsync();
                _bindingSource.DataSource = products;
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, $"No fue posible cargar los productos: {ex.Message}", "Productos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task CreateAsync()
        {
            using var dialog = new ProductDetailDialog();
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                try
                {
                    await _productService.CreateAsync(dialog.Product);
                    await LoadDataAsync();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, $"No fue posible crear el producto: {ex.Message}", "Productos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private async Task EditAsync()
        {
            if (_bindingSource.Current is not ProductModel selected)
            {
                MessageBox.Show(this, "Selecciona un producto para editar.", "Productos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using var dialog = new ProductDetailDialog(selected);
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                try
                {
                    await _productService.UpdateAsync(dialog.Product);
                    await LoadDataAsync();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, $"No fue posible actualizar el producto: {ex.Message}", "Productos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private async Task DeleteAsync()
        {
            if (_bindingSource.Current is not ProductModel selected)
            {
                MessageBox.Show(this, "Selecciona un producto para eliminar.", "Productos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show(this, $"¿Deseas eliminar {selected.Name}?", "Productos", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    await _productService.DeleteAsync(selected.Id);
                    await LoadDataAsync();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, $"No fue posible eliminar el producto: {ex.Message}", "Productos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
