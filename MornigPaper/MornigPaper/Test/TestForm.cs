using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MornigPaper.Presentation.Controls;

namespace MornigPaper.Test
{
    public partial class TestForm : Form
    {
        public TestForm()
        {
            InitializeComponent();

            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<System.Windows.Controls.CheckBox> cb = (elementHost1.Child as UserControl1).GetSelectedCheckBoxes((elementHost1.Child as UserControl1).tvMain.Items);
        }

    }
}
