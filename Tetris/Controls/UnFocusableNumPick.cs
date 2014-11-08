using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Tetris.Controls
{
    public class UnFocusableNumPic : NumericUpDown
    {
        public UnFocusableNumPic()
        {
            SetStyle(ControlStyles.Selectable, false);
            SetTopLevel(false);
            
        }
    }
}
