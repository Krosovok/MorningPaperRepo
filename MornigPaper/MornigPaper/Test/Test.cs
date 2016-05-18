using MornigPaper.Data.RSS;
using MornigPaper.Logic.PDF;
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
            RssParseTest();
            Test1();
            Test2();
            Test3();
            Test4();
        }
        private static void Test1()
        {
            Pdf.test();
            // Some code to inspect here.
        }

        private static void RssParseTest()
        {
            Pdf.test();
            // Some code to inspect here.
            Rss rss = new Rss("http://www.animespirit.ru/engine/rss.php");
            List<string> keywords = (new string[] { "студент", "адвокат", "деревня", "мир" }).ToList();
            RssParse rssParse = new RssParse(rss, keywords);
            List<string> links = rssParse.Links;
        }

        private static void Test2()
        {
            // Some code to inspect here.
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
