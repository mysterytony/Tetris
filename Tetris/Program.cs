using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Tetris.TetrisBoard;
using System.Drawing;

namespace Tetris
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new frmGamePlay());
            //"192.168.1.84"
            //Application.Run(new frmMultiplayer(120,"192.168.1.84"));


            MapGenerator mg = new MapGenerator();
            mg.addCol(0, 10, 19, Color.Red);
            mg.addRow(6, 1, 9, Color.Red);
            Application.Run(new frmGamePlay(0,mg.colors));
        }
    }
}
