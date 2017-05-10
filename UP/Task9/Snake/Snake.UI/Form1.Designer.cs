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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.createNewGamePanel = new System.Windows.Forms.Panel();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newGameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gameNameTextBox = new System.Windows.Forms.TextBox();
            this.startNewGameButton = new System.Windows.Forms.Button();
            this.snakeGamePanel = new System.Windows.Forms.Panel();
            this.menuStrip1.SuspendLayout();
            this.createNewGamePanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(284, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // createNewGamePanel
            // 
            this.createNewGamePanel.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.createNewGamePanel.Controls.Add(this.snakeGamePanel);
            this.createNewGamePanel.Controls.Add(this.startNewGameButton);
            this.createNewGamePanel.Controls.Add(this.gameNameTextBox);
            this.createNewGamePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.createNewGamePanel.Location = new System.Drawing.Point(0, 24);
            this.createNewGamePanel.Name = "createNewGamePanel";
            this.createNewGamePanel.Size = new System.Drawing.Size(284, 237);
            this.createNewGamePanel.TabIndex = 1;
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newGameToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newGameToolStripMenuItem
            // 
            this.newGameToolStripMenuItem.Name = "newGameToolStripMenuItem";
            this.newGameToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.newGameToolStripMenuItem.Text = "New Game";
            this.newGameToolStripMenuItem.Click += new System.EventHandler(this.OnNewGameMenuItemClicked);
            // 
            // gameNameTextBox
            // 
            this.gameNameTextBox.Location = new System.Drawing.Point(57, 79);
            this.gameNameTextBox.Name = "gameNameTextBox";
            this.gameNameTextBox.Size = new System.Drawing.Size(167, 20);
            this.gameNameTextBox.TabIndex = 0;
            // 
            // startNewGameButton
            // 
            this.startNewGameButton.Location = new System.Drawing.Point(82, 121);
            this.startNewGameButton.Name = "startNewGameButton";
            this.startNewGameButton.Size = new System.Drawing.Size(109, 23);
            this.startNewGameButton.TabIndex = 1;
            this.startNewGameButton.Text = "Start New Game";
            this.startNewGameButton.UseVisualStyleBackColor = true;
            this.startNewGameButton.Click += new System.EventHandler(this.OnStartNewGameButtonClicked);
            // 
            // snakeGamePanel
            // 
            this.snakeGamePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.snakeGamePanel.Location = new System.Drawing.Point(0, 0);
            this.snakeGamePanel.Name = "snakeGamePanel";
            this.snakeGamePanel.Size = new System.Drawing.Size(284, 237);
            this.snakeGamePanel.TabIndex = 2;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.createNewGamePanel);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.createNewGamePanel.ResumeLayout(false);
            this.createNewGamePanel.PerformLayout();
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
    }
}

