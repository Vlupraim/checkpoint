using Checkpoint.Data;
using System;
using System.IO;
using System.Windows.Forms;

namespace checkpoint
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            // ejecuta solo si no existe el log de inicialización
            var dbInitFlag = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "dbinit.done");
            if (!File.Exists(dbInitFlag))
            {
                DatabaseInitializer.EnsureDatabase();
                File.WriteAllText(dbInitFlag, "initialized");
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FrmLogin());
        }
    }
}