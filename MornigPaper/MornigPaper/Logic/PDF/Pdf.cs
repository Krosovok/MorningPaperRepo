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
using MornigPaper.Presentation.Forms;
using MornigPaper.Exceptions;
using System.Text.RegularExpressions;


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
        string pdfPath;

        /// <summary>
        /// Initializes a new instance of the Pdf class with specified pdf file path.
        /// </summary>
        /// <param name="pdfPath">Path of pdf file.</param>
        public Pdf(string pdfPath)
        {
            FontFactory.RegisterDirectory("Fonts");
            doc = new Document();
            try
            {
                fs = new FileStream(pdfPath, FileMode.Create);
            }
            catch(IOException)
            {
                throw new FileAccessException("You are trying to access an open file.");
            }

            this.pdfPath = pdfPath;
            writer = PdfWriter.GetInstance(doc, fs);
            doc.Open();
        }

        public int Size 
        {
            
            get 
            {
                doc.Close();
                using (StreamReader sr = new StreamReader(File.OpenRead(pdfPath)))
                {
                    Regex regex = new Regex(@"/Type\s*/Page[^s]");
                    MatchCollection matches = regex.Matches(sr.ReadToEnd());

                    return matches.Count;
                }
                doc.Open();
            }
            
        }

        public void AddArticles(List<string> links, List<string> xPaths)
        {
            links.ForEach(l => this.AddArticle(l, xPaths));
        }

        /// <summary>
        /// Adds article to pdf file.
        /// </summary>
        /// <param name="url">Url of the article.</param>
        /// <param name="xPaths">Xpaths of the article.</param>
        void AddArticle(string url, List<string> xPaths)
        {
            HtmlParser hParser = new HtmlParser(url);
            AddToPdf(hParser.GetElements(xPaths));
            AddToPdf(new PDFText("THE END", ElementType.Paragraph, Allignment.Center));
            AddToPdf(PDFText.NewLine);
           

        }

        /// <summary>
        /// Closes pdf file. Must be used at the end of work with object.
        /// </summary>
        public void Close()
        {
            doc.Close();
            fs.Close();
        }
     
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
                if (el is PDFImage)
                {
                    PDFImage image = el as PDFImage;
                    if(image.Content.Width > 3*doc.PageSize.Width / 4)
                    {
                        float k = (4 * image.Content.Width) / (3 * doc.PageSize.Width);
                        image.Content.ScaleAbsolute(image.Content.Width / k, image.Content.Height / k);
                    }                 
                    image.Allignment = Allignment.Center;
                }
                else if(el is PDFText)
                {
                    PDFText textEl = el as PDFText;
                    if(textEl.Type == ElementType.Footer)
                    {
                        textEl.Allignment = Allignment.Center;
                    }
                }

                el.addToPdf(doc);
            }           
        }

        

        public static void test()
        {
            List<string> xPaths = new List<string>();

            HtmlDocument htmlDoc = new HtmlDocument();
            List<IArticleElement> res = new List<IArticleElement>();
            HtmlWeb web = new HtmlWeb();
            
            string url = @"http://www.cnet.com/news/where-did-planet-nine-come-from/";
            string url2 = @"http://www.cnet.com/news/wood-turned-into-a-clear-material-stronger-than-glass/";
            string url3 = @"http://www.cnet.com/news/ibm-memory-advances-could-speed-up-your-phone/";

            List<string> links = new string[] { url, url2, url3 }.ToList();

            htmlDoc = web.Load(url);

            // Header.           
            xPaths.Add("//div[@class='articleHead']/p[1] | " +
                       "//div[@class='articleHead']/h1[1]");
            // Image.
            xPaths.Add("//span[@itemprop='image']/img[1]");
            // Image footer.
            xPaths.Add("//figure[@section='shortcodeImage']/figcaption/span[last()]");

            // Article.
            xPaths.Add("//div[@data-use-autolinker='true']/p");

            Pdf pdf = new Pdf("test.pdf");
            pdf.AddArticles(links, xPaths);
          
            if(pdf.Size > 10)
            {
                throw new FileSizeException("File is too big!");
            }

            pdf.Close();
        }

        
    }

}


