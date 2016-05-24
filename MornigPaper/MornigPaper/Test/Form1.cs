using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
//using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media;

namespace MornigPaper.Test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            buttonHost1.Child = new Presentation.Controls.RoundButtons();
            //buttonHost1.IsPressedBrush = new LinearGradientBrush(Colors.Gray, Colors.Green, 50.0);
            //buttonHost1.Text = "CHANGED";
            buttonHost1.ButtonHeight = 50d;
            buttonHost1.AddButtons(new string[] { "One", "Two", "Three", "Five", "E", "Pi", "sqrt100" });
            buttonHost1.AddStyle();
        }
    }
}
