﻿using MornigPaper.Presentation.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MornigPaper
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new MainForm());
            // Как завещал великий Ленин:
            Test.Test.RunTest();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
<<<<<<< HEAD
            Application.Run(new TestForm("test.pdf"));
            // Как завещал великий Ленин:
            //Test.Test.RunTest();
=======
            //Application.Run(new MainForm());
            // Как завещал великий Ленин:
            Test.Test.Testtt();
>>>>>>> refs/remotes/origin/GUI
        }
    }
}
