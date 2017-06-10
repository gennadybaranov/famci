namespace Snake
{
    partial class MainForm
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
            this.panel_highscores = new System.Windows.Forms.Panel();
            this.btn_restart = new System.Windows.Forms.Button();
            this.label_curScore = new System.Windows.Forms.Label();
            this.label_forCurScore = new System.Windows.Forms.Label();
            this.pg = new System.Windows.Forms.Panel();
            this.panel_start = new System.Windows.Forms.Panel();
            this.btn_start = new System.Windows.Forms.Button();
            this.tb_name = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel_highscores.SuspendLayout();
            this.panel_start.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel_highscores
            // 
            this.panel_highscores.BackColor = System.Drawing.Color.CornflowerBlue;
            this.panel_highscores.Controls.Add(this.btn_restart);
            this.panel_highscores.Controls.Add(this.label_curScore);
            this.panel_highscores.Controls.Add(this.label_forCurScore);
            this.panel_highscores.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_highscores.Location = new System.Drawing.Point(0, 0);
            this.panel_highscores.Margin = new System.Windows.Forms.Padding(2);
            this.panel_highscores.Name = "panel_highscores";
            this.panel_highscores.Size = new System.Drawing.Size(454, 509);
            this.panel_highscores.TabIndex = 0;
            // 
            // btn_restart
            // 
            this.btn_restart.Font = new System.Drawing.Font("Lucida Sans Unicode", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btn_restart.Location = new System.Drawing.Point(328, 102);
            this.btn_restart.Margin = new System.Windows.Forms.Padding(2);
            this.btn_restart.Name = "btn_restart";
            this.btn_restart.Size = new System.Drawing.Size(105, 32);
            this.btn_restart.TabIndex = 5;
            this.btn_restart.Text = "Retry";
            this.btn_restart.UseVisualStyleBackColor = true;
            this.btn_restart.Click += new System.EventHandler(this.btn_restart_Click);
            // 
            // label_curScore
            // 
            this.label_curScore.AutoSize = true;
            this.label_curScore.Font = new System.Drawing.Font("Lucida Sans Unicode", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_curScore.ForeColor = System.Drawing.Color.White;
            this.label_curScore.Location = new System.Drawing.Point(250, 102);
            this.label_curScore.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_curScore.Name = "label_curScore";
            this.label_curScore.Size = new System.Drawing.Size(32, 34);
            this.label_curScore.TabIndex = 4;
            this.label_curScore.Text = "0";
            // 
            // label_forCurScore
            // 
            this.label_forCurScore.AutoSize = true;
            this.label_forCurScore.Font = new System.Drawing.Font("Lucida Sans Unicode", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_forCurScore.ForeColor = System.Drawing.Color.White;
            this.label_forCurScore.Location = new System.Drawing.Point(60, 102);
            this.label_forCurScore.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_forCurScore.Name = "label_forCurScore";
            this.label_forCurScore.Size = new System.Drawing.Size(167, 34);
            this.label_forCurScore.TabIndex = 3;
            this.label_forCurScore.Text = "Your Score:";
            // 
            // pg
            // 
            this.pg.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.pg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pg.Location = new System.Drawing.Point(0, 0);
            this.pg.Margin = new System.Windows.Forms.Padding(2);
            this.pg.Name = "pg";
            this.pg.Size = new System.Drawing.Size(454, 509);
            this.pg.TabIndex = 2;
            this.pg.Paint += new System.Windows.Forms.PaintEventHandler(this.pg_Paint);
            // 
            // panel_start
            // 
            this.panel_start.BackColor = System.Drawing.Color.AliceBlue;
            this.panel_start.Controls.Add(this.btn_start);
            this.panel_start.Controls.Add(this.tb_name);
            this.panel_start.Controls.Add(this.label2);
            this.panel_start.Controls.Add(this.label1);
            this.panel_start.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_start.Location = new System.Drawing.Point(0, 0);
            this.panel_start.Margin = new System.Windows.Forms.Padding(2);
            this.panel_start.Name = "panel_start";
            this.panel_start.Size = new System.Drawing.Size(454, 509);
            this.panel_start.TabIndex = 1;
            // 
            // btn_start
            // 
            this.btn_start.Enabled = false;
            this.btn_start.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btn_start.Location = new System.Drawing.Point(154, 393);
            this.btn_start.Margin = new System.Windows.Forms.Padding(2);
            this.btn_start.Name = "btn_start";
            this.btn_start.Size = new System.Drawing.Size(178, 37);
            this.btn_start.TabIndex = 3;
            this.btn_start.Text = "Start";
            this.btn_start.UseVisualStyleBackColor = true;
            this.btn_start.Click += new System.EventHandler(this.btn_start_Click);
            // 
            // tb_name
            // 
            this.tb_name.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tb_name.Location = new System.Drawing.Point(168, 281);
            this.tb_name.Margin = new System.Windows.Forms.Padding(2);
            this.tb_name.Name = "tb_name";
            this.tb_name.Size = new System.Drawing.Size(143, 32);
            this.tb_name.TabIndex = 2;
            this.tb_name.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tb_name_KeyUp);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Lucida Sans Unicode", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(130, 214);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(238, 34);
            this.label2.TabIndex = 1;
            this.label2.Text = "Enter Your Name";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Lucida Sans Unicode", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(83, 55);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(330, 78);
            this.label1.TabIndex = 0;
            this.label1.Text = "Welcome\r\nto the XSnakeGame!";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Aqua;
            this.ClientSize = new System.Drawing.Size(454, 509);
            this.Controls.Add(this.pg);
            this.Controls.Add(this.panel_start);
            this.Controls.Add(this.panel_highscores);
            this.DoubleBuffered = true;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "XSnakeGame";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
            this.panel_highscores.ResumeLayout(false);
            this.panel_highscores.PerformLayout();
            this.panel_start.ResumeLayout(false);
            this.panel_start.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel_highscores;
        private System.Windows.Forms.Panel pg;
        private System.Windows.Forms.Panel panel_start;
        private System.Windows.Forms.Button btn_start;
        private System.Windows.Forms.TextBox tb_name;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label_curScore;
        private System.Windows.Forms.Label label_forCurScore;
        private System.Windows.Forms.Button btn_restart;
    }
}

