using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using POSCharcas.Business.Services;

namespace POSCharcas.Presentation.Forms
{
    /// <summary>
    /// Simple login window that validates user credentials before allowing access to the system.
    /// </summary>
    public partial class LoginForm : Form
    {
        private readonly AuthService _authService;

        private TextBox _usernameTextBox = null!;
        private TextBox _passwordTextBox = null!;
        private Label _messageLabel = null!;

        public LoginForm(AuthService authService)
        {
            _authService = authService;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            Text = "POS Charcas - Inicio de Sesión";
            Size = new Size(420, 280);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            StartPosition = FormStartPosition.CenterScreen;
            BackColor = Color.FromArgb(245, 246, 250);

            var titleLabel = new Label
            {
                Text = "Bienvenido",
                Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point),
                AutoSize = true,
                Location = new Point(140, 20)
            };

            var usernameLabel = new Label
            {
                Text = "Usuario",
                Location = new Point(50, 80),
                AutoSize = true
            };

            _usernameTextBox = new TextBox
            {
                Location = new Point(50, 100),
                Width = 300
            };

            var passwordLabel = new Label
            {
                Text = "Contraseña",
                Location = new Point(50, 140),
                AutoSize = true
            };

            _passwordTextBox = new TextBox
            {
                Location = new Point(50, 160),
                Width = 300,
                UseSystemPasswordChar = true
            };

            var loginButton = new Button
            {
                Text = "Ingresar",
                Location = new Point(50, 200),
                Width = 300,
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(87, 153, 255),
                ForeColor = Color.White
            };
            loginButton.FlatAppearance.BorderSize = 0;
            loginButton.Click += async (_, _) => await OnLoginClickedAsync();

            _messageLabel = new Label
            {
                ForeColor = Color.FromArgb(220, 76, 100),
                Location = new Point(50, 230),
                Width = 300,
                AutoSize = false
            };

            Controls.Add(titleLabel);
            Controls.Add(usernameLabel);
            Controls.Add(_usernameTextBox);
            Controls.Add(passwordLabel);
            Controls.Add(_passwordTextBox);
            Controls.Add(loginButton);
            Controls.Add(_messageLabel);
        }

        private async Task OnLoginClickedAsync()
        {
            _messageLabel.Text = string.Empty;
            try
            {
                bool isValid = await _authService.ValidateCredentialsAsync(_usernameTextBox.Text, _passwordTextBox.Text);
                if (isValid)
                {
                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    _messageLabel.Text = "Usuario o contraseña incorrectos.";
                }
            }
            catch (Exception ex)
            {
                _messageLabel.Text = $"Error al iniciar sesión: {ex.Message}";
            }
        }
    }
}
