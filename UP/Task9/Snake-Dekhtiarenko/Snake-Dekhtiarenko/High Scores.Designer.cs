namespace Snake_Dekhtiarenko
{
    partial class High_Scores
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
            this.ScoresList = new System.Windows.Forms.ListBox();
            this.TopPlayer = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // ScoresList
            // 
            this.ScoresList.BackColor = System.Drawing.Color.MediumPurple;
            this.ScoresList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ScoresList.Font = new System.Drawing.Font("Sylfaen", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ScoresList.ForeColor = System.Drawing.Color.DarkSlateBlue;
            this.ScoresList.FormattingEnabled = true;
            this.ScoresList.ItemHeight = 27;
            this.ScoresList.Location = new System.Drawing.Point(12, 53);
            this.ScoresList.Name = "ScoresList";
            this.ScoresList.Size = new System.Drawing.Size(427, 297);
            this.ScoresList.TabIndex = 0;
            this.ScoresList.SelectedIndexChanged += new System.EventHandler(this.ScoresList_SelectedIndexChanged);
            // 
            // TopPlayer
            // 
            this.TopPlayer.BackColor = System.Drawing.Color.MediumPurple;
            this.TopPlayer.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TopPlayer.Font = new System.Drawing.Font("Sylfaen", 21.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TopPlayer.ForeColor = System.Drawing.Color.Indigo;
            this.TopPlayer.FormattingEnabled = true;
            this.TopPlayer.ItemHeight = 39;
            this.TopPlayer.Location = new System.Drawing.Point(12, 2);
            this.TopPlayer.Name = "TopPlayer";
            this.TopPlayer.Size = new System.Drawing.Size(427, 39);
            this.TopPlayer.TabIndex = 1;
            // 
            // High_Scores
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.MediumPurple;
            this.BackgroundImage = global::Snake_Dekhtiarenko.Properties.Resources.PhotoFunia_1496578224;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(451, 362);
            this.Controls.Add(this.TopPlayer);
            this.Controls.Add(this.ScoresList);
            this.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "High_Scores";
            this.Text = "High_Scores";
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.ListBox ScoresList;
        public System.Windows.Forms.ListBox TopPlayer;
    }
}