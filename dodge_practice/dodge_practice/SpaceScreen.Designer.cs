namespace dodge_practice
{
    partial class SpaceScreen
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            moveTimer = new System.Windows.Forms.Timer(components);
            infoTimer = new System.Windows.Forms.Timer(components);
            difficultyTimer = new System.Windows.Forms.Timer(components);
            panel1 = new Panel();
            label3 = new Label();
            ScoreBoard = new Label();
            InfoBoard = new Label();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // moveTimer
            // 
            moveTimer.Interval = 10;
            moveTimer.Tick += moveTimer_tick;
            // 
            // infoTimer
            // 
            infoTimer.Interval = 10;
            infoTimer.Tick += infoTimer_tick;
            // 
            // difficultyTimer
            // 
            difficultyTimer.Interval = 1000;
            difficultyTimer.Tick += difficultyTimer_tick;
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(64, 64, 64);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(ScoreBoard);
            panel1.Controls.Add(InfoBoard);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(686, 420);
            panel1.TabIndex = 0;
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Bottom;
            label3.AutoSize = true;
            label3.ForeColor = Color.FromArgb(255, 192, 192);
            label3.Location = new Point(325, 402);
            label3.Name = "label3";
            label3.Size = new Size(79, 14);
            label3.TabIndex = 8;
            label3.Text = "SurvivedTime";
            label3.TextAlign = ContentAlignment.BottomCenter;
            // 
            // ScoreBoard
            // 
            ScoreBoard.AutoSize = true;
            ScoreBoard.Dock = DockStyle.Top;
            ScoreBoard.ForeColor = Color.FromArgb(255, 192, 192);
            ScoreBoard.Location = new Point(0, 0);
            ScoreBoard.Name = "ScoreBoard";
            ScoreBoard.Size = new Size(67, 14);
            ScoreBoard.TabIndex = 7;
            ScoreBoard.Text = "ScoreBoard";
            // 
            // InfoBoard
            // 
            InfoBoard.AutoSize = true;
            InfoBoard.Dock = DockStyle.Bottom;
            InfoBoard.ForeColor = Color.FromArgb(255, 192, 192);
            InfoBoard.Location = new Point(0, 406);
            InfoBoard.Name = "InfoBoard";
            InfoBoard.Size = new Size(61, 14);
            InfoBoard.TabIndex = 4;
            InfoBoard.Text = "SpeedInfo";
            // 
            // SpaceScreen
            // 
            AutoScaleDimensions = new SizeF(6F, 14F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaptionText;
            ClientSize = new Size(686, 420);
            Controls.Add(panel1);
            Font = new Font("D2Coding", 9F, FontStyle.Regular, GraphicsUnit.Point);
            Name = "SpaceScreen";
            Text = "Form1";
            KeyDown += WASD_Checker;
            KeyUp += WASD_keyup;
            PreviewKeyDown += WASD_Checker;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private System.Windows.Forms.Timer moveTimer;
        private System.Windows.Forms.Timer infoTimer;
        private System.Windows.Forms.Timer difficultyTimer;
        private Panel panel1;
        private Label InfoBoard;
        private PictureBox pictureBox1;
        private Label ScoreBoard;
        private Label label3;
    }
}