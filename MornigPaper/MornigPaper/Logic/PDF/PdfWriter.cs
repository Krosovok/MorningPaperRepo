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

namespace MornigPaper.Logic.PDF
{
    /// <summary>
    /// A class to create and write data to a PDF file. Uses iTextSharp library.
    /// </summary>
    class Pdf
    {
        public static void CreatePdf(string url, string fileName, List<string> xPaths)
        {
            HtmlDocument htmlDoc = new HtmlDocument();
            HtmlWeb web = new HtmlWeb();

            Document doc = new Document();
            FileStream fs = new FileStream(fileName, FileMode.Create);
            PdfWriter.GetInstance(doc, fs);
            doc.Open();

            HtmlParser parser = new HtmlParser(url);

            AddToPdf(parser.GetElements(xPaths), doc);

            doc.Close();
            fs.Close();
        }
        public static void test()
        {
            List<string> xPaths = new List<string>();
           
           
            string url = @"http://www.cnet.com/news/where-did-planet-nine-come-from/";
            string url2 = @"http://www.cnet.com/news/wood-turned-into-a-clear-material-stronger-than-glass/";
            string url3 = @"http://www.cnet.com/news/ibm-memory-advances-could-speed-up-your-phone/";

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

            CreatePdf(url, "test.pdf", xPaths);
            //CreatePdf(url2, "test.pdf", xPaths);
            //CreatePdf(url3, "test.pdf", xPaths);

            
        }


        /// <summary>
        /// Adds a single element to the specified document.
        /// </summary>
        /// <param name="element">An item to add to PDF.</param>
        /// <param name="doc">A document to add to.</param>
        static void AddToPdf(IArticleElement element, Document doc)
        {
            element.addToPdf(doc);
        }

        /// <summary>
        /// Adds an article (sequence of elements) to the specified PDF.
        /// </summary>
        /// <param name="content">Elements to add to PDF.</param>
        /// <param name="doc">A document to add to.</param>
        static void AddToPdf(Article content, Document doc)
        {
            foreach(IArticleElement el in content)
            {
                el.addToPdf(doc);
            }           
        }
    }
}


