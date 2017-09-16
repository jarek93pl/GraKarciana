using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    static class Program
    {
        static int R = 0;
        /// <summary>
        /// Główny punkt wejścia dla aplikacji.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Action ax=()=> Application.Run(new Form1() { Nazwa=R++.ToString()});
            Task.WaitAll(Task.Factory.StartNew( ax), Task.Factory.StartNew(ax), Task.Factory.StartNew(ax));
        }
    }
}
