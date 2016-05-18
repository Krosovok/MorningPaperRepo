using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace MornigPaper.Data
{
    [XmlType("DataManager")]
    public class DataManager
    {
        static string dataManagerPath = @"DataManager\DataManager.xml";

        List<KVPair<string, List<string>>> topics;
        List<KVPair<string, List<string>>> websiteXpath;
        List<KVPair<string, string>> websiteRss;


        /// <summary>
        /// Create new instance of DataManager class.
        /// </summary>
        public DataManager()
        {
            topics = new List<KVPair<string, List<string>>>();
            websiteXpath = new List<KVPair<string, List<string>>>();
            websiteRss = new List<KVPair<string, string>>();
        }

        /// <summary>
        /// Returns KVPair List of topics.
        /// </summary>
        public List<KVPair<string, List<string>>> Topics
        {
            get { return topics; }
            set { topics = value; }
        }

        /// <summary>
        /// Returns KVPair List of website Xpathes.
        /// </summary>
        public List<KVPair<string, List<string>>> WebsiteXpath
        {
            get { return websiteXpath; }
            set { websiteXpath = value; }
        }

        /// <summary>
        /// Returns KVPair List of website rss.
        /// </summary>
        public List<KVPair<string, string>> WebsiteRss
        {
            get { return websiteRss; }
            set { websiteRss = value; }
        }

        /// <summary>
        /// Initialize DataManager object with stored data.
        /// </summary>
        /// <returns>Returns DataManager object filled with stored data.</returns>
        public static DataManager Initialize()
        {
            XmlSerializer xs = new XmlSerializer(typeof(DataManager));
            using (FileStream fs = new FileStream(dataManagerPath, FileMode.Open))
            {
                using (XmlReader xr = XmlReader.Create(fs))
                {
                    return (DataManager)xs.Deserialize(xr);
                }
            }
        }

        /// <summary>
        /// Serialize data. Must be used before finishing work with class.
        /// </summary>
        public void Serialize()
        {
            XmlSerializer xs = new XmlSerializer(this.GetType());
            using (StreamWriter sw = new StreamWriter(dataManagerPath))
            {
                xs.Serialize(sw, this);
            }
        }
    }
}

[Serializable]
public class KVPair<K, V>
{
    public KVPair() { }
    public KVPair(K key, V value)
    {
        Key = key;
        Value = value;
    }
    public K Key { get; set; }
    public V Value { get; set; }
}
