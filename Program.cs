using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Checkpoint.Data;

namespace checkpoint
{
    internal static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            // Inicializar la DB (solo en entornos de desarrollo) - maneja excepciones internamente
            try
            {
                DatabaseInitializer.EnsureDatabase();
            }
            catch (Exception ex)
            {
                // Log en consola para el desarrollador; no bloquear arranque
                Console.WriteLine("Database initialize error: " + ex.Message);
            }

            Application.Run(new FrmPrincipal());
        }
    }
}
