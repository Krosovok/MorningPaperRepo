using MornigPaper.Exceptions;
using MornigPaper.Logic;
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
        Worker w;
        public Form1()
        {
            InitializeComponent();
            w = Worker.GetInstance();
            buttonHost1.Child = new Presentation.Controls.RoundButton();
            buttonHost1.IsPressedBrush = new LinearGradientBrush(Colors.Gray, Colors.Green, 50.0);
            buttonHost1.Text = "CHANGED";
            buttonHost1.AddStyle();
        }

        private void TopicBTN_Click(object sender, EventArgs e)
        {
            string topic = (sender as Button).Text;
            try
            {
                MessageBox.Show("Loading...");
                w.TopicArticles(topic);

            }
            catch (InitNotFinishedException ex)
            {
                MessageBox.Show("Can't create PDF yet: " + ex.Message);
            }
           
          
        }

        private void CancelBTN_Click(object sender, EventArgs e)
        {
            try
            {               
                w.StopExecution();
                MessageBox.Show("Canceled!");
            }
            catch(MorningPaperException ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void ShowBTN_Click(object sender, EventArgs e)
        {
            if(w.Loaded)
            {
                pdfViewer1.LoadFromFile(w.FileName);
            }
            else
            {
                MessageBox.Show("Document's not loaded yet, Master, wait...");
            }
        }
    }
}
