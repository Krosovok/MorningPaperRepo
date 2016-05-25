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
        Worker w;
        private bool inProgress;

        public MainForm()
        {
            InitializeComponent();
            w = Worker.GetInstance();
            InitButtons(); 
            this.w.LoadStarted += w_LoadStarted;
            this.w.ArticleAdded += w_ArticleAdded;
            this.w.PDFLoaded += w_PDFLoaded;
            this.w.DBInitialized += w_DBInitialized;
            InProgress = false;
        }

        private bool InProgress
        {
            get
            {
                return inProgress;
            }
            set
            {
                this.downloadProgressBar.Visible = value;
                this.cancelButton.Visible = value;
                this.pdfViewer1.Visible = !value;
                inProgress = value;
            }
        }

        private void InitButtons()
        {
            this.buttonHost1.Child = new RoundButtons();
            this.buttonHost1.ButtonHeight = 40d;
            this.buttonHost1.BackColor = this.BackColor;
            this.buttonHost1.ButtonClicked += buttonHost1_ButtonClicked;
            this.buttonHost1.AddStyle();
            
        }

        private void buttonHost1_ButtonClicked(ButtonClickedEventArgs e)
        {
            try
            {
                w.TopicArticles(e.Data);
            }
            catch(InitNotFinishedException ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
        

        private void UpdatePDFViewer()
        {
            pdfViewer1.LoadFromFile(w.FileName);
            //pdfViewer1.Show();
            InProgress = false;
        }

        private void AddButtons()
        {
            this.buttonHost1.AddButtons(w.LDM.Topics.Keys);
            //this.buttonHost1.Height = (int)this.buttonHost1.ButtonHeight * w.LDM.Topics.Keys.Count;            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            w.StopExecution();
            InProgress = false;
        } 

        private void w_LoadStarted(int links)
        {
            this.Invoke(new Action<int>(StartLoad), links);
        }

        private void StartLoad(int links)
        {
            this.downloadProgressBar.Maximum = links;
            InProgress = true;
        }

        private void w_ArticleAdded()
        {
            this.Invoke(new Action(Step));
        }

        private void Step()
        {
            this.downloadProgressBar.PerformStep();
        }

        private void w_PDFLoaded()
        {
            pdfViewer1.Invoke(new Action(UpdatePDFViewer));
        }

        private void w_DBInitialized()
        {
            buttonHost1.Invoke(new Action(AddButtons));
        }

    }

    //delegate void CustomDel();
}
