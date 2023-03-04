using System.Runtime.InteropServices;

namespace PSIP_SW_Switch
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>

        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            MainWindow mainWindow = new MainWindow();
            Application.Run(mainWindow);
        }
    }
}