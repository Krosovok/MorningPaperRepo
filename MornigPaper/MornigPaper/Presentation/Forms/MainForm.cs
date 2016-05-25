using MornigPaper.Exceptions;
using MornigPaper.Logic;
using MornigPaper.Presentation.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using PreviewHandlers;
//using BitMiracle.LibTiff;

namespace MornigPaper.Presentation.Forms
{
    /// <summary>
    /// This is the main form of the application: our start point
    /// </summary>
    public partial class MainForm : Form
    {
        Master w;
        public MainForm()
        {
            InitializeComponent();
            InitButtons();
        }

        private void InitButtons()
        {
            this.buttonHost1.Child = new RoundButtons();
            this.buttonHost1.ButtonHeight = 40d;
            this.buttonHost1.BackColor = this.BackColor;

            this.buttonHost1.ButtonClicked += buttonHost1_ButtonClicked;

            this.buttonHost1.AddStyle();

        }

        private void w_DBInitialized()
        {
            buttonHost1.Invoke(new CustomDel(AddButtons));
        }

        private void w_PDFCreated()
        {
            pdfViewer1.Invoke(new CustomDel(UpdatePDFViewer));
        }

        private void buttonHost1_ButtonClicked(ButtonClickedEventArgs e)
        {
            try
            {
                w.TopicArticles(e.Data);
            }
            catch (InitNotFinishedException ex)
            {
                MessageBox.Show(ex.Message);
            }

        }


        private void UpdatePDFViewer()
        {
            pdfViewer1.LoadFromFile("PDF/" + w.FileName);
            pdfViewer1.Show();
        }

        private void AddButtons()
        {
            this.buttonHost1.AddButtons(w.LDM.Topics.Keys);
            //this.buttonHost1.Height = (int)this.buttonHost1.ButtonHeight * w.LDM.Topics.Keys.Count;            
        }

        private void buttonHost1_ChildChanged(object sender, System.Windows.Forms.Integration.ChildChangedEventArgs e)
        {

        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            w = Master.GetInstance();
            this.w.PDFCreated += w_PDFCreated;
            this.w.DBInitialized += w_DBInitialized;
        }
    }
    delegate void CustomDel();
}
