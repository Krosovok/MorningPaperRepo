using HtmlAgilityPack;
using iTextSharp.text;
using MornigPaper.Data.HTML;
using MornigPaper.Logic.PDF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MornigPaper.Logic.HTML
{
    /// <summary>
    /// Converter from HTML article to  
    /// I suppose we will use HtmlAgilityPack for processing HTML.
    /// </summary>
    class HtmlParser
    {
        public static List<HtmlNode> filterHtml(string url, params string[] xPath)
        {

            HtmlWeb w = new HtmlWeb();
            HtmlDocument doc = w.Load(url);
            List<HtmlNode> res = new List<HtmlNode>();

            IEnumerable<HtmlNode> nodes = doc.DocumentNode.Descendants();
            return nodes.Where(n => xPath.Contains(n.XPath)).ToList();
        }

        public static List<IArticleElement> getElements(HtmlNode node)
        {
            List<IArticleElement> res = new List<IArticleElement>();
            if (node.OuterHtml.StartsWith("<p"))
            {
                res.Add(new PDFText(node, ElementType.Paragraph));
            }
            else if (node.OuterHtml.StartsWith("<h"))
            {
                res.Add(new PDFText(node, ElementType.Header));
            }
            else if (node.OuterHtml.StartsWith("<img"))
            {
                res.Add(new PDFImage(node));
            }
            else
            {
                foreach (HtmlNode n in node.ChildNodes)
                {
                    res.AddRange(getElements(n));
                }
            }
            return res;
        }
    }
}
