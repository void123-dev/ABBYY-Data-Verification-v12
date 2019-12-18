using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace VisualEditor
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            string pathWorkDirectory = "";
            string nameFileLoad = "";

            if (args.Length == 0)
            {
                MessageBox.Show("Не указана рабочая папка в аргументах для запуска, приложение будет закрыто.");
                Environment.Exit(0);
            }                
            else if (args.Length == 2)
            {
                pathWorkDirectory = args[0];
                nameFileLoad = args[1];
            }
            else if (args.Length == 1)
            {
                pathWorkDirectory = args[0];
            }
            else if (args.Length > 2)
            {
                MessageBox.Show("При запуске программы использовано больше двух параметров");
                Environment.Exit(0);
            }
                

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm(pathWorkDirectory, nameFileLoad));


        }
    }
}
