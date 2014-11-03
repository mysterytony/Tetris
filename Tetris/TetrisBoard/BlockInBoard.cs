using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace Tetris.TetrisBoard
{
    public class BlockInBoard : PictureBox
    {

        public Color color;
        public Block.Block_Status status;

        public BlockInBoard()
            :base()
        {
            
        }

        public void refreshColor(Color blockColor, Block.Block_Status bs)
        {
            this.color = blockColor;
            status = bs;


            if (bs == Block.Block_Status.PREVIEW)
                this.Image = Tetris.Properties.Resources.preview;
            else if (bs == Block.Block_Status.EMPTY)
                this.Image = Tetris.Properties.Resources.empty;



            switch (blockColor.Name)
            {
                case "Empty":
                    {
                        this.Image = Tetris.Properties.Resources.empty;
                        break;
                    }
                case "Red":
                    {
                        this.Image = Tetris.Properties.Resources.red;
                        break;
                    }
                case "Blue":
                    {
                        this.Image = Tetris.Properties.Resources.blue;
                        break;
                    }
                case "Yellow":
                    {
                        this.Image = Tetris.Properties.Resources.yellow;
                        break;
                    }
                case "Green":
                    {
                        this.Image = Tetris.Properties.Resources.green;
                        break;
                    }
                case "Cyan":
                    {
                        this.Image = Tetris.Properties.Resources.cyan;
                        break;
                    }
                case "Orange":
                    {
                        this.Image = Tetris.Properties.Resources.orange;
                        break;
                    }
                case "Pink":
                    {
                        this.Image = Tetris.Properties.Resources.pink;
                        break;
                    }

            }

            


        }
    }
}
