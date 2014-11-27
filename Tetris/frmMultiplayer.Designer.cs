namespace Tetris
{
    partial class frmMultiplayer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.timKeyDetector = new System.Windows.Forms.Timer(this.components);
            this.timTicker = new System.Windows.Forms.Timer(this.components);
            this.timKeyHold = new System.Windows.Forms.Timer(this.components);
            this.btnQuit = new Tetris.Controls.UnFocusableButton();
            this.boardMain = new Tetris.TetrisBoard.Board();
            this.SuspendLayout();
            // 
            // timKeyDetector
            // 
            this.timKeyDetector.Enabled = true;
            this.timKeyDetector.Interval = 75;
            this.timKeyDetector.Tick += new System.EventHandler(this.timKeyDetector_Tick);
            // 
            // timTicker
            // 
            this.timTicker.Enabled = true;
            this.timTicker.Interval = 10;
            this.timTicker.Tick += new System.EventHandler(this.timTicker_Tick);
            // 
            // timKeyHold
            // 
            this.timKeyHold.Enabled = true;
            this.timKeyHold.Interval = 160;
            this.timKeyHold.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // btnQuit
            // 
            this.btnQuit.Location = new System.Drawing.Point(365, 28);
            this.btnQuit.Name = "btnQuit";
            this.btnQuit.Size = new System.Drawing.Size(123, 23);
            this.btnQuit.TabIndex = 1;
            this.btnQuit.Text = "Quit";
            this.btnQuit.UseVisualStyleBackColor = true;
            this.btnQuit.Click += new System.EventHandler(this.btnQuit_Click);
            // 
            // boardMain
            // 
            this.boardMain.Location = new System.Drawing.Point(26, 28);
            this.boardMain.Name = "boardMain";
            this.boardMain.Size = new System.Drawing.Size(333, 400);
            this.boardMain.TabIndex = 0;
            // 
            // frmMultiplayer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(502, 450);
            this.Controls.Add(this.btnQuit);
            this.Controls.Add(this.boardMain);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "frmMultiplayer";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tetris";
            this.Load += new System.EventHandler(this.frmGamePlay_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmGamePlay_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.frmGamePlay_KeyUp);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timKeyDetector;
        private TetrisBoard.Board boardMain;
        private System.Windows.Forms.Timer timTicker;
        private System.Windows.Forms.Timer timKeyHold;
        private Controls.UnFocusableButton btnQuit;
    }
}

