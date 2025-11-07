using System;
using System.Windows.Forms;
using Microsoft.Extensions.Configuration;
using POSCharcas.Business.Services;

namespace POSCharcas.Presentation
{
    /// <summary>
    /// Entry point for the POS Charcas WinForms application. It loads configuration settings,
    /// wires up the dependency graph manually and starts the login workflow.
    /// </summary>
    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            IConfiguration configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();

            var authService = Bootstrapper.BuildAuthService(configuration);

            using var loginForm = new Forms.LoginForm(authService);
            if (loginForm.ShowDialog() == DialogResult.OK)
            {
                Application.Run(new Forms.MainForm(authService, configuration));
            }
        }
    }
}
