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
using System.Net;
using System.Net.Sockets;
using System.Threading;

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

        public Thread msglistener;




        public frmMultiplayer(int timelimit, string ip)
        {
            InitializeComponent();

          
             boardMain.totaltime = timelimit;
             this.Text = "Tetris - Multiplayer Mode";

             boardSecondPlayer.board_mode = TetrisBoard.Board.boardMode.watch;
             this.ip = ip;

            sendEnd = new IPEndPoint(IPAddress.Parse(ip), 5418);

            msglistener = new Thread(new ThreadStart(receiveMsg));
            msglistener.Start();
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

                this.DialogResult = System.Windows.Forms.DialogResult.None;
                processGameOver();
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
            if (MessageBox.Show("Your will lose if you quit","are you sure",MessageBoxButtons.OKCancel,MessageBoxIcon.Exclamation) == System.Windows.Forms.DialogResult.OK)
            {
                this.DialogResult = System.Windows.Forms.DialogResult.Abort;
                processGameOver();

            }
        }


        #region game

        public string ip;
        public int oppScore = 0;
        public bool isOppQuitted = false;


        public void processGameOver()
        {
            sendMsg(generateGameOverMsg());

            

            //give superior form win or lose info.
            if (isOppQuitted)
            {
                //win
                this.DialogResult = System.Windows.Forms.DialogResult.Yes;
            }

            if (this.DialogResult == System.Windows.Forms.DialogResult.Cancel || oppScore == -1)
            {
                //exception end - me or opp
                
            }

            if (this.DialogResult == System.Windows.Forms.DialogResult.Abort)
            {
                //lose
                this.DialogResult = System.Windows.Forms.DialogResult.Abort;
            }

            if (this.boardMain.score > oppScore)
            {
                //win
                this.DialogResult = System.Windows.Forms.DialogResult.Yes;
            }
            else if (this.boardMain.score < oppScore)
            {
                //lose
                this.DialogResult = System.Windows.Forms.DialogResult.No;
            }
            else
            {
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                //tie
            }


            Environment.Exit(0);

        }

        public void processRecievedMsg(string msg)
        {
            string[] msgs = msg.Split(',');

            switch (msgs[0])
            {
                case "gameover":
                    {
                        if (msgs[1] == "gamequitted")
                        {
                            isOppQuitted = true;
                            processGameOver();
                        }
                        else if (msgs[1] == "gamefinished")
                        {
                            processGameOver();

                        }
                        else if (msgs[1] == "gameexception")
                        {
                            oppScore = -1;
                            processGameOver();
                        }
                        break;
                    }
                case "updatescore":
                    {
                        boardSecondPlayer.score = Convert.ToInt16(msgs[1]);
                        this.oppScore = Convert.ToInt16(msgs[1]);
                        break;
                    }
                case "updateboard":
                    {
                        boardSecondPlayer.setBoard(msgs[1]);
                        break;
                    }
            }
        }


        public Socket sendingSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        public IPEndPoint sendEnd;

        public void sendMsg(string msg)
        {
            byte[] sendbuffer = Encoding.ASCII.GetBytes(msg);
            try
            {
                sendingSocket.SendTo(sendbuffer, sendEnd);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "error occured", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                processGameOver();
                throw e;
            }
            
        }

        public UdpClient listener = new UdpClient(5418);
        IPEndPoint groupep = new IPEndPoint(IPAddress.Any, 5418);

        public void receiveMsg()
        {

            

            byte[] bytes;
            while(true)
            {
                try
                {
                    bytes = listener.Receive(ref groupep);
                    processRecievedMsg(Encoding.ASCII.GetString(bytes, 0, bytes.Length));
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "error occured", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                    processGameOver();
                    throw e;
                }
            }
        }

        public string generateGameOverMsg()
        {
            if (this.DialogResult == System.Windows.Forms.DialogResult.Abort)
                return "gameover,gamequitted";
            else if (this.DialogResult == System.Windows.Forms.DialogResult.Cancel)
                return "gameover,gameexception";
            else
                return "gameover,gamefinished," + boardMain.score;
        }

        public string generateBoardMsg(int line)
        {
            string msg = "updateboard,";
            msg += (line + "|");
            for (int i=0;i<10;++i)
            {
                msg += ((boardMain.map.map[i, line].blockColor.Name == "0" ? "Empty" : boardMain.map.map[i, line].blockColor.Name) + "|");
            }
            return msg;
        }

        public string generateBoardMsg()
        {
            string msg = "updateboard,";
            //msg += (line + "|");
            for (int j = 0; j < 20; ++j)
            {
                msg += (j + "|");
                for (int i = 0; i < 10; ++i)
                {
                    msg += (boardMain.map.map[i, j].blockColor.Name  + "|");
                }
            }
            return msg;
        }



        #endregion

        int currsendingline = 0;
        private void timSender_Tick(object sender, EventArgs e)
        {
            //if (currsendingline >= 20)
            //{
            //    sendMsg("updatescore," + boardMain.score);
            //    currsendingline = 0;
            //    return;
            //}

            sendMsg("updatescore," + boardMain.score);
            sendMsg(generateBoardMsg());
            //currsendingline++;


            boardSecondPlayer.refreshMap();
            boardSecondPlayer.displayScore(false);

        }

        private void frmMultiplayer_FormClosing(object sender, FormClosingEventArgs e)
        {
            
            Environment.Exit(0);
        }

    }
}
