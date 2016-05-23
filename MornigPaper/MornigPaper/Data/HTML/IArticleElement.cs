using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MornigPaper.Data.HTML
{
    /// <summary>
    /// General interface for all article elements: text, images, links etc.
    /// </summary>
    interface IArticleElement
    {
        /// <summary>
        /// Add element to PDF.
        /// Of course text and images have different way to add them.
        /// </summary>
<<<<<<< HEAD
        void addToPdf(Document pdf);
=======
        void addToPdf(/*Some class representing PDF to add to.*/);
>>>>>>> refs/remotes/origin/GUI

        //...somethig else?
    }
}
