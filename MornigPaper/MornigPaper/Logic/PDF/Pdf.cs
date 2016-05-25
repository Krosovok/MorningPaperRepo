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
using MornigPaper.Data.PDF;


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
                    doc.Open();
                    return matches.Count;
                }
                
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
            HtmlWeb w = new HtmlWeb();
            HtmlDocument doc = w.Load(@"http://www.bbc.com/news/technology-36358517");
            var node = doc.DocumentNode.Descendants()
                .Where(n => n.Attributes["class"] != null && n.Attributes["class"].Value == "story-body__h1");

            List<string> urls2 = new List<string>(new string[] {
                @"https://www.sciencedaily.com/releases/2016/05/160519120708.htm",
                @"https://www.sciencedaily.com/releases/2015/11/151123100917.htm"
            });
            List<string> urls3 = new List<string>(new string[] {
                @"http://www.bbc.com/news/technology-36358517"
            });
            Pdf pdf = new Pdf("test.pdf");

            //List<string> secondXpath = new List<string>(new string[] { 
            //    "//div[@class='head-no-print']/div[1]",
            //    "//div[@class='head no-print']/div[last()]",
            //    "//h1[@id='headLine']",
            //    "//dl[@class='dl-horizontal dl-custom']/dd",
            //    "//div[@class='photo-image']/img[1]",
            //    "//div[@class='photo-image']/div[1]",
            //    "//div[@class='photo-image']/div[2]/em",
            //    "//div[@id='story_text']/p",
            //    "//div[id='story_source']/p" });

            List<string> thirdXpath = new List<string>(new string[] { 
                "//h1[@class='story-body__h1']",
                 });

            pdf.AddArticles(urls3, thirdXpath);

            if (pdf.Size > 10)
            {
                throw new FileSizeException("File is too big!");
            }

            pdf.Close();
        }

        
    }

}


