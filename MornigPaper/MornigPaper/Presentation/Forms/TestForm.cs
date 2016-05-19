using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Spire.Pdf;
using Spire.PdfViewer;
using Spire.PdfViewer.Forms;

namespace MornigPaper.Presentation.Forms
{
    public partial class TestForm : Form
    {
        public TestForm()
        {
            InitializeComponent();
            

        }

        private void TestForm_Load(object sender, EventArgs e)
        {
            pdfViewer1.LoadFromFile("test1.pdf");
            pdfViewer1.Show();
        }
    }
}
