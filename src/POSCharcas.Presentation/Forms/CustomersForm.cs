using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using POSCharcas.Business.Models;
using POSCharcas.Business.Services;

namespace POSCharcas.Presentation.Forms
{
    /// <summary>
    /// CRUD interface for managing customer records.
    /// </summary>
    public sealed class CustomersForm : Form
    {
        private readonly CustomerService _customerService;
        private readonly BindingSource _bindingSource = new();
        private DataGridView _grid = null!;

        public CustomersForm(CustomerService customerService)
        {
            _customerService = customerService;
            InitializeComponent();
            _ = LoadDataAsync();
        }

        private void InitializeComponent()
        {
            Text = "Clientes";
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

            _grid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Nombre", DataPropertyName = nameof(CustomerModel.FullName), Width = 200 });
            _grid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Email", DataPropertyName = nameof(CustomerModel.Email), Width = 200 });
            _grid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Teléfono", DataPropertyName = nameof(CustomerModel.PhoneNumber), Width = 120 });
            _grid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Dirección", DataPropertyName = nameof(CustomerModel.Address), AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
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
                var customers = await _customerService.GetAllAsync();
                _bindingSource.DataSource = customers;
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, $"No fue posible cargar los clientes: {ex.Message}", "Clientes", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task CreateAsync()
        {
            using var dialog = new CustomerDetailDialog();
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                try
                {
                    await _customerService.CreateAsync(dialog.Customer);
                    await LoadDataAsync();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, $"No fue posible crear el cliente: {ex.Message}", "Clientes", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private async Task EditAsync()
        {
            if (_bindingSource.Current is not CustomerModel selected)
            {
                MessageBox.Show(this, "Selecciona un cliente para editar.", "Clientes", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using var dialog = new CustomerDetailDialog(selected);
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                try
                {
                    await _customerService.UpdateAsync(dialog.Customer);
                    await LoadDataAsync();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, $"No fue posible actualizar el cliente: {ex.Message}", "Clientes", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private async Task DeleteAsync()
        {
            if (_bindingSource.Current is not CustomerModel selected)
            {
                MessageBox.Show(this, "Selecciona un cliente para eliminar.", "Clientes", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show(this, $"¿Deseas eliminar a {selected.FullName}?", "Clientes", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    await _customerService.DeleteAsync(selected.Id);
                    await LoadDataAsync();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, $"No fue posible eliminar el cliente: {ex.Message}", "Clientes", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
