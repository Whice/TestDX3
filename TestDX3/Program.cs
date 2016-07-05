using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace TestDX3
{
    static class Program
    {

        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            using (Form1 frm = new Form1())
            {
                // Show our form and initialize our graphics engine
                frm.Show();
                frm.InitializeGraphics();
                Application.Run(frm);
            }
        }
    }
}
