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
            spawnTimer = new System.Windows.Forms.Timer(components);
            panel1 = new Panel();
            ScoreBoard = new Label();
            label2 = new Label();
            label1 = new Label();
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
            // spawnTimer
            // 
            spawnTimer.Interval = 1000;
            spawnTimer.Tick += spawnTimer_tick;
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(64, 64, 64);
            panel1.Controls.Add(ScoreBoard);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(label1);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(686, 420);
            panel1.TabIndex = 0;
            // 
            // ScoreBoard
            // 
            ScoreBoard.AutoSize = true;
            ScoreBoard.ForeColor = Color.FromArgb(255, 192, 192);
            ScoreBoard.Location = new Point(3, 0);
            ScoreBoard.Name = "ScoreBoard";
            ScoreBoard.Size = new Size(67, 14);
            ScoreBoard.TabIndex = 7;
            ScoreBoard.Text = "ScoreBoard";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.ForeColor = Color.FromArgb(255, 192, 192);
            label2.Location = new Point(0, 402);
            label2.Name = "label2";
            label2.Size = new Size(43, 14);
            label2.TabIndex = 5;
            label2.Text = "Yspeed";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.ForeColor = Color.FromArgb(255, 192, 192);
            label1.Location = new Point(0, 388);
            label1.Name = "label1";
            label1.Size = new Size(43, 14);
            label1.TabIndex = 4;
            label1.Text = "Xspeed";
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
        private System.Windows.Forms.Timer spawnTimer;
        private Panel panel1;
        private Label label2;
        private Label label1;
        private PictureBox pictureBox1;
        private Label ScoreBoard;
    }
}