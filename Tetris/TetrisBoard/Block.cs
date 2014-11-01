using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tetris.TetrisBoard
{
    public class Block
    {
        public static enum Block_Status
        {
            VIEW,
            MOVING,
            STOPPED,
            EMPTY,
            PREVIEW,
        }

        public Color blockColor;
        public Block_Status blockStatus;





    }
}
