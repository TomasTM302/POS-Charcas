using System;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Extensions.Configuration;
using POSCharcas.Business.Services;

namespace POSCharcas.Presentation.Forms
{
    /// <summary>
    /// Main dashboard window that exposes navigation to the primary modules of the POS system.
    /// </summary>
    public partial class MainForm : Form
    {
        private readonly AuthService _authService;
        private readonly IConfiguration _configuration;

        public MainForm(AuthService authService, IConfiguration configuration)
        {
            _authService = authService;
            _configuration = configuration;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            Text = "POS Charcas - Panel Principal";
            WindowState = FormWindowState.Maximized;
            BackColor = Color.FromArgb(242, 244, 248);

            var header = new Label
            {
                Text = "Panel Principal",
                Font = new Font("Segoe UI", 24F, FontStyle.Bold, GraphicsUnit.Point),
                AutoSize = true,
                Location = new Point(40, 30)
            };

            var buttonContainer = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.LeftToRight,
                Padding = new Padding(40, 120, 40, 40),
                AutoScroll = true
            };

            buttonContainer.Controls.Add(CreateNavButton("Ventas", (_, _) => OpenSales()));
            buttonContainer.Controls.Add(CreateNavButton("Productos", (_, _) => OpenProducts()));
            buttonContainer.Controls.Add(CreateNavButton("Clientes", (_, _) => OpenCustomers()));
            buttonContainer.Controls.Add(CreateNavButton("Reportes", (_, _) => ShowReports()));
            buttonContainer.Controls.Add(CreateNavButton("Configuración", (_, _) => ShowConfiguration()));

            Controls.Add(header);
            Controls.Add(buttonContainer);
        }

        private Button CreateNavButton(string text, EventHandler onClick)
        {
            var button = new Button
            {
                Text = text,
                Width = 200,
                Height = 120,
                Margin = new Padding(20),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(87, 153, 255),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point)
            };

            button.FlatAppearance.BorderSize = 0;
            button.Click += onClick;
            return button;
        }

        private void OpenSales()
        {
            var salesService = Bootstrapper.BuildSalesService(_configuration);
            using var form = new SalesForm(salesService);
            form.ShowDialog(this);
        }

        private void OpenProducts()
        {
            var productService = Bootstrapper.BuildProductService(_configuration);
            using var form = new ProductsForm(productService);
            form.ShowDialog(this);
        }

        private void OpenCustomers()
        {
            var customerService = Bootstrapper.BuildCustomerService(_configuration);
            using var form = new CustomersForm(customerService);
            form.ShowDialog(this);
        }

        private void ShowReports()
        {
            MessageBox.Show(this, "La sección de reportes se implementará en siguientes iteraciones.", "Reportes", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ShowConfiguration()
        {
            MessageBox.Show(this, "La configuración avanzada estará disponible más adelante.", "Configuración", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
