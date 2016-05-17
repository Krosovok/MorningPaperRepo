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
        private IEnumerable<HtmlNode> nodes;
        private IEnumerable<string> xPathExclusions;

        public HtmlParser(string url, IEnumerable<string> xPaths, IEnumerable<string> xPathExclusions)
        {
            HtmlWeb w = new HtmlWeb();
            HtmlDocument doc;
            try
            {
                doc = w.Load(url);
            }
            catch(HtmlWebException e)
            {
                throw new InternetAccessException("URL was not loaded.", e);
            }
            
            this.xPathExclusions = new List<string>(xPathExclusions);
            nodes = doc.DocumentNode.Descendants()
                .Where(n => xPaths.Contains(n.XPath))
                .ToList();
        }

        public IEnumerable<IArticleElement> GetElements()
        {
            return nodes.SelectMany(node => GetElements(node));
        }

        private IEnumerable<IArticleElement> GetElements(HtmlNode node)
        {
            List<IArticleElement> res = new List<IArticleElement>();
            if(xPathExclusions.Contains(node.XPath))
            {
                return new List<IArticleElement>();
            }

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
                    res.AddRange(GetElements(n));
                }
            }
            return res;
        }
    }
}
