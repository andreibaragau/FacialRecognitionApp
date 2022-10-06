namespace Facial_Recognition_App
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
            this.picCapture = new System.Windows.Forms.PictureBox();
            this.btnCapture = new System.Windows.Forms.Button();
            this.btnDetectFaces = new System.Windows.Forms.Button();
            this.txtPersonName = new System.Windows.Forms.TextBox();
            this.btnAddPerson = new System.Windows.Forms.Button();
            this.btnTrain = new System.Windows.Forms.Button();
            this.picDetected = new System.Windows.Forms.PictureBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.resultLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.personsLabel = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.btnMaskDetect = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.picCapture)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picDetected)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.panel3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // picCapture
            // 
            this.picCapture.Location = new System.Drawing.Point(196, 141);
            this.picCapture.Margin = new System.Windows.Forms.Padding(4);
            this.picCapture.Name = "picCapture";
            this.picCapture.Size = new System.Drawing.Size(570, 406);
            this.picCapture.TabIndex = 0;
            this.picCapture.TabStop = false;
            // 
            // btnCapture
            // 
            this.btnCapture.Location = new System.Drawing.Point(43, 8);
            this.btnCapture.Margin = new System.Windows.Forms.Padding(4);
            this.btnCapture.Name = "btnCapture";
            this.btnCapture.Size = new System.Drawing.Size(153, 49);
            this.btnCapture.TabIndex = 1;
            this.btnCapture.Text = "1. Pornire flux video";
            this.btnCapture.UseVisualStyleBackColor = true;
            this.btnCapture.Click += new System.EventHandler(this.btnCapture_Click);
            // 
            // btnDetectFaces
            // 
            this.btnDetectFaces.Location = new System.Drawing.Point(43, 65);
            this.btnDetectFaces.Margin = new System.Windows.Forms.Padding(4);
            this.btnDetectFaces.Name = "btnDetectFaces";
            this.btnDetectFaces.Size = new System.Drawing.Size(153, 49);
            this.btnDetectFaces.TabIndex = 2;
            this.btnDetectFaces.Text = "2. Detectare faciala";
            this.btnDetectFaces.UseVisualStyleBackColor = true;
            this.btnDetectFaces.Click += new System.EventHandler(this.btnDetectFaces_Click);
            // 
            // txtPersonName
            // 
            this.txtPersonName.Location = new System.Drawing.Point(43, 251);
            this.txtPersonName.Margin = new System.Windows.Forms.Padding(4);
            this.txtPersonName.Name = "txtPersonName";
            this.txtPersonName.Size = new System.Drawing.Size(152, 22);
            this.txtPersonName.TabIndex = 3;
            // 
            // btnAddPerson
            // 
            this.btnAddPerson.Location = new System.Drawing.Point(43, 187);
            this.btnAddPerson.Margin = new System.Windows.Forms.Padding(4);
            this.btnAddPerson.Name = "btnAddPerson";
            this.btnAddPerson.Size = new System.Drawing.Size(153, 53);
            this.btnAddPerson.TabIndex = 4;
            this.btnAddPerson.Text = "Adaugare Persoana";
            this.btnAddPerson.UseVisualStyleBackColor = true;
            this.btnAddPerson.Click += new System.EventHandler(this.btnAddPerson_Click);
            // 
            // btnTrain
            // 
            this.btnTrain.Location = new System.Drawing.Point(43, 330);
            this.btnTrain.Margin = new System.Windows.Forms.Padding(4);
            this.btnTrain.Name = "btnTrain";
            this.btnTrain.Size = new System.Drawing.Size(153, 50);
            this.btnTrain.TabIndex = 5;
            this.btnTrain.Text = "4. Recunoastere faciala";
            this.btnTrain.UseVisualStyleBackColor = true;
            this.btnTrain.Click += new System.EventHandler(this.btnTrain_Click);
            // 
            // picDetected
            // 
            this.picDetected.Location = new System.Drawing.Point(5, 284);
            this.picDetected.Margin = new System.Windows.Forms.Padding(4);
            this.picDetected.Name = "picDetected";
            this.picDetected.Size = new System.Drawing.Size(148, 108);
            this.picDetected.TabIndex = 7;
            this.picDetected.TabStop = false;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(43, 284);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(153, 28);
            this.btnSave.TabIndex = 8;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(16, 388);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(108, 113);
            this.pictureBox1.TabIndex = 9;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Location = new System.Drawing.Point(132, 388);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(108, 113);
            this.pictureBox2.TabIndex = 10;
            this.pictureBox2.TabStop = false;
            // 
            // resultLabel
            // 
            this.resultLabel.AutoSize = true;
            this.resultLabel.Location = new System.Drawing.Point(471, 621);
            this.resultLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.resultLabel.Name = "resultLabel";
            this.resultLabel.Size = new System.Drawing.Size(13, 17);
            this.resultLabel.TabIndex = 11;
            this.resultLabel.Text = "-";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(269, 581);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(149, 17);
            this.label1.TabIndex = 12;
            this.label1.Text = "Persoana identificata: ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(269, 621);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(196, 17);
            this.label2.TabIndex = 14;
            this.label2.Text = "Toate persoanele identificate:";
            // 
            // personsLabel
            // 
            this.personsLabel.AutoSize = true;
            this.personsLabel.Location = new System.Drawing.Point(471, 581);
            this.personsLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.personsLabel.Name = "personsLabel";
            this.personsLabel.Size = new System.Drawing.Size(13, 17);
            this.personsLabel.TabIndex = 15;
            this.personsLabel.Text = "-";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Silver;
            this.panel3.Controls.Add(this.label4);
            this.panel3.Location = new System.Drawing.Point(-2, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1036, 134);
            this.panel3.TabIndex = 16;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Century Gothic", 25.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label4.Location = new System.Drawing.Point(120, 39);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(691, 52);
            this.label4.TabIndex = 0;
            this.label4.Text = "Detectie si recunoastere faciala";
            // 
            // btnMaskDetect
            // 
            this.btnMaskDetect.Location = new System.Drawing.Point(43, 121);
            this.btnMaskDetect.Name = "btnMaskDetect";
            this.btnMaskDetect.Size = new System.Drawing.Size(153, 48);
            this.btnMaskDetect.TabIndex = 1;
            this.btnMaskDetect.Text = "3. Detectare masca";
            this.btnMaskDetect.UseVisualStyleBackColor = true;
            this.btnMaskDetect.Click += new System.EventHandler(this.btnMaskDetect_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.button1.Location = new System.Drawing.Point(24, 209);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(118, 55);
            this.button1.TabIndex = 13;
            this.button1.Text = "Mail";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(24, 89);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(118, 51);
            this.button2.TabIndex = 17;
            this.button2.Text = "Profil";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(24, 146);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(118, 55);
            this.button3.TabIndex = 18;
            this.button3.Text = "Istoric";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(69)))), ((int)(((byte)(79)))));
            this.panel1.Controls.Add(this.button3);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.picDetected);
            this.panel1.Location = new System.Drawing.Point(-2, 137);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(184, 532);
            this.panel1.TabIndex = 19;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(69)))), ((int)(((byte)(79)))));
            this.panel2.Controls.Add(this.btnMaskDetect);
            this.panel2.Controls.Add(this.pictureBox2);
            this.panel2.Controls.Add(this.pictureBox1);
            this.panel2.Controls.Add(this.btnSave);
            this.panel2.Controls.Add(this.btnTrain);
            this.panel2.Controls.Add(this.btnAddPerson);
            this.panel2.Controls.Add(this.txtPersonName);
            this.panel2.Controls.Add(this.btnDetectFaces);
            this.panel2.Controls.Add(this.btnCapture);
            this.panel2.Location = new System.Drawing.Point(773, 137);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(253, 532);
            this.panel2.TabIndex = 20;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(120)))), ((int)(((byte)(138)))));
            this.ClientSize = new System.Drawing.Size(1028, 674);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.personsLabel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.resultLabel);
            this.Controls.Add(this.picCapture);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.picCapture)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picDetected)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picCapture;
        private System.Windows.Forms.Button btnCapture;
        private System.Windows.Forms.Button btnDetectFaces;
        private System.Windows.Forms.TextBox txtPersonName;
        private System.Windows.Forms.Button btnAddPerson;
        private System.Windows.Forms.Button btnTrain;
        private System.Windows.Forms.PictureBox picDetected;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label resultLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label personsLabel;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnMaskDetect;
    }
}

