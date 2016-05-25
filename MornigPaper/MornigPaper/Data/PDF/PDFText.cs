using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTextSharp;
using iTextSharp.text;
using MornigPaper.Data.HTML;
using MornigPaper.Exceptions;

namespace MornigPaper.Data.PDF
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
        private ElementType elementType;

        public String Content { get; set; }
        public Font Font { get; set; }
        public ElementType Type { get; set; }
        public Allignment Allignment { get; set; }

       
        public static PDFText PageBreak
        {
            get
            {
                return new PDFText("", ElementType.PageBreak);
            }
        }

        public static PDFText NewLine
        {
            get
            {
                return new PDFText("", ElementType.NewLine);
            }
        }

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
        /// Creates a new PDFText from an HTML node, Type and Allignment within a document.
        /// </summary>
        public PDFText(HtmlNode node, ElementType type, Allignment allignment)
        {
            this.Content = node.InnerText;
            this.Font = DefaultFont;
            this.Type = type;
            this.Allignment = allignment;
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
        /// Creates a new PDFText from an HTML node, a Type and a custom Font.
        /// </summary>
        public PDFText(string content, ElementType type, Allignment allignment)
        {
            this.Content = content;
            this.Font = DefaultFont;
            this.Type = type;
            this.Allignment = allignment;
        }

        /// <summary>
        /// Adds this element to the target document.
        /// </summary>
        /// <param name="pdf"> A document to add to.</param>
        public void addToPdf(Document pdf)
        {
            IElement el;
            switch(this.Type)
            {
                case ElementType.Paragraph: 
                    el = new Paragraph(this.Content, this.Font);
                    break;
                case ElementType.Phrase:
                    el = new Phrase(this.Content, this.Font);
                    break;
                case ElementType.Header:
                    el = new Paragraph(this.Content, new Font(this.Font.Family, this.Font.Size + 3));
                    break;
                case ElementType.Footer:
                    el = new Paragraph(this.Content, new Font(this.Font.Family, this.Font.Size - 3, Font.ITALIC));
                    break;
                case ElementType.NewLine:
                    el = Chunk.NEWLINE;
                    break;
                case ElementType.PageBreak:
                    el = Chunk.NEXTPAGE;
                    break;
                default:
                    throw new InvalidElementException("Unidentified element type.");
            }

            if (el is Paragraph)
            {
                Paragraph p = el as Paragraph;
                this.Content.Trim();
                p.Alignment = (this.Allignment != Allignment.None)
                ? (int)this.Allignment : (int)Allignment.Left;                        
            }               
             pdf.Add(el);
        }

        public override string ToString()
        {
            return this.Content + ". " + this.Type.ToString();
        }
    }
}


