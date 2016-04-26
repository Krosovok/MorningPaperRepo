using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MornigPaper.Data.HTML
{
    /// <summary>
    /// Interface for classes for storing articles, pasred from HTML.
    /// </summary>
    public interface IArticle
    {
        /// <summary>
        /// We need to add elements to our article to construct it.
        /// </summary>
        /// <param name="elem">Element which need to be added to the article.</param>
        void append(IArticleElement elem);

        // Convert to pdf here?

        //...something else?
    }
}
