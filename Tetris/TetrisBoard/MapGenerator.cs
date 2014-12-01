using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Tetris.TetrisBoard
{
    public class MapGenerator
    {

        public Color[,] colors = new Color[10, 20];

        public MapGenerator()
        {

        }

        public void addRow(int row, int startc, int endc, Color c)
        {
            for (int i = startc; i <= endc; ++i)
            {
                colors[i, row] = c;
            }
        }

        public void addCol(int col, int startr, int endr, Color c)
        {
            for (int i = startr; i <= endr; ++i)
            {
                colors[col, i] = c;
            }
        }

    }
}
