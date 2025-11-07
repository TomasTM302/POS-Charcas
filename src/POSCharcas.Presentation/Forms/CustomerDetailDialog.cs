using System.Drawing;
using System.Windows.Forms;
using POSCharcas.Business.Models;

namespace POSCharcas.Presentation.Forms
{
    /// <summary>
    /// Collects customer data for the CRUD operations.
    /// </summary>
    public sealed class CustomerDetailDialog : Form
    {
        private readonly TextBox _nameTextBox = new();
        private readonly TextBox _emailTextBox = new();
        private readonly TextBox _phoneTextBox = new();
        private readonly TextBox _addressTextBox = new();

        public CustomerDetailDialog(CustomerModel? customer = null)
        {
            Customer = customer?.Clone() ?? new CustomerModel();
            InitializeComponent();
            BindData();
        }

        public CustomerModel Customer { get; }

        private void InitializeComponent()
        {
            Text = "Detalle de cliente";
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

            _addressTextBox.Multiline = true;
            _addressTextBox.Height = 120;

            layout.Controls.Add(new Label { Text = "Nombre", AutoSize = true }, 0, 0);
            layout.Controls.Add(_nameTextBox, 1, 0);
            layout.Controls.Add(new Label { Text = "Email", AutoSize = true }, 0, 1);
            layout.Controls.Add(_emailTextBox, 1, 1);
            layout.Controls.Add(new Label { Text = "Teléfono", AutoSize = true }, 0, 2);
            layout.Controls.Add(_phoneTextBox, 1, 2);
            layout.Controls.Add(new Label { Text = "Dirección", AutoSize = true }, 0, 3);
            layout.Controls.Add(_addressTextBox, 1, 3);

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
            _nameTextBox.Text = Customer.FullName;
            _emailTextBox.Text = Customer.Email;
            _phoneTextBox.Text = Customer.PhoneNumber;
            _addressTextBox.Text = Customer.Address;
        }

        private void SaveAndClose()
        {
            Customer.FullName = _nameTextBox.Text;
            Customer.Email = _emailTextBox.Text;
            Customer.PhoneNumber = _phoneTextBox.Text;
            Customer.Address = _addressTextBox.Text;

            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
