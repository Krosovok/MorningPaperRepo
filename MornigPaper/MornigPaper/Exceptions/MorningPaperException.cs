using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MornigPaper.Exceptions
{
    abstract class MorningPaperException: Exception
    {
        public MorningPaperException(string message, Exception e) : base(message, e) { } 
    }
}
