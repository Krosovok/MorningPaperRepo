﻿using HtmlAgilityPack;
using iTextSharp.text;
using MornigPaper.Data.HTML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTextSharp;

namespace MornigPaper.Logic.PDF
{
    /// <summary>
    /// Represents an image.
    /// </summary>
    class PDFImage: IArticleElement
    {

        public PDFImage(HtmlNode node, String baseUrl)
        {
            String src = node.Attributes["src"].Value;
            this.Source = new Uri(baseUrl + src);
            this.Content = Image.GetInstance(this.Source);
        }

        /// <summary>
        /// Creates an image from the specified <img> node. 
        /// Creates a default 'image-not-found' if the picture can't be loaded.
        /// </summary>
        /// <param name="node"> HTML image node.</param>
        public PDFImage(HtmlNode node)
        {
            try
            {
                String src = node.Attributes["src"].Value;
                this.Source = new Uri(src);
                this.Content = Image.GetInstance(this.Source);
            }
            catch (HtmlWebException)
            {
                this.Content = Image.GetInstance(new Uri(@"http://www.westliguria.net/wp-content/uploads/2013/01/image-not-found.jpg")); ;
            }
        }

        /// <summary>
        /// The image itself.
        /// </summary>
        public Image Content { get; set; }

        /// <summary>
        /// URL path to the image.
        /// </summary>
        public Uri Source { get; set; }

        public Allignment Allignment { get; set; }
        
        /// <summary>
        /// Adds an image created from this element to the target document.
        /// </summary>
        /// <param name="pdf"> A document to add to.</param>
        public void addToPdf(Document pdf)
        {
            Image img = Image.GetInstance(this.Content);
            img.Alignment = (Allignment != Allignment.None)
                ? (int)Allignment : (int)Allignment.Left;
           
            pdf.Add(img);
            
        }

        /// <summary>
        /// Scales an image to a given percentage.
        /// </summary>
        /// <param name="percent">New dimensions in percents</param>
        public void Scale(float percent)
        {
            this.Content.ScalePercent(percent);
            
        }
    }
}
