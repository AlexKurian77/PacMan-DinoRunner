namespace Dino_Game
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
            this.txtScore = new System.Windows.Forms.Label();
            this.gameTimer = new System.Windows.Forms.Timer(this.components);
            this.playButton = new System.Windows.Forms.Label();
            this.textLabel = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.maxScore = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.trex = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trex)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // txtScore
            // 
            this.txtScore.AutoSize = true;
            this.txtScore.Font = new System.Drawing.Font("Consolas", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtScore.Location = new System.Drawing.Point(22, 20);
            this.txtScore.Name = "txtScore";
            this.txtScore.Size = new System.Drawing.Size(135, 33);
            this.txtScore.TabIndex = 2;
            this.txtScore.Text = "Score: 0";
            // 
            // gameTimer
            // 
            this.gameTimer.Interval = 20;
            this.gameTimer.Tick += new System.EventHandler(this.MainGameTimeEvent);
            // 
            // playButton
            // 
            this.playButton.BackColor = System.Drawing.SystemColors.InfoText;
            this.playButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.playButton.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.playButton.Location = new System.Drawing.Point(85, 57);
            this.playButton.Name = "playButton";
            this.playButton.Size = new System.Drawing.Size(147, 45);
            this.playButton.TabIndex = 0;
            this.playButton.Text = "Play Game";
            this.playButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.playButton.Click += new System.EventHandler(this.playButton_Click);
            // 
            // textLabel
            // 
            this.textLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textLabel.Location = new System.Drawing.Point(3, 126);
            this.textLabel.Name = "textLabel";
            this.textLabel.Size = new System.Drawing.Size(313, 41);
            this.textLabel.TabIndex = 1;
            this.textLabel.Text = "Use Space Bar";
            this.textLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.textLabel);
            this.panel1.Controls.Add(this.playButton);
            this.panel1.Location = new System.Drawing.Point(249, 114);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(318, 171);
            this.panel1.TabIndex = 3;
            // 
            // maxScore
            // 
            this.maxScore.Font = new System.Drawing.Font("Consolas", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.maxScore.Location = new System.Drawing.Point(526, 20);
            this.maxScore.Name = "maxScore";
            this.maxScore.Size = new System.Drawing.Size(227, 42);
            this.maxScore.TabIndex = 4;
            this.maxScore.Text = "Max Score: 0";
            this.maxScore.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Black;
            this.pictureBox1.Location = new System.Drawing.Point(-9, 402);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(816, 50);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox4
            // 
            this.pictureBox4.Image = global::Dino_Game.Properties.Resources.alexpolice;
            this.pictureBox4.Location = new System.Drawing.Point(621, 337);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(60, 66);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox4.TabIndex = 1;
            this.pictureBox4.TabStop = false;
            this.pictureBox4.Tag = "obstacle";
            // 
            // trex
            // 
            this.trex.Image = global::Dino_Game.Properties.Resources.Aditya;
            this.trex.Location = new System.Drawing.Point(124, 337);
            this.trex.Margin = new System.Windows.Forms.Padding(0);
            this.trex.Name = "trex";
            this.trex.Size = new System.Drawing.Size(51, 66);
            this.trex.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.trex.TabIndex = 1;
            this.trex.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::Dino_Game.Properties.Resources.policecare;
            this.pictureBox2.Location = new System.Drawing.Point(379, 351);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(104, 52);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 1;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Tag = "obstacle";
            this.pictureBox2.Click += new System.EventHandler(this.pictureBox2_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.trex);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.maxScore);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.txtScore);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.pictureBox4);
            this.Name = "Form1";
            this.Text = "Dino Runner";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.keyisdown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.keyisup);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trex)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox trex;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.Label txtScore;
        private System.Windows.Forms.Timer gameTimer;
        private System.Windows.Forms.Label playButton;
        private System.Windows.Forms.Label textLabel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label maxScore;
    }
}

