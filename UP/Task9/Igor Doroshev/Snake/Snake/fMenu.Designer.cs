namespace SnakeNS
{
    partial class fMenu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fMenu));
            this.Exit = new System.Windows.Forms.Button();
            this.Start = new System.Windows.Forms.Button();
            this.BestScores = new System.Windows.Forms.Button();
            this.menuPanel = new System.Windows.Forms.Panel();
            this.gamePanel = new System.Windows.Forms.Panel();
            this.Entertb = new System.Windows.Forms.TextBox();
            this.GameOverlbl = new System.Windows.Forms.Label();
            this.Backbtn = new System.Windows.Forms.Button();
            this.Showbtn = new System.Windows.Forms.Button();
            this.Restartbtn = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.GameBox = new System.Windows.Forms.PictureBox();
            this.scoresPanel = new System.Windows.Forms.Panel();
            this.Backbtnb = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.scoresList = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.menuPanel.SuspendLayout();
            this.gamePanel.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GameBox)).BeginInit();
            this.scoresPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // Exit
            // 
            this.Exit.CausesValidation = false;
            this.Exit.Font = new System.Drawing.Font("Microsoft Sans Serif", 22.1F);
            this.Exit.Location = new System.Drawing.Point(152, 549);
            this.Exit.Name = "Exit";
            this.Exit.Size = new System.Drawing.Size(834, 143);
            this.Exit.TabIndex = 2;
            this.Exit.Text = "Exit";
            this.Exit.UseVisualStyleBackColor = true;
            this.Exit.Click += new System.EventHandler(this.Exit_Click);
            // 
            // Start
            // 
            this.Start.CausesValidation = false;
            this.Start.Font = new System.Drawing.Font("Microsoft Sans Serif", 22.1F);
            this.Start.Location = new System.Drawing.Point(152, 192);
            this.Start.Name = "Start";
            this.Start.Size = new System.Drawing.Size(834, 143);
            this.Start.TabIndex = 0;
            this.Start.Text = "Start";
            this.Start.UseVisualStyleBackColor = true;
            this.Start.Click += new System.EventHandler(this.Start_Click);
            // 
            // BestScores
            // 
            this.BestScores.CausesValidation = false;
            this.BestScores.Font = new System.Drawing.Font("Microsoft Sans Serif", 22.1F);
            this.BestScores.Location = new System.Drawing.Point(152, 368);
            this.BestScores.Name = "BestScores";
            this.BestScores.Size = new System.Drawing.Size(834, 143);
            this.BestScores.TabIndex = 1;
            this.BestScores.Text = "Best Scores";
            this.BestScores.UseVisualStyleBackColor = true;
            this.BestScores.Click += new System.EventHandler(this.BestScores_Click);
            // 
            // menuPanel
            // 
            this.menuPanel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("menuPanel.BackgroundImage")));
            this.menuPanel.Controls.Add(this.Start);
            this.menuPanel.Controls.Add(this.BestScores);
            this.menuPanel.Controls.Add(this.Exit);
            this.menuPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.menuPanel.Location = new System.Drawing.Point(0, 0);
            this.menuPanel.Name = "menuPanel";
            this.menuPanel.Size = new System.Drawing.Size(1168, 1168);
            this.menuPanel.TabIndex = 3;
            // 
            // gamePanel
            // 
            this.gamePanel.BackColor = System.Drawing.Color.White;
            this.gamePanel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("gamePanel.BackgroundImage")));
            this.gamePanel.Controls.Add(this.Entertb);
            this.gamePanel.Controls.Add(this.GameOverlbl);
            this.gamePanel.Controls.Add(this.Backbtn);
            this.gamePanel.Controls.Add(this.Showbtn);
            this.gamePanel.Controls.Add(this.Restartbtn);
            this.gamePanel.Controls.Add(this.statusStrip1);
            this.gamePanel.Controls.Add(this.GameBox);
            this.gamePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gamePanel.Location = new System.Drawing.Point(0, 0);
            this.gamePanel.Name = "gamePanel";
            this.gamePanel.Size = new System.Drawing.Size(1168, 1168);
            this.gamePanel.TabIndex = 3;
            this.gamePanel.Visible = false;
            // 
            // Entertb
            // 
            this.Entertb.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.1F);
            this.Entertb.Location = new System.Drawing.Point(345, 721);
            this.Entertb.Name = "Entertb";
            this.Entertb.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Entertb.Size = new System.Drawing.Size(479, 68);
            this.Entertb.TabIndex = 8;
            this.Entertb.Text = "Enter your name";
            this.Entertb.Visible = false;
            this.Entertb.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Entertb_MouseClick);
            // 
            // GameOverlbl
            // 
            this.GameOverlbl.AutoSize = true;
            this.GameOverlbl.BackColor = System.Drawing.Color.Transparent;
            this.GameOverlbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.1F);
            this.GameOverlbl.ForeColor = System.Drawing.Color.Red;
            this.GameOverlbl.Location = new System.Drawing.Point(393, 192);
            this.GameOverlbl.Name = "GameOverlbl";
            this.GameOverlbl.Size = new System.Drawing.Size(385, 78);
            this.GameOverlbl.TabIndex = 7;
            this.GameOverlbl.Text = "Game Over";
            this.GameOverlbl.Visible = false;
            // 
            // Backbtn
            // 
            this.Backbtn.BackColor = System.Drawing.SystemColors.Control;
            this.Backbtn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Backbtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.1F);
            this.Backbtn.Location = new System.Drawing.Point(345, 458);
            this.Backbtn.Name = "Backbtn";
            this.Backbtn.Size = new System.Drawing.Size(479, 71);
            this.Backbtn.TabIndex = 6;
            this.Backbtn.Text = "Back to the menu";
            this.Backbtn.UseVisualStyleBackColor = false;
            this.Backbtn.Visible = false;
            this.Backbtn.Click += new System.EventHandler(this.Backbtn_Click);
            // 
            // Showbtn
            // 
            this.Showbtn.BackColor = System.Drawing.SystemColors.Control;
            this.Showbtn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Showbtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.1F);
            this.Showbtn.Location = new System.Drawing.Point(345, 549);
            this.Showbtn.Name = "Showbtn";
            this.Showbtn.Size = new System.Drawing.Size(479, 71);
            this.Showbtn.TabIndex = 5;
            this.Showbtn.Text = "Show best scores";
            this.Showbtn.UseVisualStyleBackColor = false;
            this.Showbtn.Visible = false;
            this.Showbtn.Click += new System.EventHandler(this.Showbtn_Click);
            // 
            // Restartbtn
            // 
            this.Restartbtn.BackColor = System.Drawing.SystemColors.Control;
            this.Restartbtn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Restartbtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.1F);
            this.Restartbtn.Location = new System.Drawing.Point(345, 368);
            this.Restartbtn.Name = "Restartbtn";
            this.Restartbtn.Size = new System.Drawing.Size(479, 71);
            this.Restartbtn.TabIndex = 4;
            this.Restartbtn.Text = "Restart";
            this.Restartbtn.UseVisualStyleBackColor = false;
            this.Restartbtn.Visible = false;
            this.Restartbtn.Click += new System.EventHandler(this.Restartbtn_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(40, 40);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 1122);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1168, 46);
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(297, 41);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // GameBox
            // 
            this.GameBox.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("GameBox.BackgroundImage")));
            this.GameBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GameBox.Location = new System.Drawing.Point(0, 0);
            this.GameBox.Name = "GameBox";
            this.GameBox.Size = new System.Drawing.Size(1168, 1168);
            this.GameBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.GameBox.TabIndex = 0;
            this.GameBox.TabStop = false;
            this.GameBox.Paint += new System.Windows.Forms.PaintEventHandler(this.GameBox_Paint);
            // 
            // scoresPanel
            // 
            this.scoresPanel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("scoresPanel.BackgroundImage")));
            this.scoresPanel.Controls.Add(this.Backbtnb);
            this.scoresPanel.Controls.Add(this.label1);
            this.scoresPanel.Controls.Add(this.scoresList);
            this.scoresPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scoresPanel.Location = new System.Drawing.Point(0, 0);
            this.scoresPanel.Name = "scoresPanel";
            this.scoresPanel.Size = new System.Drawing.Size(1168, 1168);
            this.scoresPanel.TabIndex = 9;
            this.scoresPanel.Visible = false;
            // 
            // Backbtnb
            // 
            this.Backbtnb.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.1F);
            this.Backbtnb.Location = new System.Drawing.Point(373, 1001);
            this.Backbtnb.Name = "Backbtnb";
            this.Backbtnb.Size = new System.Drawing.Size(419, 88);
            this.Backbtnb.TabIndex = 2;
            this.Backbtnb.Text = "Back to the menu";
            this.Backbtnb.UseVisualStyleBackColor = true;
            this.Backbtnb.Click += new System.EventHandler(this.Backbtnb_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.1F);
            this.label1.Location = new System.Drawing.Point(393, 64);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(399, 78);
            this.label1.TabIndex = 1;
            this.label1.Text = "Best Scores";
            // 
            // scoresList
            // 
            this.scoresList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.scoresList.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.1F);
            this.scoresList.GridLines = true;
            this.scoresList.Location = new System.Drawing.Point(170, 181);
            this.scoresList.Name = "scoresList";
            this.scoresList.Size = new System.Drawing.Size(875, 779);
            this.scoresList.TabIndex = 0;
            this.scoresList.UseCompatibleStateImageBehavior = false;
            this.scoresList.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Name";
            this.columnHeader1.Width = 445;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Score";
            this.columnHeader2.Width = 425;
            // 
            // fMenu
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(1168, 1168);
            this.Controls.Add(this.scoresPanel);
            this.Controls.Add(this.gamePanel);
            this.Controls.Add(this.menuPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "fMenu";
            this.Text = "Snake";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.fMenu_KeyDown);
            this.menuPanel.ResumeLayout(false);
            this.gamePanel.ResumeLayout(false);
            this.gamePanel.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GameBox)).EndInit();
            this.scoresPanel.ResumeLayout(false);
            this.scoresPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button Exit;
        private System.Windows.Forms.Button Start;
        private System.Windows.Forms.Button BestScores;
        private System.Windows.Forms.Panel menuPanel;
        private System.Windows.Forms.Panel gamePanel;
        private System.Windows.Forms.PictureBox GameBox;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.Button Backbtn;
        private System.Windows.Forms.Button Showbtn;
        private System.Windows.Forms.Button Restartbtn;
        private System.Windows.Forms.TextBox Entertb;
        private System.Windows.Forms.Label GameOverlbl;
        private System.Windows.Forms.Panel scoresPanel;
        private System.Windows.Forms.ListView scoresList;
        private System.Windows.Forms.Button Backbtnb;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
    }
}

