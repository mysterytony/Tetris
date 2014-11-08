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
    public partial class frmGamePlay : Form
    {
        int keyLeftTimeCounter = 0, keyRightTimeCounter;



        /// <summary>
        /// indicate wheather the key is pressed
        /// </summary>
        public bool upKey = false,
            leftKey = false,
            rightKey = false;



        public frmGamePlay()
        {
            InitializeComponent();


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
            board1.timeTick();
        }

        private void frmGamePlay_Load(object sender, EventArgs e)
        {

        }

        private void frmGamePlay_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {



            if ((e.KeyCode == Keys.Left && keyLeftTimeCounter > 1) || (e.KeyCode == Keys.Right && keyRightTimeCounter > 1))
                return;

            board1.keyPressed(e.KeyCode);

        }





        private void unFocusableButton1_Click(object sender, EventArgs e)
        {
            board1.reinit();
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
                board1.keyPressed(Keys.Left);

            }

            if (rightKey && keyRightTimeCounter > 1)
                board1.keyPressed(Keys.Right);
        }

        private void sliderSens_Scroll(object sender, EventArgs e)
        {
             numpickSens.Value =   sliderSens.Value;
             timKeyHold.Interval = sliderSens.Value;
        }

        private void numpickSens_ValueChanged(object sender, EventArgs e)
        {
            sliderSens.Value = (int)numpickSens.Value;
        }

       


    }
}
