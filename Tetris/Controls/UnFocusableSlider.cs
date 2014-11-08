using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Tetris.Controls
{
    public class UnFocusableSlider : TrackBar
    {
        public UnFocusableSlider()
        {
            SetStyle(ControlStyles.Selectable, false);
        }
    }
}
