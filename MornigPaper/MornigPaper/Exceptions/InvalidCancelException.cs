using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MornigPaper.Exceptions
{
    class InvalidCancelException: MorningPaperException
    {
        public InvalidCancelException(string message, Exception e) : base(message, e) { }

        public InvalidCancelException(string message) : base(message) { }
    }
}
