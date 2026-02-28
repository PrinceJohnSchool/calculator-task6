using System;
using System.Windows.Forms;

namespace CalculatorApp
{
    /// <summary>
    /// Main entry point for the Calculator Application.
    /// This class initializes the Windows Forms application and launches the calculator form.
    /// </summary>
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// Sets up Windows Forms visual styles and starts the calculator form.
        /// </summary>
        [STAThread] // Required for Windows Forms applications - Single Threaded Apartment
        static void Main()
        {
            // Enable visual styles for modern Windows appearance
            Application.EnableVisualStyles();
            // Use compatible text rendering for better font rendering
            Application.SetCompatibleTextRenderingDefault(false);
            // Create and run the main calculator form
            Application.Run(new CalculatorForm());
        }
    }
}
