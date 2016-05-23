using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MornigPaper.Data.HTML
{
    /// <summary>
    /// Class for storing articles, pasred from HTML.
    /// </summary>
    class Article: List<IArticleElement>
    {
        public Article(IEnumerable<IArticleElement> elements): base(elements) {}

        public Article() : base() { }
       
        /// <summary>
        /// We need to add elements to our article to construct it.
        /// </summary>
        /// <param name="elem">Element which need to be added to the article.</param>

        void append(IArticleElement elem)
        {
            this.Add(elem);
        }


        void AddToPdf(Document doc)
        {
            this.ForEach(el => el.addToPdf(doc));
        }

    }
}
