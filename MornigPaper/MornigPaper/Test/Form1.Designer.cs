namespace MornigPaper.Test
{
    partial class Form1
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
            this.AppleTopicBTN = new System.Windows.Forms.Button();
            this.MSTopicBTN = new System.Windows.Forms.Button();
            this.CancelBTN = new System.Windows.Forms.Button();
            this.buttonHost1 = new MornigPaper.Presentation.Controls.ButtonHost();
            this.ShoeBTN = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // pdfViewer1
            // 
            this.pdfViewer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pdfViewer1.IsToolBarVisible = true;
            this.pdfViewer1.Location = new System.Drawing.Point(184, 13);
            this.pdfViewer1.MultiPagesThreshold = 60;
            this.pdfViewer1.Name = "pdfViewer1";
            this.pdfViewer1.Size = new System.Drawing.Size(368, 300);
            this.pdfViewer1.TabIndex = 1;
            this.pdfViewer1.Text = "pdfViewer";
            this.pdfViewer1.Threshold = 60;
            // 
            // AppleTopicBTN
            // 
            this.AppleTopicBTN.Location = new System.Drawing.Point(12, 13);
            this.AppleTopicBTN.Name = "AppleTopicBTN";
            this.AppleTopicBTN.Size = new System.Drawing.Size(75, 23);
            this.AppleTopicBTN.TabIndex = 2;
            this.AppleTopicBTN.Text = "Apple";
            this.AppleTopicBTN.UseVisualStyleBackColor = true;
            this.AppleTopicBTN.Click += new System.EventHandler(this.TopicBTN_Click);
            // 
            // MSTopicBTN
            // 
            this.MSTopicBTN.Location = new System.Drawing.Point(12, 42);
            this.MSTopicBTN.Name = "MSTopicBTN";
            this.MSTopicBTN.Size = new System.Drawing.Size(75, 23);
            this.MSTopicBTN.TabIndex = 2;
            this.MSTopicBTN.Text = "Microsoft";
            this.MSTopicBTN.UseVisualStyleBackColor = true;
            this.MSTopicBTN.Click += new System.EventHandler(this.TopicBTN_Click);
            // 
            // CancelBTN
            // 
            this.CancelBTN.Location = new System.Drawing.Point(12, 121);
            this.CancelBTN.Name = "CancelBTN";
            this.CancelBTN.Size = new System.Drawing.Size(75, 23);
            this.CancelBTN.TabIndex = 3;
            this.CancelBTN.Text = "Cancel";
            this.CancelBTN.UseVisualStyleBackColor = true;
            this.CancelBTN.Click += new System.EventHandler(this.CancelBTN_Click);
            // 
            // buttonHost1
            // 
            this.buttonHost1.Location = new System.Drawing.Point(12, 255);
            this.buttonHost1.Name = "buttonHost1";
            this.buttonHost1.Size = new System.Drawing.Size(147, 58);
            this.buttonHost1.TabIndex = 0;
            this.buttonHost1.Child = null;
            // 
            // ShoeBTN
            // 
            this.ShoeBTN.Location = new System.Drawing.Point(13, 92);
            this.ShoeBTN.Name = "ShoeBTN";
            this.ShoeBTN.Size = new System.Drawing.Size(75, 23);
            this.ShoeBTN.TabIndex = 4;
            this.ShoeBTN.Text = "Show PDF";
            this.ShoeBTN.UseVisualStyleBackColor = true;
            this.ShoeBTN.Click += new System.EventHandler(this.ShowBTN_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(564, 325);
            this.Controls.Add(this.ShoeBTN);
            this.Controls.Add(this.CancelBTN);
            this.Controls.Add(this.MSTopicBTN);
            this.Controls.Add(this.AppleTopicBTN);
            this.Controls.Add(this.pdfViewer1);
            this.Controls.Add(this.buttonHost1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private Presentation.Controls.ButtonHost buttonHost1;
        private Spire.PdfViewer.Forms.PdfViewer pdfViewer1;
        private System.Windows.Forms.Button AppleTopicBTN;
        private System.Windows.Forms.Button MSTopicBTN;
        private System.Windows.Forms.Button CancelBTN;
        private System.Windows.Forms.Button ShoeBTN;




    }
}