using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace MornigPaper.Data.RSS
{
    class Rss
    {
        //rss представлен xml документом
        XmlDocument rss;

        /// <summary>
        /// Инициализирует новый экземпляр класса Rss, используя url rss ресурса.
        /// </summary>
        /// <param name="url">Url rss ресурса.</param>
        public Rss(string url)
        {
            WebRequest wr = WebRequest.Create(@url);
            using (WebResponse response = wr.GetResponse())
            {
                using (Stream content = response.GetResponseStream())
                {
                    using (StreamReader sr = new StreamReader(content, Encoding.Default))
                    {
                        string s = sr.ReadToEnd();
                        rss = new XmlDocument();
                        rss.LoadXml(s);
                    }
                }
            }
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса Rss, используя локальную копию.
        /// </summary>
        /// <param name="localPath">Путь к rss файлу.</param>
        public Rss(string localPath, int stub = 0)
        {
            using (StreamReader sr = new StreamReader(localPath))
            {
                string s = sr.ReadToEnd();
                rss = new XmlDocument();
                rss.LoadXml(s);
            }
        }

        /// <summary>
        /// Возвращает Rss, представленный xml документом.
        /// </summary>
        public XmlDocument RssDoc
        {
            get { return rss; }
        }
    }
}
