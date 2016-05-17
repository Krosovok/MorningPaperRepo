using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTextSharp.text;
using iTextSharp;
using iTextSharp.text.pdf;
using System.IO;
using HtmlAgilityPack;
using MornigPaper.Data.HTML;
using MornigPaper.Logic.HTML;

/*
 * 
 * 
 * 
 * 
  !!!TO DO!!! - I need different paragraph and phrase classes, the former being a container maybe? 
 *Element's "Type" property - do I need it then? 
 *
 * 
 * 
*/
namespace MornigPaper.Logic.PDF
{
    /// <summary>
    /// It will use some library to create PDF.
    /// </summary>
    class Pdf
    {
        public static void test()
        {
            string headerXPath = "/html[1]/body[1]/div[3]/div[1]/div[1]/div[1]/div[1]/div[2]";
            string articleXPath = "/html[1]/body[1]/div[3]/div[1]/div[3]/div[3]/div[1]/div[1]/div[2]/div[1]/article[1]/div[3]/div[2]"; // - 
           // string imageXPath = "/html[1]/body[1]/div[3]/div[1]/div[3]/div[3]/div[1]/div[1]/div[2]/div[1]/article[1]/div[3]/div[2]/figure[1]/div[1]/span[1]";
            //string xPathExclusin = "/html[1]/body[1]/div[3]/div[1]/div[3]/div[3]/div[1]/div[1]/div[2]/div[1]/article[1]/div[3]/div[2]/div[2]";
           
            HtmlDocument htmlDoc = new HtmlDocument();
            List<IArticleElement> res = new List<IArticleElement>();
            HtmlWeb web = new HtmlWeb();
            string url = @"http://www.cnet.com/news/where-did-planet-nine-come-from/";
            htmlDoc = web.Load(url);
            IEnumerable<HtmlNode> document = htmlDoc.DocumentNode.Descendants();

            string xPathExclusion = document
                .First(n => n.Attributes["section"] != null && n.Attributes["section"].Value == "shortcodeRelatedLinks")
                .XPath;

            using(Document doc = CreateDocument("test.pdf", FileMode.Create))
            {
                HtmlParser parser = new HtmlParser(url, 
                    new string[] { headerXPath, articleXPath }, 
                    new string[] { xPathExclusion });

                AddToPdf(parser.GetElements(), doc);
            }
        }

        static Document CreateDocument(string fileName, FileMode mode)
        {
            Document doc = new Document();
            PdfWriter.GetInstance(doc, new FileStream(fileName, mode));
            doc.Open();
            return doc;
        }

        static void AddToPdf(IArticleElement element, Document doc)
        {
            element.addToPdf(doc);
        }

        static void AddToPdf(IEnumerable<IArticleElement> elements, Document doc)
        {
            foreach(IArticleElement el in elements)
            {
                el.addToPdf(doc);
            }           
        }
    }
}


