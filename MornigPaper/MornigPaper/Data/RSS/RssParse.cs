using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace MornigPaper.Data.RSS
{
    /// <summary>
    /// Class to somehow contan RSS (Feed?) data.
    /// </summary>
    class RssParse
    {
        //список ключевых слов, список ссылок, Rss документ, который парсится
        List<string> keywords;
        List<string> links;
        Rss rssParsed;

        /// <summary>
        /// Инициализирует новый экземпляр класса RssParse.
        /// </summary>
        /// <param name="rssParsed">Rss документ, который парсится</param>
        /// <param name="keywords">ключевые слова</param>
        public RssParse(Rss rssParsed, List<string> keywords)
        {
            this.rssParsed = rssParsed;
            this.keywords = keywords;
            links = new List<string>();
        }

        /// <summary>
        /// Возвращает или задает Rss документ, который парсится.
        /// </summary>
        public Rss RssParsed
        {
            get { return rssParsed; }
            set { rssParsed = value; }
        }

        /// <summary>
        /// Возвращает или задает список ключевых слов.
        /// </summary>
        public List<string> Keywords
        {
            get { return keywords; }
            set { keywords = value; }
        }

        /// <summary>
        /// Возвращает список ссылок в результате парсинга.
        /// </summary>
        public List<string> Links
        {
            get
            {
                Parse();
                return links; 
            }
        }

        //метод парсинга основан на рекурсивном обходе.
        void Parse()
        {
            if (rssParsed.RssDoc.HasChildNodes)
            {
                foreach (XmlNode node in rssParsed.RssDoc.ChildNodes)
                {
                    TraverseNode(node);
                }
            }
        }

        //метод для рекурсивного обхода всех узлов, с вызовом метода-посетителя для каждого пройденного узла.
        void TraverseNode(XmlNode node)
        {
            //вызываем метод-посетитель
            NodeVisitor(node);

            //рекурсивно обходим детей
            if (node.HasChildNodes)
            {
                foreach (XmlNode n in node)
                {
                    TraverseNode(n);
                }
            }
        }

        //Вызывается для каждого узла при рекурсивном обходе.
        void NodeVisitor(XmlNode node)
        {
            //смотрим есть ли дети
            if (node.HasChildNodes)
            {
                string link = "";
                string desc = "";
                //ищем среди детей link и description, если находим копируем
                foreach (XmlNode n in node.ChildNodes)
                {
                    if (n.LocalName == "link")
                    {
                        link = n.InnerText;
                    }
                    if (n.LocalName == "description")
                    {
                        desc = n.InnerText;
                    }
                }
                //если есть оба ищем в description ключевые слова
                if (link != "" && desc != "")
                {
                    foreach (string keyword in keywords)
                    {
                        if (desc.Contains(keyword))
                        {
                            links.Add(link);
                            break;
                        }
                    }
                }
            }
        }
    }
}
