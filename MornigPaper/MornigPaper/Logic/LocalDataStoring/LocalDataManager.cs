using MornigPaper.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MornigPaper.Logic.LocalDataStoring
{

    class LocalDataManager
    {
        Dictionary<string, List<string>> topics;
        Dictionary<string, List<string>> websiteXpath;
        Dictionary<string, string> websiteRss;

        /// <summary>
        /// Initialize LocalDataManager object.
        /// </summary>
        public LocalDataManager()
        {
            topics = new Dictionary<string, List<string>>();
            websiteXpath = new Dictionary<string, List<string>>();
            websiteRss = new Dictionary<string, string>();
        }

        /// <summary>
        /// Returns Dictionary with topic as a key and related websites.
        /// </summary>
        public Dictionary<string, List<string>> Topics
        {
            get { return topics; }
            set { topics = value; }
        }

        /// <summary>
        /// Returns Dictionary wih website as a key and related xpathes.
        /// </summary>
        public Dictionary<string, List<string>> WebsiteXpath
        {
            get { return websiteXpath; }
            set { websiteXpath = value; }
        }

        /// <summary>
        /// Returns Dictionary with website as a key and related rss.
        /// </summary>
        public Dictionary<string, string> WebsiteRss
        {
            get { return websiteRss; }
            set { websiteRss = value; }
        }

        /// <summary>
        /// Initialize LocalDataManager object with stored data.
        /// </summary>
        /// <returns>Returns LocalDataManager object filled with stored data.</returns>
        public static LocalDataManager Initialize()
        {
            DataManager dm = DataManager.Initialize();
            LocalDataManager ldm = new LocalDataManager();
            foreach (KVPair<string, List<string>> pair in dm.Topics)
            {
                ldm.Topics.Add(pair.Key, pair.Value);
            }
            foreach (KVPair<string, List<string>> pair in dm.WebsiteXpath)
            {
                ldm.WebsiteXpath.Add(pair.Key, pair.Value);
            }
            foreach (KVPair<string, string> pair in dm.WebsiteRss)
            {
                ldm.WebsiteRss.Add(pair.Key, pair.Value);
            }
            return ldm;
        }

        /// <summary>
        /// Serialize data. Must be used before finishing work with class.
        /// </summary>
        public void Serialize()
        {
            DataManager dm = new DataManager();
            foreach (KeyValuePair<string, List<string>> pair in topics)
            {
                dm.Topics.Add(new KVPair<string, List<string>>(pair.Key, pair.Value));
            }
            foreach (KeyValuePair<string, List<string>> pair in websiteXpath)
            {
                dm.WebsiteXpath.Add(new KVPair<string, List<string>>(pair.Key, pair.Value));
            }
            foreach (KeyValuePair<string, string> pair in websiteRss)
            {
                dm.WebsiteRss.Add(new KVPair<string, string>(pair.Key, pair.Value));
            }
            dm.Serialize();
        }
    }
}
