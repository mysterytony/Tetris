namespace Tetris
{
    partial class frmGamePlay
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
            this.timUpKeyDector = new System.Windows.Forms.Timer(this.components);
            this.board1 = new Tetris.TetrisBoard.Board();
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
            // timUpKeyDector
            // 
            this.timUpKeyDector.Enabled = true;
            this.timUpKeyDector.Interval = 120;
            this.timUpKeyDector.Tick += new System.EventHandler(this.timUpKeyDector_Tick);
            // 
            // board1
            // 
            this.board1.Location = new System.Drawing.Point(42, 12);
            this.board1.Name = "board1";
            this.board1.Size = new System.Drawing.Size(300, 400);
            this.board1.TabIndex = 0;
            // 
            // frmGamePlay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(599, 460);
            this.Controls.Add(this.board1);
            this.Name = "frmGamePlay";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.frmGamePlay_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmGamePlay_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timKeyDetector;
        private TetrisBoard.Board board1;
        private System.Windows.Forms.Timer timTicker;
        private System.Windows.Forms.Timer timUpKeyDector;
    }
}

