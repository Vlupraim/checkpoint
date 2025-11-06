using System;
using System.Windows.Forms;

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

            // Inicia directamente con el Login, asumiendo que la BD 
            // ha sido creada manualmente por el desarrollador.
            Application.Run(new FrmLogin());
        }
    }
}