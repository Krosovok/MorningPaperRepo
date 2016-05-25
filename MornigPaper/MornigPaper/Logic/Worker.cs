using iTextSharp.text;
using MornigPaper.Data.RSS;
using MornigPaper.Exceptions;
using MornigPaper.Logic.LocalDataStoring;
using MornigPaper.Logic.PDF;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MornigPaper.Logic
{
    class Master
    {
        private static Master theOneWhoIsTheOne;
        Pdf pdf;
       
        /// <summary>
        /// A thread, where DB initialization or PDF generation.
        /// </summary>
        Thread t;

        bool loaded;
        bool init;        

        private Master()
        {
            //Loaded = false;
            //Initialized = false;
            t = new Thread(InitializeDB);
            t.Name = "Init";
            t.Start();
        }

        public event Action PDFCreated;
        public event Action DBInitialized;

        public string FileName { get; private set; }
        public bool Loaded 
        { 
            get
            {
                return loaded;
            }
            private set
            {
                loaded = value;
                if (loaded)
                {
                    if(PDFCreated != null)
                    {
                        PDFCreated();
                    }
                }
                else
                {
                    if(t != null && !t.IsAlive)
                    {
                        this.pdf.Close();
                    }
                }
                    
            }
        }
        public bool Initialized 
        { 
            get
            {
                return init;
            }
            private set
            {
                init = value;
                if (init)
                {
                    if (DBInitialized != null)
                    {
                        DBInitialized();
                    }
                }
            }
        }

        public LocalDataManager LDM { get; private set; }

        /// <summary>
        /// Begs TheOneWhoIsTheOne to do His magic.
        /// </summary>
        /// <returns></returns>
        static public Master GetInstance()
        {
            return theOneWhoIsTheOne ?? new Master();
        }

        /// <summary>
        /// Creates a pdf and populates it with articles on a given topic.
        /// </summary>
        /// <param name="topic">Topic to filter articles on.</param>
        public void TopicArticles(string topic)
        {
            if(t.IsAlive)
            {
                throw new InitNotFinishedException("Article is still loading.");
            }           

            t = new Thread(DoWork);
            t.Name = "PDF";
            t.Start(topic);
        }

        /// <summary
        /// Stops the generation of the PDF file.
        /// </summary>
        public void StopExecution()
        {
            if(t.Name == "PDF")
            {
                t.Abort();
                Loaded = false;
                File.Delete(FileName);
            }
            else if(t.Name == "Init")
            {
                throw new InitNotFinishedException("Can't abort DB init.");
            }
            else
            {
                throw new InvalidCancelException("Nothing to cancel.");
            }
        }

        /// <summary>
        /// If everything is right, generates the PDF.
        /// </summary>
        /// <param name="topic"></param>
        private void DoWork(object topic)
        {  
            Loaded = false;
            this.FileName = DateTime.Now.ToShortDateString() + "_" 
                + DateTime.Now.ToShortTimeString().Replace(':', '.') + ".pdf";
            pdf = new Pdf(this.FileName);

            foreach (string website in LDM.Topics[(string)topic])
            {
                Rss rss = new Rss(LDM.WebsiteRss[website]);
                RssParse parse = new RssParse(rss, LDM.TopicKeywords[(string)topic]);

                pdf.AddArticles(parse.Links, LDM.WebsiteXpath[website]);
            }
            pdf.Close();
            Loaded = true;
          
        }

        private void InitializeDB()
        {
            LDM = LocalDataManager.Initialize();
            Initialized = true;
        }

    }
}
