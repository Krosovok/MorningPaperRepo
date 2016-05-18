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
            List<string> xPaths = new List<string>();
           
            HtmlDocument htmlDoc = new HtmlDocument();
            List<IArticleElement> res = new List<IArticleElement>();
            HtmlWeb web = new HtmlWeb();
            string url = @"http://www.cnet.com/news/where-did-planet-nine-come-from/";
            string url2 = @"http://www.cnet.com/news/wood-turned-into-a-clear-material-stronger-than-glass/";
            string url3 = @"http://www.cnet.com/news/ibm-memory-advances-could-speed-up-your-phone/";
            htmlDoc = web.Load(url);
            IEnumerable<HtmlNode> document = htmlDoc.DocumentNode.Descendants();

            // Header.           
            xPaths.Add("//div[@class='articleHead']/p[1] | " + 
                       "//div[@class='articleHead']/h1[1]");
            // Image.
            xPaths.Add("//span[@itemprop='image']/img[1]");
            // Image footer.
            xPaths.Add("//figure[@section='shortcodeImage']/figcaption/span[1]/p");
            xPaths.Add("//figure[@section='shortcodeImage']/figcaption/span[last()]");
            
            // Article.
            xPaths.Add("//div[@data-use-autolinker='true']/p");



            //var x = htmlDoc.DocumentNode.SelectNodes();

            using(Document doc = CreateDocument("test3.pdf", FileMode.Create))
            {
                HtmlParser parser = new HtmlParser(url3);

                AddToPdf(parser.GetElements(xPaths), doc);
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

        static void AddToPdf(Article content, Document doc)
        {
            foreach(IArticleElement el in content)
            {
                el.addToPdf(doc);
            }           
        }
    }
}


