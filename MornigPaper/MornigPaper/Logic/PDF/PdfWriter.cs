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
//using iTextSharp.text.html.simpleparser;
//using iTextSharp.text.html;
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
            string imageXPath = "/html[1]/body[1]/div[3]/div[1]/div[3]/div[3]/div[1]/div[1]/div[2]/div[1]/article[1]/div[3]/div[2]/figure[1]/div[1]/span[1]";

            Document doc = new Document();
            HtmlDocument htmlDoc = new HtmlDocument();
            List<IArticleElement> res = new List<IArticleElement>();
            HtmlWeb web = new HtmlWeb();
            string url = @"http://www.cnet.com/news/where-did-planet-nine-come-from/";
            htmlDoc = web.Load(url);
            IEnumerable<HtmlNode> document = htmlDoc.DocumentNode.Descendants();

            //PdfWriter w = PdfWriter.GetInstance(doc, new FileStream("test.pdf", FileMode.Create));
            PdfWriter w = CreateDocument("test.pdf", FileMode.Create);


            var x = HtmlParser.filterHtml(url, headerXPath, articleXPath, imageXPath);
            x.ForEach(el => res.AddRange(HtmlParser.getElements(el)));
            doc.Open();

            //res.ForEach(el => el.addToPdf(doc));
            //AddToPdf(res, doc);
            doc.Close();

        }

        static PdfWriter CreateDocument(string fileName, FileMode mode)
        {
            return PdfWriter.GetInstance(new Document(), new FileStream(fileName, mode));
        }

        static void AddToPdf(IArticleElement element, Document doc)
        {
            element.addToPdf(doc);
        }

        static void AddToPdf(List<IArticleElement> elements, Document doc)
        {
            elements.ForEach(el => el.addToPdf(doc));
        }


    }
}


