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
    class Worker
    {
        private static Worker theOneWhoIsTheOne;
        private LocalDataManager ldm;

        

        /// <summary>
        /// A thread, where DB initialization or PDF generation.
        /// </summary>
        Thread t;

        private Worker()
        {
            Loaded = false;
            t = new Thread(InitializeDB);
            t.Name = "Init";
            t.Start();
        }

        public string FileName { get; private set; }
        public bool Loaded { get; private set; } 

        /// <summary>
        /// Begs TheOneWhoIsTheOne to do His magic.
        /// </summary>
        /// <returns></returns>
        static public Worker GetInstance()
        {
            return theOneWhoIsTheOne ?? new Worker();
        }

        /// <summary>
        /// Creates a pdf and populates it with articles on a given topic.
        /// </summary>
        /// <param name="topic">Topic to filter articles on.</param>
        public void TopicArticles(string topic)
        {
            if(t.IsAlive)
            {
                throw new InitNotFinishedException("Init not finished");
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
            else
            {
                throw new InitNotFinishedException("Can't abort DB init.");
            }
        }

        /// <summary>
        /// If everything is right, generates the PDF.
        /// </summary>
        /// <param name="topic"></param>
        private void DoWork(object topic)
        {
            this.FileName = DateTime.Now.ToShortDateString() + "_" 
                + DateTime.Now.ToShortTimeString().Replace(':', '.') + ".pdf";
            Pdf pdf = new Pdf(this.FileName);
            foreach (string website in ldm.Topics[(string)topic])
            {
                Rss rss = new Rss(ldm.WebsiteRss[website]);
                RssParse parse = new RssParse(rss, ldm.TopicKeywords[(string)topic]);

                pdf.AddArticles(parse.Links, ldm.WebsiteXpath[website]);
            }
            pdf.Close();
            Loaded = true;
        }

        private void InitializeDB()
        {
            ldm = LocalDataManager.Initialize();
        }
    }
}
