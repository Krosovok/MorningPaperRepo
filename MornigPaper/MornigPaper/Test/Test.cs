using MornigPaper.Data;
using MornigPaper.Data.RSS;
using MornigPaper.Logic.LocalDataStoring;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

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
            DataManagerTest();
            Test3();
            Test4();
        }

        private static void RssParseTest()
        {
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

        private static void Test3()
        {
            // Some code to inspect here.
        }

        private static void Test4()
        {
            // Some code to inspect here.
        }
    }
}
