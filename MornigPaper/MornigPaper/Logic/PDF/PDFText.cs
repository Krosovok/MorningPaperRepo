using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTextSharp;
using iTextSharp.text;
using MornigPaper.Data.HTML;

namespace MornigPaper.Logic.PDF
{
   
    /// <summary>
    /// Represents a type of a text element e.g. a phrase, a header or a paragraph.
    /// </summary>
    /// 
    enum ElementType { Phrase, Paragraph, Header, Footer, NewLine, PageBreak }

    
    /// <summary>
    /// Represents a piece of formatted text. 
    /// </summary>
    class PDFText : Data.HTML.IArticleElement
    {
        public String Content { get; set; }
        public Font Font { get; set; }
        public ElementType Type { get; set; } 

        /// <summary>
        /// The default font is Times New Roman, 14pt.
        /// </summary>
        public static Font DefaultFont 
        { 
            get
            {
                Font defaultFont = new Font(Font.FontFamily.TIMES_ROMAN, 14);
                return defaultFont;
            }
        }

        /// <summary>
        /// Creates a new PDFText from a string.
        /// </summary>
        public PDFText(string content, ElementType type)
        {
            this.Content = content;
            this.Font = DefaultFont;
            this.Type = type;
        }


        /// <summary>
        /// Creates a new PDFText from an HTML node and a Type.
        /// </summary>
        /// <param name="node">HTML node that contains necessary info.</param>
        /// <param name="type">PDFText Type (e.g. Paragraph, Header or Footer).</param>
        public PDFText(HtmlNode node, ElementType type)
        {
            this.Content = node.InnerText;
            this.Font = DefaultFont;
            this.Type = type;
        }

        /// <summary>
        /// Creates a new PDFText from an HTML node, a Type and a custom Font.
        /// </summary>
        /// <param name="node">HTML node that contains necessary info.</param>
        /// <param name="type">PDFText Type (e.g. Paragraph, Header or Footer).</param>
        /// <param name="font">A custom font used to override the default one.</param>
        public PDFText(HtmlNode node, ElementType type, Font font)
        {
            this.Content = node.InnerText.Trim();
            this.Font = new Font(font);
            this.Type = type;
        }

        /// <summary>
        /// Adds this element to the target document.
        /// </summary>
        /// <param name="pdf"> A document to add to.</param>
        public void addToPdf(Document pdf)
        {
             if (this.Type == ElementType.Phrase)
             {
                 pdf.Add(new Phrase(this.Content, this.Font));
             }
             else if(this.Type == ElementType.Paragraph)
             {
                 pdf.Add(new Paragraph(this.Content, this.Font));
             }
             else if(this.Type == ElementType.Header)
             {
                 pdf.Add(new Paragraph(this.Content, new Font(this.Font.Family, this.Font.Size + 3)));
             }
             else if(this.Type == ElementType.Footer)
             {
                 pdf.Add(new Paragraph(this.Content, new Font(this.Font.Family, this.Font.Size - 3, Font.ITALIC)));
             }
             else if(this.Type == ElementType.NewLine)
             {
                 pdf.Add(Chunk.NEWLINE);
             }
             else if(this.Type == ElementType.PageBreak)
             {
                 pdf.Add(Chunk.NEXTPAGE);
             }
            
        }

        public override string ToString()
        {
            return this.Content + ". " + this.Type.ToString();
        }
    }
}


