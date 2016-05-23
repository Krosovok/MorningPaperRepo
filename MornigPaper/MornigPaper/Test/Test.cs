using MornigPaper.Data;
using MornigPaper.Data.RSS;
using MornigPaper.Logic.PDF;
using MornigPaper.Logic.LocalDataStoring;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using HtmlAgilityPack;
using iTextSharp.text;
using MornigPaper.Logic.HTML;
using iTextSharp.text.pdf;

namespace MornigPaper.Test
{
    /// <summary>
    /// Class to play with written code.
    /// Don't remove tests! They may be needed later!
    /// </summary>
    static class Test
    {
        /// <summary>
        /// Start some test code.
        /// </summary>
        public static void RunTest()
        {
            //RssParseTest();
            //Test1();
            //RssParseTest();
            //DataManagerTest();
            TestMain();
            Test4();
        }
        private static void Test1()
        {
            //Pdf.test();
            // Some code to inspect here.
        }

        private static void RssParseTest()
        {
            //Pdf.test();
            // Some code to inspect here.
            Rss rss = new Rss("http://www.animespirit.ru/engine/rss.php");
            List<string> keywords = (new string[] { "студент", "адвокат", "деревня", "мир" }).ToList();
            RssParse rssParse = new RssParse(rss, keywords);
            List<string> links = rssParse.Links;
        }

        private static void DataManagerTest()
        {
            Dictionary<string, List<string>> topics = new Dictionary<string, List<string>>();
            List<string> firstTopic = new List<string>(new string[] {"website1", "website2", "website3" });
            List<string> secondTopic = new List<string>(new string[] { "website4", "website5" });
            List<string> thirdTopic = new List<string>(new string[] { "website6" });
            topics.Add("first", firstTopic);
            topics.Add("second", secondTopic);
            topics.Add("third", thirdTopic);
            Dictionary<string, List<string>> websiteXpath = new Dictionary<string, List<string>>();
            List<string> firstXpath = new List<string>(new string[] { "wtf1", "wtf2", "wtf3" });
            List<string> secondXpath = new List<string>(new string[] { "wtf" });
            websiteXpath.Add("website1", firstXpath);
            websiteXpath.Add("website2", secondXpath);
            Dictionary<string, string> websiteRss = new Dictionary<string, string>();
            websiteRss.Add("website1", "rss1");
            websiteRss.Add("website2", "rss2");
            websiteRss.Add("website3", "rss3");
            LocalDataManager ldm = new LocalDataManager();
            ldm.Topics = topics;
            ldm.WebsiteXpath = websiteXpath;
            ldm.WebsiteRss = websiteRss;
            ldm.Serialize();
            LocalDataManager ldmg = LocalDataManager.Initialize();
        }

        private static void TestMain()
        {
            //Dictionary<string, List<string>> topics = new Dictionary<string, List<string>>();
            //List<string> firstTopic = new List<string>(new string[] { "cnet"});
            //List<string> secondTopic = new List<string>(new string[] { "scienceDaily", "scienceMag" });

            //topics.Add("Apple", firstTopic);
            //topics.Add("Microsoft", firstTopic);
            //topics.Add("Google", firstTopic);
            //topics.Add("Science & Space", secondTopic);
            //topics.Add("Gaming", firstTopic);

            //Dictionary<string, List<string>> websiteXpath = new Dictionary<string, List<string>>();
            //List<string> firstXpath = new List<string>(new string[] { "//div[@class='articleHead']/p[1] | " 
            //    + "//div[@class='articleHead']/h1[1]",
            //    "//span[@itemprop='image']/img[1]", 
            //    "//figure[@section='shortcodeImage']/figcaption/span[1]/p", 
            //    "//figure[@section='shortcodeImage']/figcaption/span[last()]",
            //    "//div[@data-use-autolinker='true']/p"});

            //websiteXpath.Add("cnet", firstXpath);

            //Dictionary<string, string> websiteRss = new Dictionary<string, string>();
            //websiteRss.Add("cnet", "http://www.cnet.com/rss/news/");
            //websiteRss.Add("scienceDaily", "https://rss.sciencedaily.com/top/science.xml");
            //websiteRss.Add("scienceMag", "https://www.sciencemag.org/rss/news_current.xml");

            //Dictionary<string, List<string>> topicKeywords = new Dictionary<string, List<string>>();
            //List<string> apple = new List<string>(new string[] { "Apple", "iPhone", "iPad", "iTunes", "Mac" });
            //List<string> micro = new List<string>(new string[] { "Mircrosoft", "Windows" });
            //List<string> google = new List<string>(new string[] { "Google", "Android" });
            //List<string> scienceSpace = new List<string>(new string[] { "NASA", "Mars", "shuttle", "Earth" });
            //List<string> gaming = new List<string>(new string[] { "game", "E3", "PC", "computer" });

            //topicKeywords.Add("Apple", apple);
            //topicKeywords.Add("Microsoft", micro);
            //topicKeywords.Add("Google", google);
            //topicKeywords.Add("Science & Space", scienceSpace);
            //topicKeywords.Add("Gaming", gaming);

            //LocalDataManager ldm = new LocalDataManager();
            //ldm.Topics = topics;
            //ldm.WebsiteXpath = websiteXpath;
            //ldm.WebsiteRss = websiteRss;
            //ldm.TopicKeywords = topicKeywords;
            //ldm.Serialize();
            LocalDataManager ldm = LocalDataManager.Initialize();
            //Pdf pdf = new Pdf("wtfpdf.pdf");
            //foreach (KeyValuePair<string, List<string>> pair in ldm.Topics)
            //{
            //    foreach (string website in pair.Value)
            //    {
            //        Rss rss = new Rss(ldm.WebsiteRss[website]);
            //        RssParse parse = new RssParse(rss, new List<string>(new string[] { "gameasasd", "ga", "gae", "gameasd", "gzfe", "gzxzme", "gafze", "adme", "ghame", "asdame", "gdfge" }));

            //        foreach (string link in parse.Links)
            //        {
            //            pdf.AddArticle(link, ldm.WebsiteXpath[website]);
            //        }

            //    }
            //}
            //pdf.Close();
        }

        private static void Test4()
        {
            // Some code to inspect here.
        }
    }
}
