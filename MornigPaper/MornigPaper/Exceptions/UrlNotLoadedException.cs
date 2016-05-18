using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MornigPaper.Exceptions
{
    class InternetAccessException: MorningPaperException
    {
        public InternetAccessException(string message, Exception e) : base(message, e) { }     
    }
}
