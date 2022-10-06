namespace Facial_Recognition_App
{
    partial class UserControlDays
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.dayLabel = new System.Windows.Forms.Label();
            this.eventLabel = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // dayLabel
            // 
            this.dayLabel.AutoSize = true;
            this.dayLabel.Location = new System.Drawing.Point(52, 20);
            this.dayLabel.Name = "dayLabel";
            this.dayLabel.Size = new System.Drawing.Size(24, 17);
            this.dayLabel.TabIndex = 0;
            this.dayLabel.Text = "00";
            // 
            // eventLabel
            // 
            this.eventLabel.Location = new System.Drawing.Point(3, 37);
            this.eventLabel.Name = "eventLabel";
            this.eventLabel.Size = new System.Drawing.Size(132, 53);
            this.eventLabel.TabIndex = 1;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // UserControlDays
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.Controls.Add(this.eventLabel);
            this.Controls.Add(this.dayLabel);
            this.Name = "UserControlDays";
            this.Size = new System.Drawing.Size(147, 98);
            this.Load += new System.EventHandler(this.UserControlDays_Load);
            this.Click += new System.EventHandler(this.UserControlDays_Click);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label dayLabel;
        private System.Windows.Forms.Label eventLabel;
        private System.Windows.Forms.Timer timer1;
    }
}
