using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MornigPaper.Exceptions
{
    class InvalidElementException: MorningPaperException
    {
        public InvalidElementException(string message, Exception e) : base(message, e) { }

        public InvalidElementException(string message) : base(message) { } 
    }
}
