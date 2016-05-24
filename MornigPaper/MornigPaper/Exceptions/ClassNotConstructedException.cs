using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MornigPaper.Exceptions
{
    class ClassNotConstructedException : MorningPaperException
    {
        public ClassNotConstructedException() : base("Trying to acces not constructed part of a class.") { }
    }
}
