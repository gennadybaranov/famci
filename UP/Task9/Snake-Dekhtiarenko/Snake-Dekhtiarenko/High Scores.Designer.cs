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
            this.SuspendLayout();
            // 
            // ScoresList
            // 
            this.ScoresList.BackColor = System.Drawing.Color.MediumPurple;
            this.ScoresList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ScoresList.Font = new System.Drawing.Font("Sylfaen", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ScoresList.FormattingEnabled = true;
            this.ScoresList.ItemHeight = 27;
            this.ScoresList.Location = new System.Drawing.Point(12, 12);
            this.ScoresList.Name = "ScoresList";
            this.ScoresList.Size = new System.Drawing.Size(427, 326);
            this.ScoresList.TabIndex = 0;
            this.ScoresList.SelectedIndexChanged += new System.EventHandler(this.ScoresList_SelectedIndexChanged);
            // 
            // High_Scores
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.MediumPurple;
            this.ClientSize = new System.Drawing.Size(451, 362);
            this.Controls.Add(this.ScoresList);
            this.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "High_Scores";
            this.Text = "High_Scores";
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.ListBox ScoresList;
    }
}