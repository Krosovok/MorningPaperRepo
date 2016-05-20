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
        Document doc;
        FileStream fs;
        PdfWriter writer;

        /// <summary>
        /// Initializes a new instance of the Pdf class with specified pdf file path.
        /// </summary>
        /// <param name="pdfPath">Path of pdf file.</param>
        public Pdf(string pdfPath)
        {
            doc = new Document();
            fs = new FileStream(pdfPath, FileMode.Create);
            writer = PdfWriter.GetInstance(doc, fs);

            doc.Open();
        }

        /// <summary>
        /// Adds article to pdf file.
        /// </summary>
        /// <param name="url">Url of the article.</param>
        /// <param name="xPaths">Xpaths of the article.</param>
        public void AddArticle(string url, List<string> xPaths)
        {
            HtmlParser hParseer = new HtmlParser(url);
            AddToPdf(hParseer.GetElements(xPaths));
        }

        /// <summary>
        /// Closes pdf file. Must be used at the end of work with object.
        /// </summary>
        public void Close()
        {
            doc.Close();
            fs.Close();
        }

        //public static void test()
        //{
        //    List<string> xPaths = new List<string>();
           
        //    HtmlDocument htmlDoc = new HtmlDocument();
        //    List<IArticleElement> res = new List<IArticleElement>();
        //    HtmlWeb web = new HtmlWeb();
        //    string url = @"http://www.cnet.com/news/where-did-planet-nine-come-from/";
        //    string url2 = @"http://www.cnet.com/news/wood-turned-into-a-clear-material-stronger-than-glass/";
        //    string url3 = @"http://www.cnet.com/news/ibm-memory-advances-could-speed-up-your-phone/";
        //    htmlDoc = web.Load(url);
        //    IEnumerable<HtmlNode> document = htmlDoc.DocumentNode.Descendants();

        //     Header.           
        //    xPaths.Add("//div[@class='articleHead']/p[1] | " + 
        //               "//div[@class='articleHead']/h1[1]");
        //     Image.
        //    xPaths.Add("//span[@itemprop='image']/img[1]");
        //     Image footer.
        //    xPaths.Add("//figure[@section='shortcodeImage']/figcaption/span[1]/p");
        //    xPaths.Add("//figure[@section='shortcodeImage']/figcaption/span[last()]");
            
        //     Article.
        //    xPaths.Add("//div[@data-use-autolinker='true']/p");



        //    var x = htmlDoc.DocumentNode.SelectNodes();

        //    using(Document doc = CreateDocument("test3.pdf", FileMode.Create))
        //    {
        //        HtmlParser parser = new HtmlParser(url3);

        //        AddToPdf(parser.GetElements(xPaths), doc);
        //    }
        //}

        //remove pubs later.
        //public static Document CreateDocument(string fileName, FileMode mode)
        //{
        //    Document doc = new Document();
        //    FileStream fs = new FileStream(fileName, mode);
        //    PdfWriter.GetInstance(doc, fs);
        //    doc.Open();
        //    return doc;
        //}

        /// <summary>
        /// Adds element to pdf file.
        /// </summary>
        /// <param name="element">Added element.</param>
        void AddToPdf(IArticleElement element)
        {
            element.addToPdf(doc);
        }

        /// <summary>
        /// Adds article to pdf file.
        /// </summary>
        /// <param name="content">Added article.</param>
        void AddToPdf(Article content)
        {
            foreach(IArticleElement el in content)
            {
                el.addToPdf(doc);
            }           
        }
    }
}


