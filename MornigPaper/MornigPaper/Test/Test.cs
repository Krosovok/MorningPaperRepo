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
using MornigPaper.Logic;
using System.Threading;

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
            //Pdf.test();
            //Test1();
            //RssParseTest();
            //DataManagerTest();
            //TestMain();
            Test4();
        }
        private static void Test1()
        {
            //Pdf.test();
            // Some code to inspect here.
        }

        private static void TestMain()
        {
            #region InitDB
            Dictionary<string, List<string>> topics = new Dictionary<string, List<string>>();
            List<string> firstTopic = new List<string>(new string[] { "cnet" });
            //List<string> secondTopic = new List<string>(new string[] { "scienceDaily", "scienceMag" });

            topics.Add("Apple", firstTopic);
            topics.Add("Microsoft", firstTopic);
            topics.Add("Google", firstTopic);
            //topics.Add("Science & Space", secondTopic);
            topics.Add("Gaming", firstTopic);

            Dictionary<string, List<string>> websiteXpath = new Dictionary<string, List<string>>();
            List<string> firstXpath = new List<string>(new string[] { 
                "//div[@class='articleHead']/h1[1]", 
                "//div[@class='articleHead']/p[1]",
                "//span[@itemprop='image']/img[1]", 
                "//figure[@section='shortcodeImage']/figcaption/span[1]/p", 
                "//figure[@section='shortcodeImage']/figcaption/span[last()]",
                "//div[@data-use-autolinker='true']/p"});


            websiteXpath.Add("cnet", firstXpath);

            // Add xPaths!!!!!!!!!!!!
            //websiteXpath.Add("scienceDaily", secondXpath);
            //websiteXpath.Add("scienceMag", new List<string>());

            Dictionary<string, string> websiteRss = new Dictionary<string, string>();
            websiteRss.Add("cnet", "http://www.cnet.com/rss/news/");
            // websiteRss.Add("scienceDaily", "https://rss.sciencedaily.com/top/science.xml");
            //websiteRss.Add("scienceMag", "https://www.sciencemag.org/rss/news_current.xml");

            Dictionary<string, List<string>> topicKeywords = new Dictionary<string, List<string>>();
            List<string> apple = new List<string>(new string[] { "Apple", "iPhone", "iPad", "iTunes", "Mac" });
            List<string> micro = new List<string>(new string[] { "Mircrosoft", "Windows" });
            List<string> google = new List<string>(new string[] { "Google", "Android" });
            //List<string> scienceSpace = new List<string>(new string[] { "NASA", "Mars", "shuttle", "Earth" });
            List<string> gaming = new List<string>(new string[] { "game", "E3", "PC", "computer" });

            topicKeywords.Add("Apple", apple);
            topicKeywords.Add("Microsoft", micro);
            topicKeywords.Add("Google", google);
           // topicKeywords.Add("Science & Space", scienceSpace);
            topicKeywords.Add("Gaming", gaming);

            LocalDataManager ldm = new LocalDataManager();
            ldm.Topics = topics;
            ldm.WebsiteXpath = websiteXpath;
            ldm.WebsiteRss = websiteRss;
            ldm.TopicKeywords = topicKeywords;
            ldm.Serialize();
           #endregion

          
            //List<string> secondXpath = new List<string>(new string[] { 
            //    "//div[@class='head no-print']/div[1]",
            //    "//div[@class='head no-print']/div[last()]",
            //    "//h1[@id='headLine']",
            //    "//dl[@class='dl-horizontal dl-custom']/dd",
            //    "//div[@class='photo-image']/img[1]",
            //    "//div[@class='photo-image']/div[1]",
            //    "//div[@class='photo-image']/div[2]/em",
            //    "//div[@id='story_text']/p",
            //    "//div[id='story_source']/p" });
        }

        private static void Test4()
        {
            new Form1().ShowDialog();

            // Some code to inspect here.
        }
    }
}
