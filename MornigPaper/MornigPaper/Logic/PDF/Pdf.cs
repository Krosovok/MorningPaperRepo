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
            pdfPath = "PDF/" + pdfPath;
            FontFactory.RegisterDirectory("Fonts");
            doc = new Document();
            try
            {
                if(!Directory.Exists("PDF"))
                {
                    Directory.CreateDirectory("PDF");
                }

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

        public event Action ArticleAdded;

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
            }
        }

        public void AddArticles(List<string> links, List<string> xPaths)
        {
            if(links.Count == 0)
            {
                //this.AddToPdf(new PDFText("Nothing was found, try another topic.", 
                //    ElementType.Header, Allignment.Center));
                return;
            }

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
            Article a = hParser.GetElements(xPaths);
            
            if(a.Count > 3)
            {
                AddToPdf(a);
            AddToPdf(new PDFText("THE END", ElementType.Paragraph, Allignment.Center));
                for (int i = 0; i < 5; i++)
                {
            AddToPdf(PDFText.NewLine);
                } 
            }
            
           
            OnArticleAdded();
        }

        /// <summary>
        /// Closes pdf file. Must be used at the end of work with object.
        /// </summary>
        public void Close()
        {
            doc.Add(new Chunk(Environment.NewLine));
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
            List<string> urls3 = new List<string>(new string[] {
                @"http://www.bbc.com/news/technology-36358517",
                @"http://www.bbc.com/news/technology-36376962"
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
                "//div[@class='byline']/span",
                "//img[@class='js-image-replace']",
                "//div[@class='story-body__inner']/p"
                 });

            pdf.AddArticles(urls3, thirdXpath);

            if (pdf.Size > 10)
            {
                throw new FileSizeException("File is too big!");
            }

            pdf.Close();
        }

        private void OnArticleAdded()
        {
            if (ArticleAdded != null)
            {
                ArticleAdded();
            }
        }
    }

}


