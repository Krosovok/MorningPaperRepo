using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MornigPaper.Exceptions
{
    class FileSizeException : MorningPaperException
    {
        public FileSizeException(string message, Exception e) : base(message, e) { }

        public FileSizeException(string message) : base(message) { }
    }
}
