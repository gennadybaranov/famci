namespace GAMEGAMEGAME
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.MainMenu = new System.Windows.Forms.Panel();
            this.BestScoresPanel = new System.Windows.Forms.Panel();
            this.ScoresBox = new System.Windows.Forms.ListBox();
            this.BtnBack = new System.Windows.Forms.Button();
            this.NameBox = new System.Windows.Forms.TextBox();
            this.BtnBest = new System.Windows.Forms.Button();
            this.BtnStart = new System.Windows.Forms.Button();
            this.SnakePanel = new System.Windows.Forms.Panel();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.NewGame = new System.Windows.Forms.MenuStrip();
            this.newGameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bestScoresToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.BtnBackFromGame = new System.Windows.Forms.Button();
            this.MainMenu.SuspendLayout();
            this.BestScoresPanel.SuspendLayout();
            this.SnakePanel.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.NewGame.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainMenu
            // 
            this.MainMenu.Controls.Add(this.NameBox);
            this.MainMenu.Controls.Add(this.BtnBest);
            this.MainMenu.Controls.Add(this.BtnStart);
            this.MainMenu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainMenu.Location = new System.Drawing.Point(0, 0);
            this.MainMenu.Name = "MainMenu";
            this.MainMenu.Size = new System.Drawing.Size(669, 442);
            this.MainMenu.TabIndex = 0;
            this.MainMenu.Paint += new System.Windows.Forms.PaintEventHandler(this.MainMenu_Paint);
            // 
            // BestScoresPanel
            // 
            this.BestScoresPanel.Controls.Add(this.ScoresBox);
            this.BestScoresPanel.Controls.Add(this.BtnBack);
            this.BestScoresPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BestScoresPanel.Location = new System.Drawing.Point(0, 0);
            this.BestScoresPanel.Name = "BestScoresPanel";
            this.BestScoresPanel.Size = new System.Drawing.Size(669, 442);
            this.BestScoresPanel.TabIndex = 3;
            this.BestScoresPanel.Visible = false;
            this.BestScoresPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.BestScoresPanel_Paint);
            // 
            // ScoresBox
            // 
            this.ScoresBox.FormattingEnabled = true;
            this.ScoresBox.Location = new System.Drawing.Point(12, 23);
            this.ScoresBox.Name = "ScoresBox";
            this.ScoresBox.Size = new System.Drawing.Size(306, 394);
            this.ScoresBox.Sorted = true;
            this.ScoresBox.TabIndex = 1;
            this.ScoresBox.SelectedIndexChanged += new System.EventHandler(this.ScoresBox_SelectedIndexChanged);
            // 
            // BtnBack
            // 
            this.BtnBack.Location = new System.Drawing.Point(324, 81);
            this.BtnBack.Name = "BtnBack";
            this.BtnBack.Size = new System.Drawing.Size(139, 23);
            this.BtnBack.TabIndex = 0;
            this.BtnBack.Text = "Back to main menu";
            this.BtnBack.UseVisualStyleBackColor = true;
            this.BtnBack.Click += new System.EventHandler(this.BtnBack_Click);
            // 
            // NameBox
            // 
            this.NameBox.Location = new System.Drawing.Point(238, 122);
            this.NameBox.Name = "NameBox";
            this.NameBox.Size = new System.Drawing.Size(152, 20);
            this.NameBox.TabIndex = 2;
            this.NameBox.TextChanged += new System.EventHandler(this.NameBox_TextChanged);
            this.NameBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.NameBox_MouseDown);
            // 
            // BtnBest
            // 
            this.BtnBest.Location = new System.Drawing.Point(238, 226);
            this.BtnBest.Name = "BtnBest";
            this.BtnBest.Size = new System.Drawing.Size(152, 23);
            this.BtnBest.TabIndex = 1;
            this.BtnBest.Text = "Best scores";
            this.BtnBest.UseVisualStyleBackColor = true;
            this.BtnBest.Click += new System.EventHandler(this.BtnBest_Click);
            // 
            // BtnStart
            // 
            this.BtnStart.Location = new System.Drawing.Point(238, 174);
            this.BtnStart.Name = "BtnStart";
            this.BtnStart.Size = new System.Drawing.Size(152, 23);
            this.BtnStart.TabIndex = 0;
            this.BtnStart.Text = "Start a new game";
            this.BtnStart.UseVisualStyleBackColor = true;
            this.BtnStart.Click += new System.EventHandler(this.BtnStart_Click);
            // 
            // SnakePanel
            // 
            this.SnakePanel.Controls.Add(this.BtnBackFromGame);
            this.SnakePanel.Controls.Add(this.statusStrip);
            this.SnakePanel.Controls.Add(this.NewGame);
            this.SnakePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SnakePanel.Location = new System.Drawing.Point(0, 0);
            this.SnakePanel.Name = "SnakePanel";
            this.SnakePanel.Size = new System.Drawing.Size(669, 442);
            this.SnakePanel.TabIndex = 1;
            this.SnakePanel.Visible = false;
            this.SnakePanel.SizeChanged += new System.EventHandler(this.SizeChanged);
            this.SnakePanel.Paint += new System.Windows.Forms.PaintEventHandler(this.SnakePanel_Paint);
            this.SnakePanel.Resize += new System.EventHandler(this.SnakePanel_Resize);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip.Location = new System.Drawing.Point(0, 420);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(669, 22);
            this.statusStrip.TabIndex = 4;
            this.statusStrip.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 17);
            this.toolStripStatusLabel1.Click += new System.EventHandler(this.toolStripStatusLabel1_Click);
            // 
            // NewGame
            // 
            this.NewGame.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newGameToolStripMenuItem,
            this.bestScoresToolStripMenuItem});
            this.NewGame.Location = new System.Drawing.Point(0, 0);
            this.NewGame.Name = "NewGame";
            this.NewGame.Size = new System.Drawing.Size(669, 24);
            this.NewGame.TabIndex = 3;
            this.NewGame.Text = "menuStrip1";
            // 
            // newGameToolStripMenuItem
            // 
            this.newGameToolStripMenuItem.Name = "newGameToolStripMenuItem";
            this.newGameToolStripMenuItem.Size = new System.Drawing.Size(73, 20);
            this.newGameToolStripMenuItem.Text = "New game";
            this.newGameToolStripMenuItem.Click += new System.EventHandler(this.newGameToolStripMenuItem_Click);
            // 
            // bestScoresToolStripMenuItem
            // 
            this.bestScoresToolStripMenuItem.Name = "bestScoresToolStripMenuItem";
            this.bestScoresToolStripMenuItem.Size = new System.Drawing.Size(77, 20);
            this.bestScoresToolStripMenuItem.Text = "Best Scores";
            this.bestScoresToolStripMenuItem.Click += new System.EventHandler(this.bestScoresToolStripMenuItem_Click);
            // 
            // BtnBackFromGame
            // 
            this.BtnBackFromGame.Location = new System.Drawing.Point(238, 110);
            this.BtnBackFromGame.Name = "BtnBackFromGame";
            this.BtnBackFromGame.Size = new System.Drawing.Size(120, 23);
            this.BtnBackFromGame.TabIndex = 5;
            this.BtnBackFromGame.Text = "Back to main menu";
            this.BtnBackFromGame.UseVisualStyleBackColor = true;
            this.BtnBackFromGame.Visible = false;
            this.BtnBackFromGame.Click += new System.EventHandler(this.BtnBackFromGame_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(669, 442);
            this.Controls.Add(this.SnakePanel);
            this.Controls.Add(this.MainMenu);
            this.Controls.Add(this.BestScoresPanel);
            this.Name = "Form1";
            this.Text = "Snake";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.keyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.keyUp);
            this.MainMenu.ResumeLayout(false);
            this.MainMenu.PerformLayout();
            this.BestScoresPanel.ResumeLayout(false);
            this.SnakePanel.ResumeLayout(false);
            this.SnakePanel.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.NewGame.ResumeLayout(false);
            this.NewGame.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel MainMenu;
        private System.Windows.Forms.TextBox NameBox;
        private System.Windows.Forms.Button BtnBest;
        private System.Windows.Forms.Button BtnStart;
        private System.Windows.Forms.Panel BestScoresPanel;
        private System.Windows.Forms.ListBox ScoresBox;
        private System.Windows.Forms.Button BtnBack;
        private System.Windows.Forms.Panel SnakePanel;
        private System.Windows.Forms.MenuStrip NewGame;
        private System.Windows.Forms.ToolStripMenuItem newGameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bestScoresToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.Button BtnBackFromGame;
    }
}

