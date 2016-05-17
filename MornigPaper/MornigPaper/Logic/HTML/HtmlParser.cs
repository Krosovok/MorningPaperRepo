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

namespace MornigPaper.Logic.HTML
{
    /// <summary>
    /// Converter from HTML article to  
    /// I suppose we will use HtmlAgilityPack for processing HTML.
    /// </summary>
    class HtmlParser
    {
        private HtmlNode rootNode;

        public HtmlParser(string url)
        {
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

        private IArticleElement CreateElement(HtmlNode node)
        {
            if(node.OuterHtml.StartsWith("<p"))
            {
                return new PDFText(node, ElementType.Paragraph);
            }
            else if(node.OuterHtml.StartsWith("<h"))
            {
                return new PDFText(node, ElementType.Header);
            }
            else if(node.OuterHtml.StartsWith("<img"))
            {
               return new PDFImage(node);
            }
            else if(node.OuterHtml.StartsWith("<span"))
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
