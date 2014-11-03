using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace Tetris.TetrisBoard
{
    public class Board : Panel
    {
        public enum boardMode
        {
            play,
            watch,
        }


        public bool isGameOver = false;

        public Label lblTime = new Label();

        public BlockInBoard[,] board;

        public Map map;

        public Map.Shape nextShape = Map.Shape.NULL;

        public PictureBox picNext = new PictureBox();
        public PictureBox picSave = new PictureBox();

        public int score, combo = 0;
        public int interval = 1000;//milisecond
        public int timecounter = 0, runningcounter = 0;
        public int totaltime = 30;//in second

        public boardMode board_mode;

        public Board()
            : base()
        {


            this.Width = 350;
            this.Height = 400;
            //boardMode bm = boardMode.play
            this.board_mode = boardMode.play;

            this.map = new Map(10, 20);
            this.board = new BlockInBoard[10, 20];



            this.nextShape = Map.Shape.NULL;


            this.displayScore(false);

            picNext.Size = new Size(80,80);
            picNext.Location = new Point(210, 50);

            picSave.Size = new Size(80, 80);
            picSave.Location = new Point(210, 140);

            lblTime.Location = new Point(210, 10);

            this.Controls.Add(lblTime);
            this.Controls.Add(picNext);
            this.Controls.Add(picSave);

            refreshMap();


        }


        public void refersh(String msg)//BlockInBoard[,] board, int s, bool isCombo)
        {
            
        }


        public void refreshMap()
        {

            for (int i = 0; i < 20; ++i)
            {
                for (int j = 0; j < 10; ++j)
                {


                    if (board[j, i] == null)
                    {
                        this.board[j, i] = new BlockInBoard();
                        this.board[j, i].refreshColor(Color.Empty, Block.Block_Status.EMPTY);
                        this.board[j, i].Size = new Size(20, 20);
                        this.board[j, i].Location = new Point(j * 20, i * 20);
                        this.Controls.Add(board[j, i]);
                        continue;
                    }
                    else if (board[j,i].status != map.getBlockStatus(j,i) || board[j,i].color != map.getBlockColor(j,i))
                    {

                        this.board[j, i].refreshColor(map.getBlockColor(j, i), map.getBlockStatus(j,i));
         
                    }

                }
            }


        }

        public void displayScore(bool isCombo)
        {

        }

        public void setScore(int rowCleaned)
        {

        }

        public void displayTime()
        {
            int leftTime = (totaltime * 1000 - timecounter)/1000;

            lblTime.Text = leftTime / 60 + ":" + (leftTime % 60 < 10 ? "0" : "") + leftTime % 60;
        }

        public void drawNextSaveShape()
        {
            picNext.Image = getShapeImg(nextShape);
            picSave.Image = getShapeImg(map.saveBlock);
        }

        public void createGameOver()
        {
            isGameOver = true;

        }

        public Bitmap getShapeImg(Map.Shape s)
        {
            switch (s)
            {
                case Map.Shape.I:
                    return Tetris.Properties.Resources.I;
                case Map.Shape.J:
                    return Tetris.Properties.Resources.J;
                case Map.Shape.L:
                    return Tetris.Properties.Resources.L;
                case Map.Shape.O:
                    return Tetris.Properties.Resources.O;
                case Map.Shape.S:
                    return Tetris.Properties.Resources.S;
                case Map.Shape.T:
                    return Tetris.Properties.Resources.T;
                case Map.Shape.Z:
                    return Tetris.Properties.Resources.Z;
                default:
                    return Tetris.Properties.Resources.Shape_Empty;
            }
        }



        /// <summary>
        /// to be called 10
        /// </summary>
        public void timeTick()
        {

            if (isGameOver)
                return;

            timecounter += 10;

            runningcounter += 10;


            if (!map.errorCheck())
            {
                if (map.currentShape != Map.Shape.NULL) 
                //				System.out.println(map.currentShape);
                map.recreateBlock(nextShape);
            }


            int rowCleaned = map.cleanOneRow();
            if (map.isFall)
                setScore(rowCleaned);
            map.cleanRow();
            //		displayScore(rowCleaned);
            bool e = true;
            if (!map.isExistMovingBlock() || map.isFall)
            {

                Random r = new Random();
                int shape = r.Next(7);
                int x = 4;



                if (nextShape != Map.Shape.NULL)
                {
                    e = map.createShape(nextShape, x);
                }


                switch (shape)
                {
                    case 0:
                        nextShape = Map.Shape.I;
                        break;
                    case 1:
                        nextShape = Map.Shape.J;
                        break;
                    case 2:
                        nextShape = Map.Shape.L;
                        break;
                    case 3:
                        nextShape = Map.Shape.O;
                        break;
                    case 4:
                        nextShape = Map.Shape.S;
                        break;
                    case 5:
                        nextShape = Map.Shape.T;
                        break;
                    case 6:
                        nextShape = Map.Shape.Z;
                        break;
                }


                map.createShape(Map.Shape.J, x);




            }

            if (!e || timecounter >= (totaltime * 1000))
            {

                createGameOver();
               


                return;
            }


            if (runningcounter >= interval)
            {
                map.run();
                runningcounter = 0;
            }


            map.drawPreview();
            refreshMap();
            drawNextSaveShape();
            displayTime();
        }


        public void keyPressed(Keys key)
        {
            if (isGameOver)
                return;


            switch (key)
            {
                case Keys.Up:
                    {
                        map.turn();
                        break;
                    }
                case Keys.Down:
                    {
                        map.run();
                        break;
                    }
                
                case Keys.Space:
                    {
                        map.fallToGround();
                        break;
                    }
                case Keys.Left:
                    {
                        map.moveLeft();
                        break;
                    }
                case Keys.Right:
                    {
                        map.moveRight();
                        break;
                    }
                case Keys.C:
                    {
                        map.swtichSave();
                        break;
                    }
                    

            }

            refreshMap();
            drawNextSaveShape();
            displayTime();


        }







    }
}
