using HtmlAgilityPack;
using iTextSharp.text;
using MornigPaper.Data.HTML;
using MornigPaper.Logic.PDF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MornigPaper.Exceptions;
using MornigPaper.Properties;

namespace MornigPaper.Logic.HTML
{
    /// <summary>
    /// Converts an HTML page into an Article object which can be added to PDF
    /// </summary>
    class HtmlParser
    {
        private HtmlNode rootNode;

        /// <summary>
        /// Creates an HTML parser for the specific web page specifies by it's URL.
        /// </summary>
        /// <param name="url">Web-page address. </param>
        public HtmlParser(string url)
        {
            //FontFactory.RegisterDirectory(@"C:\Windows\Fonts");
            HtmlWeb w = new HtmlWeb();
            HtmlDocument doc;
            try
            {
                doc = w.Load(url);
                rootNode = doc.DocumentNode;

            }
            catch(HtmlWebException e)
            {
                throw new InternetAccessException("URL was not loaded.", e);
            }
        }

        /// <summary>
        /// Retrieves the information from the selected web-page using xPath into an Article.
        /// </summary>
        /// <param name="xPaths"> Path to the nodes that directly contain all necessary information.</param>
        /// <returns></returns>
        public Article GetElements(IEnumerable<string> xPaths)
        {
           Article res = new Article();
           foreach(string path in xPaths)
           {
                IEnumerable<HtmlNode> nodes = rootNode.SelectNodes(path);
                if(nodes == null)
                    continue;
                foreach(HtmlNode node in nodes)
                {
                    res.Add(CreateElement(node));
                }            
           }
           return res;
        }

        /// <summary>
        /// Create an Article Element e.g. paragraph, image, header etc. from an HTML node.
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private IArticleElement CreateElement(HtmlNode node)
        {
            if(node.OuterHtml.StartsWith("<p") || 
               node.OuterHtml.StartsWith("<div") ||
               node.OuterHtml.StartsWith("<dd"))
            {
                //var x = FontFactory.RegisteredFonts;
                //x = FontFactory.RegisteredFamilies;
                Font font = FontFactory.GetFont("times-roman");

                return new PDFText(node, ElementType.Paragraph, font);
            }
            else if(node.OuterHtml.StartsWith("<h"))
            {
                return new PDFText(node, ElementType.Header);
            }
            else if(node.OuterHtml.StartsWith("<img"))
            {
               return new PDFImage(node);
            }
            else if(node.OuterHtml.StartsWith("<span") || 
                    node.OuterHtml.StartsWith("<em"))
            {
                return new PDFText(node, ElementType.Footer);
            }
            else
            {
                throw new InvalidElementException("Unrecognized element, check your input.");
            }
            
        }  
    }
}
