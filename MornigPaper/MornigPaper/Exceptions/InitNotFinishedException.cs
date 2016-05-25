using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MornigPaper.Exceptions
{
    class InitNotFinishedException: MorningPaperException
    {
        public InitNotFinishedException(string message, Exception e) : base(message, e) { }

        public InitNotFinishedException(string message) : base(message) { }
    }
}
