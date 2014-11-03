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
                board1.keyPressed(Keys.Left);

            if (rightKey)
                board1.keyPressed(Keys.Right);
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
            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Right)
                return;

            board1.keyPressed(e.KeyCode);

        }

        private void timUpKeyDector_Tick(object sender, EventArgs e)
        {
            //upKey = Keyboard.IsKeyDown(Keys.Up);

            //if (upKey)
            //    board1.keyPressed(Keys.Up);
        }

        
    }
}
