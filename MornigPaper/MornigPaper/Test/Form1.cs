using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MornigPaper.Test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            (this.elementHost1.Child as MornigPaper.Presentation.Controls.RoundButton).Text = "This is possible indeed.";
        }
    }
}
