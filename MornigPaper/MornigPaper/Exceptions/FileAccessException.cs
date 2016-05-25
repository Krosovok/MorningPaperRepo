using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MornigPaper.Exceptions
{
    class FileAccessException: MorningPaperException
    {
         public FileAccessException(string message, Exception e) : base(message, e) { }

         public FileAccessException(string message) : base(message) { } 
    }
}
