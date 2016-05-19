using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MornigPaper.Presentation.Controls
{
    class ClassNotConstructedException : Exception
    {
        public ClassNotConstructedException() : base() { }
    }
}
