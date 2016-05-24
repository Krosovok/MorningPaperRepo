using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MornigPaper.Presentation.Controls
{
    /// <summary>
    /// Представляет собой информацию, передаваему с нажатием на кнопку.
    /// </summary>
    public class ButtonClickedEventArgs
    {
        public ButtonClickedEventArgs(string data)
        {
            Data = data;
        }

        public string Data
        {
            get;
            private set;
        }
    }
}
