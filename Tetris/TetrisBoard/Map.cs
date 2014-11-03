using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Tetris.TetrisBoard;

namespace Tetris
{
    public class Map
    {
        public enum Shape
        {
            /*
             * ....
             */
            I,

            /*
             * ...
             *   . 
             */
            J,

            /*
             * ...
             * .
             */
            L,

            /*
             * ..
             * ..
             */
            O,

            /*
             *  ..
             * ..
             */
            S,

            /*
             *...
             * .
             */
            T,

            /*
             * ..
             *  ..
             */
            Z,

            /*
             * empty
             */
            NULL,

        };

        public int x;
        public int y;

        public Block[,] map;

        public int stage = 0;
        public Shape currentShape = Shape.NULL;
        public int movingY = 0;
        public int movingX = 0;

        public bool isFall = false;

        public Shape saveBlock;
        public bool canSwitch = true;

        public Map(int x, int y)
        {
            this.x = x;
            this.y = y;
            map = new Block[x, y];
            for (int i = 0; i < x; ++i)
            {
                for (int j = 0; j < y; ++j)
                {
                    map[i, j] = new Block(Color.Empty, Block.Block_Status.EMPTY);
                }

            }
        }

        private Color getShapeColor(Shape s)
        {
            switch (s)
            {
                case Shape.I:
                    return Color.Yellow;
                case Shape.L:
                    return Color.Pink;
                case Shape.S:
                    return Color.Green;
                case Shape.Z:
                    return Color.Orange;
                case Shape.O:
                    return Color.Red;
                case Shape.J:
                    return Color.Blue;
                case Shape.T:
                    return Color.Cyan;
                default:
                    return Color.Empty;
            }
        }


        public void cleanMovingShape()
        {
            for (int i = 0; i < x; ++i)
            {
                for (int j = 0; j < y; ++j)
                {
                    if (map[i, j].blockStatus == Block.Block_Status.MOVING)
                        map[i, j].createEmpty();
                }
            }
            currentShape = Shape.NULL;
            stage = 0;
            movingX = 0;
            movingY = 0;
            isFall = false;
        }


        public void swtichSave()
        {
            if (canSwitch)
            {
                if (saveBlock == null)
                {
                    saveBlock = currentShape;

                    cleanMovingShape();
                }
                else
                {
                    Shape s = currentShape;
                    cleanMovingShape();
                    createShape(saveBlock, this.x / 2);
                    saveBlock = s;
                }
                canSwitch = false;
            }
        }

        public void drawPreview()
        {
            for (int j = this.y - 1; j > -1; --j)
            {
                for (int i = 0; i < this.x; ++i)
                {
                    if (map[i, j].blockStatus == Block.Block_Status.PREVIEW)
                    {
                        map[i, j].createEmpty();
                    }
                }
            }
            int counter = this.y + 1;

            for (int j = this.y - 1; j > -1; --j)
            {
                for (int i = 0; i < this.x; ++i)
                {
                    if (map[i, j].blockStatus == Block.Block_Status.MOVING)
                    {
                        int temp = getBlockHeight(i, j);
                        if (temp < counter)
                            counter = temp;
                    }
                }
            }

            for (int j = this.y - 1; j > -1; --j)
            {
                for (int i = 0; i < this.x; ++i)
                {
                    if (map[i, j].blockStatus == Block.Block_Status.MOVING
                            && map[i, j + counter].blockStatus != Block.Block_Status.MOVING)
                    {
                        map[i, j + counter].blockStatus = Block.Block_Status.PREVIEW;
                    }
                }
            }

        }

        public void recreateBlock(Shape s)
        {
            for (int i = 0; i < this.x; ++i)
            {
                for (int j = 0; j < this.y; ++j)
                {
                    if (map[i, j].blockStatus == Block.Block_Status.MOVING)
                    {
                        map[i, j].createEmpty();
                    }
                }
            }
            this.createShape(s, movingX);
        }

        public bool errorCheck()
        {
            int counter = 0;
            for (int i = 0; i < this.x; ++i)
            {
                for (int j = 0; j < this.y; ++j)
                {
                    if (map[i, j].blockStatus == Block.Block_Status.MOVING)
                        counter++;
                }
            }
            if (counter == 4 || counter == 0)
                return true;
            return false;
        }


        public bool createShape(Shape s, int x)
        {
            isFall = false;
            for (int i = 0; i < this.x; ++i)
            {
                if (map[i, 0].blockStatus == Block.Block_Status.STOPPED)//game over
                {
                    return false;
                }
            }
            if (isExistMovingBlock())//no shape is allowed to create while a moving shape exist
                return true;

            this.currentShape = s;
            stage = 0;
            movingY = 0;
            movingX = x;
            switch (s)
            {
                case Shape.I:// ....
                    {
                        if (x > this.x - 4)
                        {
                            return false;
                        }
                        //					map[x,0].blockStatus = Block_Status.MOVING;
                        //					map[x + 1,0].blockStatus = Block_Status.MOVING;
                        //					map[x + 2,0].blockStatus = Block_Status.MOVING;
                        //					map[x + 3,0].blockStatus = Block_Status.MOVING;
                        map[x, 0].setStatus(Block.Block_Status.MOVING, getShapeColor(currentShape));
                        map[x + 1, 0].setStatus(Block.Block_Status.MOVING, getShapeColor(currentShape));
                        map[x + 2, 0].setStatus(Block.Block_Status.MOVING, getShapeColor(currentShape));
                        map[x + 3, 0].setStatus(Block.Block_Status.MOVING, getShapeColor(currentShape));
                        return true;
                    }
                case Shape.J:
                    {
                        if (x > this.x - 3)
                        {
                            return false;
                        }
                        map[x, 0].setStatus(Block.Block_Status.MOVING, getShapeColor(currentShape));
                        map[x + 1, 0].setStatus(Block.Block_Status.MOVING, getShapeColor(currentShape));
                        map[x + 2, 0].setStatus(Block.Block_Status.MOVING, getShapeColor(currentShape));
                        map[x + 2, 1].setStatus(Block.Block_Status.MOVING, getShapeColor(currentShape));
                        return true;
                    }
                case Shape.L:
                    {
                        if (x > this.x - 3)
                        {
                            return false;
                        }
                        map[x, 0].setStatus(Block.Block_Status.MOVING, getShapeColor(currentShape));
                        map[x + 1, 0].setStatus(Block.Block_Status.MOVING, getShapeColor(currentShape));
                        map[x + 2, 0].setStatus(Block.Block_Status.MOVING, getShapeColor(currentShape));
                        map[x, 1].setStatus(Block.Block_Status.MOVING, getShapeColor(currentShape));
                        return true;
                    }
                case Shape.O:
                    {
                        if (x > this.x - 2)
                        {
                            return false;
                        }
                        map[x, 0].setStatus(Block.Block_Status.MOVING, getShapeColor(currentShape));
                        map[x + 1, 0].setStatus(Block.Block_Status.MOVING, getShapeColor(currentShape));
                        map[x, 1].setStatus(Block.Block_Status.MOVING, getShapeColor(currentShape));
                        map[x + 1, 1].setStatus(Block.Block_Status.MOVING, getShapeColor(currentShape));
                        return true;
                    }
                case Shape.S:
                    {
                        if (x > this.x - 3)
                        {
                            return false;
                        }
                        map[x, 1].setStatus(Block.Block_Status.MOVING, getShapeColor(currentShape));
                        map[x + 1, 0].setStatus(Block.Block_Status.MOVING, getShapeColor(currentShape));
                        map[x + 1, 1].setStatus(Block.Block_Status.MOVING, getShapeColor(currentShape));
                        map[x + 2, 0].setStatus(Block.Block_Status.MOVING, getShapeColor(currentShape));
                        return true;
                    }
                case Shape.T:
                    {
                        if (x > this.x - 3)
                        {
                            return false;
                        }
                        map[x, 0].setStatus(Block.Block_Status.MOVING, getShapeColor(currentShape));
                        map[x + 1, 0].setStatus(Block.Block_Status.MOVING, getShapeColor(currentShape));
                        map[x + 2, 0].setStatus(Block.Block_Status.MOVING, getShapeColor(currentShape));
                        map[x + 1, 1].setStatus(Block.Block_Status.MOVING, getShapeColor(currentShape));
                        return true;
                    }
                case Shape.Z:
                    {
                        if (x > this.x - 3)
                        {
                            return false;
                        }
                        map[x, 0].setStatus(Block.Block_Status.MOVING, getShapeColor(currentShape));
                        map[x + 1, 0].setStatus(Block.Block_Status.MOVING, getShapeColor(currentShape));
                        map[x + 1, 1].setStatus(Block.Block_Status.MOVING, getShapeColor(currentShape));
                        map[x + 2, 1].setStatus(Block.Block_Status.MOVING, getShapeColor(currentShape));
                        return true;
                    }
                default:
                    {
                        return false;
                    }
            }
        }


        public bool turn()
        {
            stage++;
            switch (currentShape)
            {
                case Shape.I:
                    {
                        if (stage == 1)
                        {
                            if (movingY <= (this.y - 4))
                                if (map[movingX, movingY + 1].blockStatus != Block.Block_Status.STOPPED
                                        && map[movingX, movingY + 2].blockStatus != Block.Block_Status.STOPPED
                                        && map[movingX, movingY + 3].blockStatus != Block.Block_Status.STOPPED)
                                {
                                    map[movingX, movingY + 1].setStatus(Block.Block_Status.MOVING, getShapeColor(currentShape));
                                    map[movingX, movingY + 2].setStatus(Block.Block_Status.MOVING, getShapeColor(currentShape));
                                    map[movingX, movingY + 3].setStatus(Block.Block_Status.MOVING, getShapeColor(currentShape));

                                    map[movingX + 1, movingY].createEmpty();
                                    map[movingX + 2, movingY].createEmpty();
                                    map[movingX + 3, movingY].createEmpty();
                                    return true;
                                }

                            if (movingY > (this.y - 4))
                            {
                                break;
                            }
                            if (map[movingX + 3, movingY + 1].blockStatus != Block.Block_Status.STOPPED
                                    && map[movingX + 3, movingY + 2].blockStatus != Block.Block_Status.STOPPED
                                    && map[movingX + 3, movingY + 3].blockStatus != Block.Block_Status.STOPPED)
                            {
                                map[movingX + 3, movingY + 1].setStatus(Block.Block_Status.MOVING, getShapeColor(currentShape));
                                map[movingX + 3, movingY + 2].setStatus(Block.Block_Status.MOVING, getShapeColor(currentShape));
                                map[movingX + 3, movingY + 3].setStatus(Block.Block_Status.MOVING, getShapeColor(currentShape));

                                map[movingX, movingY].createEmpty();
                                map[movingX + 1, movingY].createEmpty();
                                map[movingX + 2, movingY].createEmpty();
                                movingX += 3;
                                return true;
                            }
                            else
                                break;

                        }
                        else if (stage == 2)
                        {

                            if (movingX <= (this.x - 4))

                                if (map[movingX + 1, movingY].blockStatus != Block.Block_Status.STOPPED
                                        && map[movingX + 2, movingY].blockStatus != Block.Block_Status.STOPPED
                                        && map[movingX + 3, movingY].blockStatus != Block.Block_Status.STOPPED)
                                {
                                    map[movingX + 1, movingY].setStatus(Block.Block_Status.MOVING, getShapeColor(currentShape));
                                    map[movingX + 2, movingY].setStatus(Block.Block_Status.MOVING, getShapeColor(currentShape));
                                    map[movingX + 3, movingY].setStatus(Block.Block_Status.MOVING, getShapeColor(currentShape));

                                    map[movingX, movingY + 1].createEmpty();
                                    map[movingX, movingY + 2].createEmpty();
                                    map[movingX, movingY + 3].createEmpty();
                                    stage = 0;
                                    return true;
                                }

                            if (movingX < 4)// not enough space
                            {
                                break;
                            }
                            if (map[movingX - 1, movingY].blockStatus != Block.Block_Status.STOPPED
                                    && map[movingX - 2, movingY].blockStatus != Block.Block_Status.STOPPED
                                    && map[movingX - 3, movingY].blockStatus != Block.Block_Status.STOPPED)
                            {
                                map[movingX - 1, movingY].setStatus(Block.Block_Status.MOVING, getShapeColor(currentShape));
                                map[movingX - 2, movingY].setStatus(Block.Block_Status.MOVING, getShapeColor(currentShape));
                                map[movingX - 3, movingY].setStatus(Block.Block_Status.MOVING, getShapeColor(currentShape));

                                map[movingX, movingY + 1].createEmpty();
                                map[movingX, movingY + 2].createEmpty();
                                map[movingX, movingY + 3].createEmpty();
                                stage = 0;
                                movingX -= 3;
                                return true;
                            }
                            else
                                break;

                        }
                        else
                            break;
                    }

                case Shape.O:
                    {
                        break;
                    }

                case Shape.Z:
                    {
                        if (stage == 1)
                        {
                            if (movingY > (this.y - 3))
                            {
                                break;
                            }
                            if (map[movingX, movingY + 1].blockStatus != Block.Block_Status.STOPPED
                                    && map[movingX, movingY + 2].blockStatus != Block.Block_Status.STOPPED)
                            {
                                map[movingX, movingY + 2].setStatus(Block.Block_Status.MOVING, getShapeColor(currentShape));
                                map[movingX, movingY + 1].setStatus(Block.Block_Status.MOVING, getShapeColor(currentShape));

                                map[movingX, movingY].createEmpty();
                                map[movingX + 2, movingY + 1].createEmpty();
                                return true;
                            }
                            else
                                break;
                        }
                        else if (stage == 2)
                        {
                            if (movingX <= (this.x - 3))

                                if (map[movingX, movingY].blockStatus != Block.Block_Status.STOPPED
                                        && map[movingX + 2, movingY + 1].blockStatus != Block.Block_Status.STOPPED)
                                {
                                    map[movingX, movingY].setStatus(Block.Block_Status.MOVING, getShapeColor(currentShape));
                                    map[movingX + 2, movingY + 1].setStatus(Block.Block_Status.MOVING, getShapeColor(currentShape));

                                    map[movingX, movingY + 2].createEmpty();
                                    map[movingX, movingY + 1].createEmpty();
                                    stage = 0;
                                    return true;
                                }

                            if (movingX < 0)
                                break;

                            if (map[movingX, movingY].blockStatus != Block.Block_Status.STOPPED
                                    && map[movingX - 1, movingY].blockStatus != Block.Block_Status.STOPPED)
                            {
                                map[movingX, movingY].setStatus(Block.Block_Status.MOVING, getShapeColor(currentShape));
                                map[movingX - 1, movingY].setStatus(Block.Block_Status.MOVING, getShapeColor(currentShape));

                                map[movingX + 1, movingY].createEmpty();
                                map[movingX, movingY + 2].createEmpty();
                                stage = 0;
                                movingX--;
                                return true;
                            }
                            else break;
                        }

                        break;
                    }

                case Shape.S:
                    {
                        if (stage == 1)
                        {
                            if (movingY < (this.y - 3))

                                if (map[movingX, movingY].blockStatus != Block.Block_Status.STOPPED
                                        && map[movingX + 1, movingY + 2].blockStatus != Block.Block_Status.STOPPED)
                                {
                                    map[movingX + 1, movingY + 2].setStatus(Block.Block_Status.MOVING, getShapeColor(currentShape));
                                    map[movingX, movingY].setStatus(Block.Block_Status.MOVING, getShapeColor(currentShape));

                                    map[movingX + 1, movingY].createEmpty();
                                    map[movingX + 2, movingY].createEmpty();
                                    return true;
                                }
                                else
                                    break;
                        }
                        else if (stage == 2)
                        {

                            if (movingX <= (this.x - 3))

                                if (map[movingX + 2, movingY].blockStatus != Block.Block_Status.STOPPED
                                        && map[movingX + 1, movingY].blockStatus != Block.Block_Status.STOPPED)
                                {
                                    map[movingX + 1, movingY].setStatus(Block.Block_Status.MOVING, getShapeColor(currentShape));
                                    map[movingX + 2, movingY].setStatus(Block.Block_Status.MOVING, getShapeColor(currentShape));

                                    map[movingX, movingY].createEmpty();
                                    map[movingX + 1, movingY + 2].createEmpty();
                                    stage = 0;
                                    return true;
                                }

                            if (movingX < 0)
                                break;
                            if (map[movingX - 1, movingY + 1].blockStatus != Block.Block_Status.STOPPED
                                    && map[movingX + 1, movingY].blockStatus != Block.Block_Status.STOPPED)
                            {
                                map[movingX + 1, movingY].setStatus(Block.Block_Status.MOVING, getShapeColor(currentShape));
                                map[movingX - 1, movingY + 1].setStatus(Block.Block_Status.MOVING, getShapeColor(currentShape));

                                map[movingX + 1, movingY + 1].createEmpty();
                                map[movingX + 1, movingY + 2].createEmpty();
                                stage = 0;
                                movingX--;
                                return true;
                            }
                            else
                                break;


                        }
                        break;
                    }

                case Shape.T:
                    {
                        if (stage == 1)
                        {
                            if (movingY > (this.y - 3))
                            {
                                break;
                            }
                            if (map[movingX, movingY + 1].blockStatus != Block.Block_Status.STOPPED
                                    && map[movingX + 1, movingY + 2].blockStatus != Block.Block_Status.STOPPED)
                            {
                                map[movingX + 1, movingY + 2].setStatus(Block.Block_Status.MOVING, getShapeColor(currentShape));
                                map[movingX, movingY + 1].setStatus(Block.Block_Status.MOVING, getShapeColor(currentShape));

                                map[movingX, movingY].createEmpty();
                                map[movingX + 2, movingY].createEmpty();
                                return true;
                            }
                            else
                                break;
                        }
                        else if (stage == 2)
                        {
                            if (movingX <= (this.x - 3))

                                if (map[movingX + 2, movingY + 1].blockStatus != Block.Block_Status.STOPPED)
                                {
                                    map[movingX + 2, movingY + 1].setStatus(Block.Block_Status.MOVING, getShapeColor(currentShape));

                                    map[movingX + 1, movingY + 2].createEmpty();
                                    return true;
                                }

                            if (movingX < 0)
                                break;
                            if (map[movingX, movingY].blockStatus != Block.Block_Status.STOPPED &&
                                    map[movingX - 1, movingY + 1].blockStatus != Block.Block_Status.STOPPED)
                            {
                                map[movingX, movingY].setStatus(Block.Block_Status.MOVING, getShapeColor(currentShape));
                                map[movingX - 1, movingY + 1].setStatus(Block.Block_Status.MOVING, getShapeColor(currentShape));

                                map[movingX + 1, movingY].createEmpty();
                                map[movingX + 1, movingY + 2].createEmpty();
                                movingX--;
                                return true;
                            }
                            else break;

                        }
                        else if (stage == 3)
                        {
                            if (movingY > (this.y - 3))
                            {
                                break;
                            }
                            if (map[movingX, movingY].blockStatus != Block.Block_Status.STOPPED
                                    && map[movingX, movingY + 2].blockStatus != Block.Block_Status.STOPPED)
                            {
                                map[movingX, movingY].setStatus(Block.Block_Status.MOVING, getShapeColor(currentShape));
                                map[movingX, movingY + 2].setStatus(Block.Block_Status.MOVING, getShapeColor(currentShape));

                                map[movingX + 1, movingY].createEmpty();
                                map[movingX + 2, movingY + 1].createEmpty();
                                return true;
                            }
                            else
                                break;
                        }
                        else if (stage == 4)
                        {
                            if (movingX <= (this.x - 3))

                                if (map[movingX + 1, movingY].blockStatus != Block.Block_Status.STOPPED
                                        && map[movingX + 2, movingY].blockStatus != Block.Block_Status.STOPPED)
                                {
                                    map[movingX + 1, movingY].setStatus(Block.Block_Status.MOVING, getShapeColor(currentShape));
                                    map[movingX + 2, movingY].setStatus(Block.Block_Status.MOVING, getShapeColor(currentShape));

                                    map[movingX, movingY + 2].createEmpty();
                                    map[movingX, movingY + 1].createEmpty();
                                    stage = 0;
                                    return true;
                                }

                            if (movingX < 0)
                                break;
                            if (map[movingX + 1, movingY].blockStatus != Block.Block_Status.STOPPED &&
                                    map[movingX - 1, movingY].blockStatus != Block.Block_Status.STOPPED)
                            {
                                map[movingX - 1, movingY].setStatus(Block.Block_Status.MOVING, getShapeColor(currentShape));
                                map[movingX + 1, movingY].setStatus(Block.Block_Status.MOVING, getShapeColor(currentShape));

                                map[movingX, movingY + 2].createEmpty();
                                map[movingX + 1, movingY + 1].createEmpty();

                                stage = 0;
                                movingX--;
                                return true;
                            }
                            else break;
                        }
                        break;
                    }

                case Shape.J:
                    {
                        if (stage == 1)
                        {
                            if (movingY > (this.y - 3))
                            {
                                break;
                            }
                            if (map[movingX + 1, movingY + 1].blockStatus != Block.Block_Status.STOPPED
                                    && map[movingX + 1, movingY + 2].blockStatus != Block.Block_Status.STOPPED
                                    && map[movingX, movingY + 2].blockStatus != Block.Block_Status.STOPPED)
                            {
                                map[movingX + 1, movingY + 1].setStatus(Block.Block_Status.MOVING, getShapeColor(currentShape));
                                map[movingX + 1, movingY + 2].setStatus(Block.Block_Status.MOVING, getShapeColor(currentShape));
                                map[movingX, movingY + 2].setStatus(Block.Block_Status.MOVING, getShapeColor(currentShape));

                                map[movingX, movingY].createEmpty();
                                map[movingX + 2, movingY].createEmpty();
                                map[movingX + 2, movingY + 1].createEmpty();
                                return true;
                            }
                            else
                                break;

                        }
                        else if (stage == 2)
                        {
                            if (movingX <= (this.x - 3))

                                if (map[movingX, movingY].blockStatus != Block.Block_Status.STOPPED
                                        && map[movingX, movingY + 1].blockStatus != Block.Block_Status.STOPPED
                                        && map[movingX + 2, movingY + 1].blockStatus != Block.Block_Status.STOPPED)
                                {
                                    map[movingX, movingY].setStatus(Block.Block_Status.MOVING, getShapeColor(currentShape));
                                    map[movingX, movingY + 1].setStatus(Block.Block_Status.MOVING, getShapeColor(currentShape));
                                    map[movingX + 2, movingY + 1].setStatus(Block.Block_Status.MOVING, getShapeColor(currentShape));

                                    map[movingX + 1, movingY].createEmpty();
                                    map[movingX + 1, movingY + 2].createEmpty();
                                    map[movingX, movingY + 2].createEmpty();
                                    return true;
                                }

                            if (movingX < 0)
                                break;

                            if (map[movingX - 1, movingY].blockStatus != Block.Block_Status.STOPPED
                                    && map[movingX - 1, movingY + 1].blockStatus != Block.Block_Status.STOPPED
                                    && map[movingX, movingY + 1].blockStatus != Block.Block_Status.STOPPED)
                            {
                                map[movingX - 1, movingY].setStatus(Block.Block_Status.MOVING, getShapeColor(currentShape));
                                map[movingX - 1, movingY + 1].setStatus(Block.Block_Status.MOVING, getShapeColor(currentShape));
                                map[movingX, movingY + 1].setStatus(Block.Block_Status.MOVING, getShapeColor(currentShape));

                                map[movingX + 1, movingY].createEmpty();
                                map[movingX + 1, movingY + 2].createEmpty();
                                map[movingX, movingY + 2].createEmpty();
                                movingX--;
                                return true;
                            }
                            else break;
                        }

                        else if (stage == 3)
                        {
                            if (movingY > (this.y - 3))
                            {
                                break;
                            }
                            if (map[movingX + 1, movingY].blockStatus != Block.Block_Status.STOPPED
                                    && map[movingX, movingY + 2].blockStatus != Block.Block_Status.STOPPED)
                            {
                                map[movingX + 1, movingY].setStatus(Block.Block_Status.MOVING, getShapeColor(currentShape));
                                map[movingX, movingY + 2].setStatus(Block.Block_Status.MOVING, getShapeColor(currentShape));

                                map[movingX + 1, movingY + 1].createEmpty();
                                map[movingX + 2, movingY + 1].createEmpty();
                                return true;
                            }
                            else
                                break;
                        }
                        else if (stage == 4)
                        {
                            if (movingX <= (this.x - 3))

                                if (map[movingX + 2, movingY].blockStatus != Block.Block_Status.STOPPED
                                        && map[movingX + 2, movingY + 1].blockStatus != Block.Block_Status.STOPPED)
                                {
                                    map[movingX + 2, movingY].setStatus(Block.Block_Status.MOVING, getShapeColor(currentShape));
                                    map[movingX + 2, movingY + 1].setStatus(Block.Block_Status.MOVING, getShapeColor(currentShape));

                                    map[movingX, movingY + 1].createEmpty();
                                    map[movingX, movingY + 2].createEmpty();
                                    stage = 0;
                                    return true;
                                }

                            if (movingX < 0)
                                break;

                            if (map[movingX - 1, movingY].blockStatus != Block.Block_Status.STOPPED
                                    && map[movingX + 1, movingY + 1].blockStatus != Block.Block_Status.STOPPED)
                            {
                                map[movingX - 1, movingY].setStatus(Block.Block_Status.MOVING, getShapeColor(currentShape));
                                map[movingX + 1, movingY + 1].setStatus(Block.Block_Status.MOVING, getShapeColor(currentShape));

                                map[movingX, movingY + 1].createEmpty();
                                map[movingX, movingY + 2].createEmpty();
                                stage = 0;
                                movingX--;
                                return true;
                            }
                            else break;
                        }

                        break;
                    }

                case Shape.L:
                    {
                        if (stage == 1)
                        {
                            if (movingY > (this.y - 3))
                            {
                                break;
                            }
                            if (map[movingX + 1, movingY + 1].blockStatus != Block.Block_Status.STOPPED
                                    && map[movingX + 1, movingY + 2].blockStatus != Block.Block_Status.STOPPED)
                            {
                                map[movingX + 1, movingY + 1].setStatus(Block.Block_Status.MOVING, getShapeColor(currentShape));
                                map[movingX + 1, movingY + 2].setStatus(Block.Block_Status.MOVING, getShapeColor(currentShape));

                                map[movingX, movingY + 1].createEmpty();
                                map[movingX + 2, movingY].createEmpty();
                                return true;
                            }
                            else
                                break;
                        }
                        else if (stage == 2)
                        {
                            if (movingX <= (this.x - 3))

                                if (map[movingX, movingY + 1].blockStatus != Block.Block_Status.STOPPED
                                        && map[movingX + 2, movingY].blockStatus != Block.Block_Status.STOPPED
                                        && map[movingX + 2, movingY + 1].blockStatus != Block.Block_Status.STOPPED)
                                {
                                    map[movingX, movingY + 1].setStatus(Block.Block_Status.MOVING, getShapeColor(currentShape));
                                    map[movingX + 2, movingY + 1].setStatus(Block.Block_Status.MOVING, getShapeColor(currentShape));
                                    map[movingX + 2, movingY].setStatus(Block.Block_Status.MOVING, getShapeColor(currentShape));

                                    map[movingX, movingY].createEmpty();
                                    map[movingX + 1, movingY].createEmpty();
                                    map[movingX + 1, movingY + 2].createEmpty();

                                    return true;
                                }

                            if (movingX < 0)
                                break;
                            if (map[movingX, movingY + 1].blockStatus != Block.Block_Status.STOPPED
                                    && map[movingX - 1, movingY + 1].blockStatus != Block.Block_Status.STOPPED)
                            {
                                map[movingX, movingY + 1].setStatus(Block.Block_Status.MOVING, getShapeColor(currentShape));
                                map[movingX - 1, movingY + 1].setStatus(Block.Block_Status.MOVING, getShapeColor(currentShape));

                                map[movingX, movingY].createEmpty();
                                map[movingX + 1, movingY + 2].createEmpty();

                                movingX--;
                                return true;
                            }

                        }

                        else if (stage == 3)
                        {
                            if (movingY > (this.y - 3))
                            {
                                break;
                            }
                            if (map[movingX, movingY].blockStatus != Block.Block_Status.STOPPED
                                    && map[movingX, movingY + 2].blockStatus != Block.Block_Status.STOPPED
                                    && map[movingX + 1, movingY + 2].blockStatus != Block.Block_Status.STOPPED)
                            {
                                map[movingX, movingY].setStatus(Block.Block_Status.MOVING, getShapeColor(currentShape));
                                map[movingX, movingY + 2].setStatus(Block.Block_Status.MOVING, getShapeColor(currentShape));
                                map[movingX + 1, movingY + 2].setStatus(Block.Block_Status.MOVING, getShapeColor(currentShape));

                                map[movingX + 2, movingY].createEmpty();
                                map[movingX + 2, movingY + 1].createEmpty();
                                map[movingX + 1, movingY + 1].createEmpty();

                                return true;
                            }
                            else
                                break;
                        }

                        else if (stage == 4)
                        {
                            if (movingX <= (this.x - 3))

                                if (map[movingX + 2, movingY].blockStatus != Block.Block_Status.STOPPED
                                        && map[movingX + 1, movingY].blockStatus != Block.Block_Status.STOPPED)
                                {
                                    map[movingX + 2, movingY].setStatus(Block.Block_Status.MOVING, getShapeColor(currentShape));
                                    map[movingX + 1, movingY].setStatus(Block.Block_Status.MOVING, getShapeColor(currentShape));

                                    map[movingX, movingY + 2].createEmpty();
                                    map[movingX + 1, movingY + 2].createEmpty();
                                    stage = 0;

                                    return true;
                                }

                            if (movingX < 0)
                                break;

                            if (map[movingX + 1, movingY].blockStatus != Block.Block_Status.STOPPED
                                    && map[movingX - 1, movingY].blockStatus != Block.Block_Status.STOPPED
                                    && map[movingX - 1, movingY + 1].blockStatus != Block.Block_Status.STOPPED)
                            {
                                map[movingX + 1, movingY].setStatus(Block.Block_Status.MOVING, getShapeColor(currentShape));
                                map[movingX - 1, movingY].setStatus(Block.Block_Status.MOVING, getShapeColor(currentShape));
                                map[movingX - 1, movingY + 1].setStatus(Block.Block_Status.MOVING, getShapeColor(currentShape));

                                map[movingX, movingY + 1].createEmpty();
                                map[movingX, movingY + 2].createEmpty();
                                map[movingX + 1, movingY + 2].createEmpty();

                                movingX--;
                                stage = 0;

                                return true;
                            }
                        }
                        break;
                    }
            }
            stage--;
            return false;
        }



        public bool isExistMovingBlock()
        {
            for (int i = 0; i < this.x; ++i)
            {
                for (int j = 0; j < this.y; ++j)
                {
                    if (map[i, j].blockStatus == Block.Block_Status.MOVING)
                        return true;
                }
            }
            return false;
        }


        public void run()
        {
            int counter = this.y + 1;

            for (int j = this.y - 1; j > -1; --j)
            {
                for (int i = 0; i < this.x; ++i)
                {
                    if (map[i, j].blockStatus == Block.Block_Status.MOVING)
                    {
                        int temp = getBlockHeight(i, j);
                        if (temp < counter)
                            counter = temp;
                    }
                }
            }
            if (counter > 0)
            {
                movingY++;
                for (int i = 0; i < this.x; ++i)
                {
                    for (int j = this.y - 1; j > -1; --j)
                    {
                        if (map[i, j].blockStatus == Block.Block_Status.MOVING)
                        {
                            map[i, j + 1].setStatus(Block.Block_Status.MOVING, getShapeColor(currentShape));
                            map[i, j].createEmpty();
                        }
                    }
                }
                isFall = false;

            }
            else if (counter <= 0)
            {
                for (int i = 0; i < this.x; ++i)
                {
                    for (int j = this.y - 1; j > -1; --j)
                    {
                        if (map[i, j].blockStatus == Block.Block_Status.MOVING)
                        {
                            map[i, j].setStatus(Block.Block_Status.STOPPED);
                        }
                    }
                }
                movingY = 0;
                movingX = 0;
                isFall = true;
                canSwitch = true;
            }
        }


        public bool moveLeft()
        {
            int counter = this.y + 1;
            for (int i = 0; i < this.x; ++i)
            {
                for (int j = this.y - 1; j > -1; --j)
                {
                    if (map[i, j].blockStatus == Block.Block_Status.MOVING)
                    {
                        int temp = getBlockMoveLeftSpace(i, j);
                        if (temp < counter)
                            counter = temp;
                    }
                }
            }
            if (counter > 0)
            {
                for (int j = this.y - 1; j > -1; --j)
                {
                    for (int i = 0; i < this.x; ++i)
                    {
                        if (map[i, j].blockStatus == Block.Block_Status.MOVING)
                        {
                            map[i - 1, j].setStatus(Block.Block_Status.MOVING, getShapeColor(currentShape));
                            map[i, j].createEmpty();
                        }
                    }
                }
            }
            else
                return false;
            movingX--;
            return true;
        }


        private int getBlockMoveLeftSpace(int x, int y)
        {
            // int counter = 0;
            // for (int i = this.x - 1; i > -1; --i) {
            // if (map[i,y].blockStatus == Block_Status.STOPPED)
            // break;
            // else
            // counter++;
            //
            // }
            // counter = this.x - (this.x - counter);
            // return counter - (this.x - x);

            int counter = 0;
            for (int i = 0; i < this.x; ++i)
            {
                if (map[i, y].blockStatus == Block.Block_Status.STOPPED)
                    counter = 0;
                else if (map[i, y].blockStatus == Block.Block_Status.MOVING)
                    break;
                else
                    counter++;
            }
            return counter;
        }


        public bool moveRight()
        {
            int counter = this.y + 1;
            for (int i = 0; i < this.x; ++i)
            {
                for (int j = 0; j < this.y; ++j)
                {
                    if (map[i, j].blockStatus == Block.Block_Status.MOVING)
                    {
                        int temp = getBlockMoveRightSpace(i, j);
                        if (temp < counter)
                            counter = temp;
                    }
                }
            }
            if (counter > 0)
            {
                for (int j = this.y - 1; j > -1; --j)
                {
                    for (int i = this.x - 1; i > -1; --i)
                    {
                        if (map[i, j].blockStatus == Block.Block_Status.MOVING)
                        {
                            map[i + 1, j].setStatus(Block.Block_Status.MOVING, getShapeColor(currentShape));
                            map[i, j].createEmpty();
                        }
                    }
                }
            }
            else
                return false;
            movingX++;
            return true;
        }



        public int getBlockMoveRightSpace(int x, int y)
        {
            // int counter = 0;
            // for (int i = 0; i < this.x; ++i) {
            // if (map[i,y].blockStatus == Block_Status.STOPPED)
            // break;
            // else
            // counter++;
            //
            // }
            // counter = this.x - (this.x - counter);
            // return counter - (x) - 1;

            int counter = 0;
            for (int i = this.x - 1; i > -1; --i)
            {
                if (map[i, y].blockStatus == Block.Block_Status.STOPPED)
                    counter = 0;
                else if (map[i, y].blockStatus == Block.Block_Status.MOVING)
                    break;
                else
                    counter++;
            }
            return counter;

        }


        private int getBlockHeight(int x, int y)
        {
            // int counter = 0;
            // for (int i = 0; i < this.y; ++i) {
            // if (map[x,i].blockStatus == Block_Status.STOPPED)
            // break;
            // else
            // counter++;
            //
            // }
            // counter = this.y - (this.y - counter);
            // return counter - y - 1;

            int counter = 0;
            for (int i = this.y - 1; i > -1; --i)
            {
                if (map[x, i].blockStatus == Block.Block_Status.STOPPED)
                    counter = 0;
                else if (map[x, i].blockStatus == Block.Block_Status.MOVING)
                    break;
                else
                    counter++;
            }
            return counter;
        }


        public int cleanOneRow()
        {
            int rowCleaned = 0;
            for (int i = this.y - 1; i > -1; --i)
            {
                int counter = 0;
                for (int j = 0; j < this.x; ++j)
                {
                    if (map[j, i].blockStatus == Block.Block_Status.STOPPED)
                        counter++;
                }
                if (counter == this.x)
                {
                    rowCleaned++;

                }
            }

            return rowCleaned;
        }



        public void cleanRow()
        {
            for (int i = this.y - 1; i > -1; --i)
            {
                int counter = 0;
                for (int j = 0; j < this.x; ++j)
                {
                    if (map[j, i].blockStatus == Block.Block_Status.STOPPED)
                        counter++;
                }
                if (counter == this.x)
                {
                    for (int k = 0; k < this.x; ++k)
                    {
                        map[k, i].createEmpty();
                    }

                    for (int j = i - 1; j > -1; --j)
                    {
                        for (int k = 0; k < this.x; ++k)
                        {
                            if (map[k, j].blockStatus == Block.Block_Status.STOPPED)
                            {

                                map[k, j + 1].setStatus(map[k, j].blockStatus, map[k, j].blockColor);
                                map[k, j].createEmpty();
                            }
                        }
                    }
                }
            }

        }



        public void fallToGround()
        {
            int counter = this.y + 1;

            for (int j = this.y - 1; j > -1; --j)
            {
                for (int i = 0; i < this.x; ++i)
                {
                    if (map[i, j].blockStatus == Block.Block_Status.MOVING)
                    {
                        int temp = getBlockHeight(i, j);
                        if (temp < counter)
                            counter = temp;
                    }
                }
            }
            if (counter > 0)
            {
                for (int j = this.y - 1; j > -1; --j)
                {
                    for (int i = 0; i < this.x; ++i)
                    {
                        if (map[i, j].blockStatus == Block.Block_Status.MOVING)
                        {
                            map[i, j + counter].setStatus(Block.Block_Status.STOPPED, getShapeColor(currentShape));
                            map[i, j].createEmpty();
                        }
                    }
                }
            }
            isFall = true;

            canSwitch = true;
        }


        public Block.Block_Status getBlockStatus(int y, int x)
        {

            return map[y,x].blockStatus;

        }

        public Color getBlockColor(int y, int x)
        {
            return map[y,x].blockColor;
        }


    }
}
