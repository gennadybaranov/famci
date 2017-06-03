namespace Snake.UI
{
    partial class Form1
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newGameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.scoresMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createNewGamePanel = new System.Windows.Forms.Panel();
            this.scoresButton = new System.Windows.Forms.Button();
            this.startNewGameButton = new System.Windows.Forms.Button();
            this.gameNameTextBox = new System.Windows.Forms.TextBox();
            this.snakeGamePanel = new System.Windows.Forms.Panel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.yourScore = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel4 = new System.Windows.Forms.ToolStripStatusLabel();
            this.bestScore = new System.Windows.Forms.ToolStripStatusLabel();
            this.scoresPanel = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.listScores = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.menuStrip1.SuspendLayout();
            this.createNewGamePanel.SuspendLayout();
            this.snakeGamePanel.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.scoresPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(8, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(379, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newGameToolStripMenuItem,
            this.scoresMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(44, 24);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newGameToolStripMenuItem
            // 
            this.newGameToolStripMenuItem.Name = "newGameToolStripMenuItem";
            this.newGameToolStripMenuItem.Size = new System.Drawing.Size(157, 26);
            this.newGameToolStripMenuItem.Text = "New Game";
            this.newGameToolStripMenuItem.Click += new System.EventHandler(this.OnNewGameMenuItemClicked);
            // 
            // scoresMenuItem
            // 
            this.scoresMenuItem.Name = "scoresMenuItem";
            this.scoresMenuItem.Size = new System.Drawing.Size(157, 26);
            this.scoresMenuItem.Text = "Scores";
            this.scoresMenuItem.Click += new System.EventHandler(this.scoresMenuItem_Click);
            // 
            // createNewGamePanel
            // 
            this.createNewGamePanel.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.createNewGamePanel.Controls.Add(this.scoresButton);
            this.createNewGamePanel.Controls.Add(this.startNewGameButton);
            this.createNewGamePanel.Controls.Add(this.gameNameTextBox);
            this.createNewGamePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.createNewGamePanel.Location = new System.Drawing.Point(0, 28);
            this.createNewGamePanel.Margin = new System.Windows.Forms.Padding(4);
            this.createNewGamePanel.Name = "createNewGamePanel";
            this.createNewGamePanel.Size = new System.Drawing.Size(379, 293);
            this.createNewGamePanel.TabIndex = 1;
            // 
            // scoresButton
            // 
            this.scoresButton.Location = new System.Drawing.Point(109, 209);
            this.scoresButton.Name = "scoresButton";
            this.scoresButton.Size = new System.Drawing.Size(145, 33);
            this.scoresButton.TabIndex = 1;
            this.scoresButton.Text = "Scores";
            this.scoresButton.UseVisualStyleBackColor = true;
            this.scoresButton.Click += new System.EventHandler(this.scoresButton_Click);
            // 
            // startNewGameButton
            // 
            this.startNewGameButton.Location = new System.Drawing.Point(109, 149);
            this.startNewGameButton.Margin = new System.Windows.Forms.Padding(4);
            this.startNewGameButton.Name = "startNewGameButton";
            this.startNewGameButton.Size = new System.Drawing.Size(145, 33);
            this.startNewGameButton.TabIndex = 1;
            this.startNewGameButton.Text = "Start New Game";
            this.startNewGameButton.UseVisualStyleBackColor = true;
            this.startNewGameButton.Click += new System.EventHandler(this.OnStartNewGameButtonClicked);
            // 
            // gameNameTextBox
            // 
            this.gameNameTextBox.Location = new System.Drawing.Point(76, 97);
            this.gameNameTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.gameNameTextBox.Name = "gameNameTextBox";
            this.gameNameTextBox.Size = new System.Drawing.Size(221, 22);
            this.gameNameTextBox.TabIndex = 0;
            // 
            // snakeGamePanel
            // 
            this.snakeGamePanel.Controls.Add(this.statusStrip1);
            this.snakeGamePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.snakeGamePanel.Location = new System.Drawing.Point(0, 28);
            this.snakeGamePanel.Margin = new System.Windows.Forms.Padding(4);
            this.snakeGamePanel.Name = "snakeGamePanel";
            this.snakeGamePanel.Size = new System.Drawing.Size(379, 293);
            this.snakeGamePanel.TabIndex = 2;
            this.snakeGamePanel.Paint += new System.Windows.Forms.PaintEventHandler(this.snakeGamePanel_Paint);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel2,
            this.yourScore,
            this.toolStripStatusLabel4,
            this.bestScore});
            this.statusStrip1.Location = new System.Drawing.Point(0, 268);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(379, 25);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(77, 20);
            this.toolStripStatusLabel1.Text = "Your score";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(0, 20);
            // 
            // yourScore
            // 
            this.yourScore.Name = "yourScore";
            this.yourScore.Size = new System.Drawing.Size(0, 20);
            // 
            // toolStripStatusLabel4
            // 
            this.toolStripStatusLabel4.Name = "toolStripStatusLabel4";
            this.toolStripStatusLabel4.Size = new System.Drawing.Size(78, 20);
            this.toolStripStatusLabel4.Text = "Best Score";
            // 
            // bestScore
            // 
            this.bestScore.Name = "bestScore";
            this.bestScore.Size = new System.Drawing.Size(0, 20);
            // 
            // scoresPanel
            // 
            this.scoresPanel.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.scoresPanel.Controls.Add(this.label1);
            this.scoresPanel.Controls.Add(this.listScores);
            this.scoresPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scoresPanel.Location = new System.Drawing.Point(0, 28);
            this.scoresPanel.Margin = new System.Windows.Forms.Padding(4);
            this.scoresPanel.Name = "scoresPanel";
            this.scoresPanel.Size = new System.Drawing.Size(379, 293);
            this.scoresPanel.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(153, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "label1";
            // 
            // listScores
            // 
            this.listScores.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.listScores.Location = new System.Drawing.Point(61, 48);
            this.listScores.Name = "listScores";
            this.listScores.Size = new System.Drawing.Size(253, 204);
            this.listScores.TabIndex = 0;
            this.listScores.UseCompatibleStateImageBehavior = false;
            this.listScores.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Player";
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Score";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(379, 321);
            this.Controls.Add(this.snakeGamePanel);
            this.Controls.Add(this.createNewGamePanel);
            this.Controls.Add(this.scoresPanel);
            this.Controls.Add(this.menuStrip1);
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "Form1";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnKeyDown);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.createNewGamePanel.ResumeLayout(false);
            this.createNewGamePanel.PerformLayout();
            this.snakeGamePanel.ResumeLayout(false);
            this.snakeGamePanel.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.scoresPanel.ResumeLayout(false);
            this.scoresPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newGameToolStripMenuItem;
        private System.Windows.Forms.Panel createNewGamePanel;
        private System.Windows.Forms.Button startNewGameButton;
        private System.Windows.Forms.TextBox gameNameTextBox;
        private System.Windows.Forms.Panel snakeGamePanel;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel yourScore;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel4;
        private System.Windows.Forms.ToolStripStatusLabel bestScore;
        private System.Windows.Forms.Button scoresButton;
        private System.Windows.Forms.Panel scoresPanel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListView listScores;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolStripMenuItem scoresMenuItem;
    }
}

