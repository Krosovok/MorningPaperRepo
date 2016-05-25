using HtmlAgilityPack;
using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MornigPaper.Data.HTML
{
    /// <summary>
    /// Interface for an HTML elements that will be created directly from the DB.
    /// It's core data is an HTMLNode and it may also have some additional info like font style and size.
    /// </summary>
    interface IHtmlElement
    {
        /// <summary>
        /// Gets the HTML part of the element represented by an HtmlNode.
        /// </summary>
        HtmlNode GetHtml();

        /// <summary>
        /// Gets the font used to format plain text extracted from html.
        /// </summary>
        Font GetFont();

        /// <summary>
        /// Gets the integer number representing the element's allignment in th PDF.
        /// </summary>
        /// <returns></returns>
        Allignment GetAllignment();
    }
}
