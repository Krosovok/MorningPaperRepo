namespace MornigPaper.Presentation.Forms
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pdfViewer1 = new Spire.PdfViewer.Forms.PdfViewer();
            this.buttonHost1 = new MornigPaper.Presentation.Controls.ButtonHost();
            this.SuspendLayout();
            // 
            // pdfViewer1
            // 
            this.pdfViewer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pdfViewer1.IsToolBarVisible = true;
            this.pdfViewer1.Location = new System.Drawing.Point(221, 48);
            this.pdfViewer1.MultiPagesThreshold = 60;
            this.pdfViewer1.Name = "pdfViewer1";
            this.pdfViewer1.Size = new System.Drawing.Size(277, 341);
            this.pdfViewer1.TabIndex = 1;
            this.pdfViewer1.Text = "pdfViewer1";
            this.pdfViewer1.Threshold = 60;
            // 
            // buttonHost1
            // 
            this.buttonHost1.ButtonHeight = 0D;
            this.buttonHost1.Location = new System.Drawing.Point(13, 48);
            this.buttonHost1.Name = "buttonHost1";
            this.buttonHost1.Radius = 6D;
            this.buttonHost1.Size = new System.Drawing.Size(162, 341);
            this.buttonHost1.TabIndex = 0;
            this.buttonHost1.Text = "buttonHost1";
            this.buttonHost1.ChildChanged += new System.EventHandler<System.Windows.Forms.Integration.ChildChangedEventArgs>(this.buttonHost1_ChildChanged);
            this.buttonHost1.Child = null;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(510, 401);
            this.Controls.Add(this.pdfViewer1);
            this.Controls.Add(this.buttonHost1);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.ButtonHost buttonHost1;
        private Spire.PdfViewer.Forms.PdfViewer pdfViewer1;

    }
}