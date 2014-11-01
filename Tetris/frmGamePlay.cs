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
            downKey = false,
            leftKey = false,
            rightKey = false,
            spaceKey = false,
            cKey = false;



        public frmGamePlay()
        {
            InitializeComponent();


        }



        private void timKeyDetector_Tick(object sender, EventArgs e)
        {
            upKey = Keyboard.IsKeyDown(Keys.Up);
            downKey = Keyboard.IsKeyDown(Keys.Down);
            leftKey = Keyboard.IsKeyDown(Keys.Left);
            rightKey = Keyboard.IsKeyDown(Keys.Right);
            spaceKey = Keyboard.IsKeyDown(Keys.Space);
            cKey = Keyboard.IsKeyDown(Keys.C);
        }

        
    }
}
