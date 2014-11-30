using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace Tetris.TetrisBoard
{
    public class Block
    {
        public enum Block_Status
        {
            VIEW,
            MOVING,
            STOPPED,
            EMPTY,
            PREVIEW,
        }

        

        public Color blockColor = Color.Empty;
        public Block_Status blockStatus;

        /// <summary>
        /// if the block status is view, it wont change, otherwise change to empty
        /// </summary>
        public void createEmpty()
        {
            this.blockColor = Color.Empty;
            this.blockStatus = (blockStatus == Block_Status.VIEW) ? Block_Status.VIEW : Block_Status.EMPTY;
        }

        public String getColorName()
        {
            return blockColor.ToString();
        }

        public bool setStatus(Block_Status bs)
        {
            this.blockStatus = blockStatus == Block_Status.VIEW ? Block_Status.VIEW : bs;

            return blockStatus != Block_Status.VIEW;
        }

        public bool setStatus(Block_Status bs, Color bc)
        {
            this.blockColor = bc;

            this.blockStatus = blockStatus == Block_Status.VIEW ? Block_Status.VIEW : bs;
            return blockStatus != Block_Status.VIEW;
        }

        public void setColor(Color bc)
        {
            this.blockColor = bc;
        }


        //public void refreshColor()
        //{


        //    switch (blockColor.Name)
        //    {
        //        case "empty":
        //            {
        //                this.Image = Tetris.Properties.Resources.empty;
        //                break;
        //            }
        //        case "red":
        //            {
        //                this.Image = Tetris.Properties.Resources.red;
        //                break;
        //            }
        //        case "blue":
        //            {
        //                this.Image = Tetris.Properties.Resources.blue;
        //                break;
        //            }
        //        case "yellow":
        //            {
        //                this.Image = Tetris.Properties.Resources.yellow;
        //                break;
        //            }
        //        case "green":
        //            {
        //                this.Image = Tetris.Properties.Resources.green;
        //                break;
        //            }
        //        case "cyan":
        //            {
        //                this.Image = Tetris.Properties.Resources.cyan;
        //                break;
        //            }
        //        case "orange":
        //            {
        //                this.Image = Tetris.Properties.Resources.orange;
        //                break;
        //            }
        //        case "pink":
        //            {
        //                this.Image = Tetris.Properties.Resources.pink;
        //                break;
        //            }

        //    }

        //    if (blockStatus == Block_Status.PREVIEW)
        //        this.Image = Tetris.Properties.Resources.preview;
        //    else if (blockStatus == Block_Status.EMPTY)
        //        this.Image = Tetris.Properties.Resources.empty;

        //}

        public Block(Color c, Block_Status b)
        {
            this.blockColor = c;
            this.blockStatus = b;


        }



    }
}
