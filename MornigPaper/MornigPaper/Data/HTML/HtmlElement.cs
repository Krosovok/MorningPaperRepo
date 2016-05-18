using HtmlAgilityPack;
using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTextSharp;
using iTextSharp.text;

namespace MornigPaper.Data.HTML
{
    public enum Allignment 
    {
        Left = Element.ALIGN_LEFT;
        Center = Element.ALIGN_CENTER;
        Right = Element.ALIGN_RIGHT;
        Justified = Element.ALIGN_JUSTIFIED;

    }

    class HtmlElement: IHtmlElement
    {
        private HtmlNode core;
        private Font font;
        private Allignment allignment;

        /// <summary>
        /// Creates an element from an HTML node, everything else is default.
        /// </summary>
        public HtmlElement(HtmlNode html)
        {
            core = html;
        }

        /// <summary>
        /// Creates an element from an HTML node and a font, everything else is default.
        /// </summary>
        public HtmlElement(HtmlNode html, Font font)
        {
            core = html;
            font = new Font(font);
        }

        /// <summary>
        /// Creates an element from an HTML node and a font and element's allignment, everything else is default.
        /// </summary>
        public HtmlElement(HtmlNode html, Font font, Allignment allignment)
        {
            this.core = html;
            this.font = new Font(font);
            this.allignment = allignment
        }

        /// <summary>
        /// The default font (Times New Roman, 14pt.) that's used when no font is et by a user.
        /// </summary>
        public static Font DefaultFont
        {
            get
            {
                Font defaultFont = new Font(Font.FontFamily.TIMES_ROMAN, 14);
                return defaultFont;
            }
        }

        public static Allignment DefaultAllignment
        {
            get { return Allignment.Center; }
        }

        public HtmlNode GetHtml()
        {
            return core;
        }
        public Font GetFont()
        {
            return font ?? DefaultFont;
        }

        Allignment GetAllignment()
        {
            return allignment ?? DefaultAllignment;
        }

    }
}
