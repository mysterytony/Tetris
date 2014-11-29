using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Input;
using System.Runtime.InteropServices;

namespace Tetris
{
    public partial class frmMultiplayer : Form
    {
        int keyLeftTimeCounter = 0, keyRightTimeCounter;



        /// <summary>
        /// indicate wheather the key is pressed
        /// </summary>
        public bool upKey = false,
            leftKey = false,
            rightKey = false;






        public frmMultiplayer(int timelimit, string ip)
        {
            InitializeComponent();

          
             boardMain.totaltime = timelimit;
             this.Text = "Tetris - Multiplayer Mode";
            

            
        }



        private void timKeyDetector_Tick(object sender, EventArgs e)
        {

            leftKey = Keyboard.IsKeyDown(Keys.Left);
            rightKey = Keyboard.IsKeyDown(Keys.Right);

            if (leftKey)
            {
                keyLeftTimeCounter++;
            }
            if (rightKey)
            {
                keyRightTimeCounter++;
            }


        }

        private void timTicker_Tick(object sender, EventArgs e)
        {
            boardMain.timeTick();

            if (boardMain.isGameOver)
            {
                this.timKeyDetector.Enabled = false;
                this.timKeyHold.Enabled = false;
                this.timTicker.Enabled = false;

                MessageBox.Show("Game Over!!\nFinal score: " + boardMain.score);
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
        }

        private void frmGamePlay_Load(object sender, EventArgs e)
        {

        }

        private void frmGamePlay_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {



            if ((e.KeyCode == Keys.Left && keyLeftTimeCounter > 1) || (e.KeyCode == Keys.Right && keyRightTimeCounter > 1))
                return;

            boardMain.keyPressed(e.KeyCode);

        }





        private void unFocusableButton1_Click(object sender, EventArgs e)
        {
            boardMain.reinit();
        }

        private void btnFeedBack_Click(object sender, EventArgs e)
        {
            //System.Diagnostics.Process.Start("mailto:lihenantony1997@gmail.com");
            System.Diagnostics.Process.Start("mailto:lihenantony1997@gmail.com?subject=Tetris Feedback");
        }

        private void frmGamePlay_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            keyLeftTimeCounter = 0;
            keyRightTimeCounter = 0;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            if (leftKey && keyLeftTimeCounter > 1)
            {
                boardMain.keyPressed(Keys.Left);

            }

            if (rightKey && keyRightTimeCounter > 1)
                boardMain.keyPressed(Keys.Right);
        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Game progress will lost!","are you sure",MessageBoxButtons.OKCancel,MessageBoxIcon.Exclamation) == System.Windows.Forms.DialogResult.OK)
            {
                this.DialogResult = System.Windows.Forms.DialogResult.Abort;
                this.Close();

            }
        }



       


    }
}
