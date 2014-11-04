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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnFeedBack = new Tetris.Controls.UnFocusableButton();
            this.btnRestart = new Tetris.Controls.UnFocusableButton();
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
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(445, 127);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "<--- Next Shape";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(445, 229);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "<--- Saved Shape";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(566, 127);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(151, 78);
            this.label3.TabIndex = 3;
            this.label3.Text = "Tetris 1.0 build 0102 by Tony\r\n\r\narrow key left, right, down\r\narrow key up to tur" +
    "n\r\nspace to fall\r\nc key to save :)";
            // 
            // btnFeedBack
            // 
            this.btnFeedBack.Location = new System.Drawing.Point(569, 270);
            this.btnFeedBack.Name = "btnFeedBack";
            this.btnFeedBack.Size = new System.Drawing.Size(75, 23);
            this.btnFeedBack.TabIndex = 5;
            this.btnFeedBack.Text = "feedback :)";
            this.btnFeedBack.UseVisualStyleBackColor = true;
            this.btnFeedBack.Click += new System.EventHandler(this.btnFeedBack_Click);
            // 
            // btnRestart
            // 
            this.btnRestart.Location = new System.Drawing.Point(569, 219);
            this.btnRestart.Name = "btnRestart";
            this.btnRestart.Size = new System.Drawing.Size(75, 23);
            this.btnRestart.TabIndex = 4;
            this.btnRestart.Text = "restart";
            this.btnRestart.UseVisualStyleBackColor = true;
            this.btnRestart.Click += new System.EventHandler(this.unFocusableButton1_Click);
            // 
            // board1
            // 
            this.board1.Location = new System.Drawing.Point(26, 28);
            this.board1.Name = "board1";
            this.board1.Size = new System.Drawing.Size(413, 400);
            this.board1.TabIndex = 0;
            // 
            // frmGamePlay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(666, 434);
            this.Controls.Add(this.btnFeedBack);
            this.Controls.Add(this.btnRestart);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.board1);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "frmGamePlay";
            this.Text = "Tetris";
            this.Load += new System.EventHandler(this.frmGamePlay_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmGamePlay_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.frmGamePlay_KeyUp);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer timKeyDetector;
        private TetrisBoard.Board board1;
        private System.Windows.Forms.Timer timTicker;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private Controls.UnFocusableButton btnRestart;
        private Controls.UnFocusableButton btnFeedBack;
    }
}

