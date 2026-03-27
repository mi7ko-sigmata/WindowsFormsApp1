using System;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // ТУК Е ВАЖНАТА ПРОМЯНА:
            // Сменяме 'new Form1()' с 'new ClubsForm()', за да стартира твоята нова форма.
            Application.Run(new Form1());
        }
    }
}