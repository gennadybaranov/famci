namespace Snake
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
            this.newGamePanel = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.nameBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.scoresToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bestScoresToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.snakeCanvas = new System.Windows.Forms.Panel();
            this.menuStrip2 = new System.Windows.Forms.MenuStrip();
            this.gameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newGameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bestScoresToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.bestScoresPanel = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.bestScoresList = new System.Windows.Forms.ListBox();
            this.OKButton = new System.Windows.Forms.Button();
            this.menuStrip3 = new System.Windows.Forms.MenuStrip();
            this.gameToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.newGameToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.newGamePanel.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.snakeCanvas.SuspendLayout();
            this.menuStrip2.SuspendLayout();
            this.bestScoresPanel.SuspendLayout();
            this.menuStrip3.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // newGamePanel
            // 
            this.newGamePanel.BackColor = System.Drawing.SystemColors.MenuText;
            this.newGamePanel.BackgroundImage = global::Snake.Properties.Resources.background;
            this.newGamePanel.Controls.Add(this.button1);
            this.newGamePanel.Controls.Add(this.nameBox);
            this.newGamePanel.Controls.Add(this.label1);
            this.newGamePanel.Controls.Add(this.menuStrip1);
            this.newGamePanel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.newGamePanel.Location = new System.Drawing.Point(0, 0);
            this.newGamePanel.Name = "newGamePanel";
            this.newGamePanel.Size = new System.Drawing.Size(321, 375);
            this.newGamePanel.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Arial Narrow", 17F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.button1.Location = new System.Drawing.Point(93, 263);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(117, 59);
            this.button1.TabIndex = 2;
            this.button1.Text = "New Game";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // nameBox
            // 
            this.nameBox.Location = new System.Drawing.Point(78, 149);
            this.nameBox.Name = "nameBox";
            this.nameBox.Size = new System.Drawing.Size(155, 20);
            this.nameBox.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft New Tai Lue", 15F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.label1.Location = new System.Drawing.Point(74, 108);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(170, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "Enter your name: ";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.scoresToolStripMenuItem});
            this.menuStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(321, 23);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // scoresToolStripMenuItem
            // 
            this.scoresToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bestScoresToolStripMenuItem});
            this.scoresToolStripMenuItem.Name = "scoresToolStripMenuItem";
            this.scoresToolStripMenuItem.Size = new System.Drawing.Size(53, 19);
            this.scoresToolStripMenuItem.Text = "Scores";
            // 
            // bestScoresToolStripMenuItem
            // 
            this.bestScoresToolStripMenuItem.Name = "bestScoresToolStripMenuItem";
            this.bestScoresToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.bestScoresToolStripMenuItem.Text = "Best Scores";
            this.bestScoresToolStripMenuItem.Click += new System.EventHandler(this.bestScoresToolStripMenuItem_Click);
            // 
            // snakeCanvas
            // 
            this.snakeCanvas.BackgroundImage = global::Snake.Properties.Resources.background;
            this.snakeCanvas.Controls.Add(this.statusStrip1);
            this.snakeCanvas.Controls.Add(this.menuStrip2);
            this.snakeCanvas.Font = new System.Drawing.Font("Arial", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.snakeCanvas.Location = new System.Drawing.Point(0, 0);
            this.snakeCanvas.Name = "snakeCanvas";
            this.snakeCanvas.Size = new System.Drawing.Size(300, 350);
            this.snakeCanvas.TabIndex = 1;
            this.snakeCanvas.Paint += new System.Windows.Forms.PaintEventHandler(this.snakeCanvas_Paint);
            // 
            // menuStrip2
            // 
            this.menuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.gameToolStripMenuItem});
            this.menuStrip2.Location = new System.Drawing.Point(0, 0);
            this.menuStrip2.Name = "menuStrip2";
            this.menuStrip2.Size = new System.Drawing.Size(300, 24);
            this.menuStrip2.TabIndex = 0;
            this.menuStrip2.Text = "menuStrip2";
            // 
            // gameToolStripMenuItem
            // 
            this.gameToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newGameToolStripMenuItem,
            this.stopToolStripMenuItem,
            this.startToolStripMenuItem,
            this.bestScoresToolStripMenuItem1});
            this.gameToolStripMenuItem.Name = "gameToolStripMenuItem";
            this.gameToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.gameToolStripMenuItem.Text = "Game";
            // 
            // newGameToolStripMenuItem
            // 
            this.newGameToolStripMenuItem.Name = "newGameToolStripMenuItem";
            this.newGameToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.newGameToolStripMenuItem.Text = "New Game";
            this.newGameToolStripMenuItem.Click += new System.EventHandler(this.newGameToolStripMenuItem_Click);
            // 
            // stopToolStripMenuItem
            // 
            this.stopToolStripMenuItem.Name = "stopToolStripMenuItem";
            this.stopToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.stopToolStripMenuItem.Text = "Stop";
            this.stopToolStripMenuItem.Click += new System.EventHandler(this.stopToolStripMenuItem_Click);
            // 
            // startToolStripMenuItem
            // 
            this.startToolStripMenuItem.Name = "startToolStripMenuItem";
            this.startToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.startToolStripMenuItem.Text = "Start";
            this.startToolStripMenuItem.Click += new System.EventHandler(this.startToolStripMenuItem_Click);
            // 
            // bestScoresToolStripMenuItem1
            // 
            this.bestScoresToolStripMenuItem1.Name = "bestScoresToolStripMenuItem1";
            this.bestScoresToolStripMenuItem1.Size = new System.Drawing.Size(133, 22);
            this.bestScoresToolStripMenuItem1.Text = "Best Scores";
            this.bestScoresToolStripMenuItem1.Click += new System.EventHandler(this.bestScoresToolStripMenuItem1_Click);
            // 
            // bestScoresPanel
            // 
            this.bestScoresPanel.BackgroundImage = global::Snake.Properties.Resources.background;
            this.bestScoresPanel.Controls.Add(this.label2);
            this.bestScoresPanel.Controls.Add(this.bestScoresList);
            this.bestScoresPanel.Controls.Add(this.OKButton);
            this.bestScoresPanel.Controls.Add(this.menuStrip3);
            this.bestScoresPanel.Location = new System.Drawing.Point(0, 0);
            this.bestScoresPanel.Name = "bestScoresPanel";
            this.bestScoresPanel.Size = new System.Drawing.Size(300, 359);
            this.bestScoresPanel.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Arial Narrow", 15F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label2.Location = new System.Drawing.Point(81, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(129, 24);
            this.label2.TabIndex = 3;
            this.label2.Text = "BEST SCORES";
            // 
            // bestScoresList
            // 
            this.bestScoresList.Font = new System.Drawing.Font("Berlin Sans FB", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.bestScoresList.FormattingEnabled = true;
            this.bestScoresList.ItemHeight = 18;
            this.bestScoresList.Location = new System.Drawing.Point(35, 97);
            this.bestScoresList.Name = "bestScoresList";
            this.bestScoresList.Size = new System.Drawing.Size(229, 148);
            this.bestScoresList.TabIndex = 2;
            // 
            // OKButton
            // 
            this.OKButton.Location = new System.Drawing.Point(111, 275);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(75, 23);
            this.OKButton.TabIndex = 1;
            this.OKButton.Text = "OK";
            this.OKButton.UseVisualStyleBackColor = true;
            this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
            // 
            // menuStrip3
            // 
            this.menuStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.gameToolStripMenuItem1});
            this.menuStrip3.Location = new System.Drawing.Point(0, 0);
            this.menuStrip3.Name = "menuStrip3";
            this.menuStrip3.Size = new System.Drawing.Size(300, 24);
            this.menuStrip3.TabIndex = 0;
            this.menuStrip3.Text = "menuStrip3";
            // 
            // gameToolStripMenuItem1
            // 
            this.gameToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newGameToolStripMenuItem1});
            this.gameToolStripMenuItem1.Name = "gameToolStripMenuItem1";
            this.gameToolStripMenuItem1.Size = new System.Drawing.Size(50, 20);
            this.gameToolStripMenuItem1.Text = "Game";
            // 
            // newGameToolStripMenuItem1
            // 
            this.newGameToolStripMenuItem1.Name = "newGameToolStripMenuItem1";
            this.newGameToolStripMenuItem1.Size = new System.Drawing.Size(132, 22);
            this.newGameToolStripMenuItem1.Text = "New Game";
            this.newGameToolStripMenuItem1.Click += new System.EventHandler(this.newGameToolStripMenuItem1_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 328);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(300, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(118, 17);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(299, 351);
            this.Controls.Add(this.newGamePanel);
            this.Controls.Add(this.snakeCanvas);
            this.Controls.Add(this.bestScoresPanel);
            this.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Snake Game";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.snakeCanvas_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.snakeCanvas_KeyUp);
            this.newGamePanel.ResumeLayout(false);
            this.newGamePanel.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.snakeCanvas.ResumeLayout(false);
            this.snakeCanvas.PerformLayout();
            this.menuStrip2.ResumeLayout(false);
            this.menuStrip2.PerformLayout();
            this.bestScoresPanel.ResumeLayout(false);
            this.bestScoresPanel.PerformLayout();
            this.menuStrip3.ResumeLayout(false);
            this.menuStrip3.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel newGamePanel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox nameBox;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem scoresToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bestScoresToolStripMenuItem;
        private System.Windows.Forms.Panel snakeCanvas;
        private System.Windows.Forms.MenuStrip menuStrip2;
        private System.Windows.Forms.ToolStripMenuItem gameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newGameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stopToolStripMenuItem;
        private System.Windows.Forms.Panel bestScoresPanel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox bestScoresList;
        private System.Windows.Forms.Button OKButton;
        private System.Windows.Forms.MenuStrip menuStrip3;
        private System.Windows.Forms.ToolStripMenuItem gameToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem newGameToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem startToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bestScoresToolStripMenuItem1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
    }
}

