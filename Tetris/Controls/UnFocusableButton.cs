using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Tetris.Controls
{
    public class UnFocusableButton : Button
    {
        public UnFocusableButton()
        {
            SetStyle(ControlStyles.Selectable, false);
        }
    }
}
