namespace Backgammon
{
    partial class StartRollMenuForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.ColorPlayer1 = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.ColorPlayer2 = new System.Windows.Forms.ListBox();
            this.ColorSubmitPlayer1 = new System.Windows.Forms.Button();
            this.ColorSubmitPlayer2 = new System.Windows.Forms.Button();
            this.player2Roll = new System.Windows.Forms.Button();
            this.player1Roll = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.StartGame = new System.Windows.Forms.Button();
            this.playerFirstTxt = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(285, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(228, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Roll higher to start first";
            // 
            // ColorPlayer1
            // 
            this.ColorPlayer1.FormattingEnabled = true;
            this.ColorPlayer1.Items.AddRange(new object[] {
            "Red",
            "Black",
            "White",
            "Blue",
            "Yellow",
            "Green",
            "Brown",
            "Purple"});
            this.ColorPlayer1.Location = new System.Drawing.Point(58, 86);
            this.ColorPlayer1.Name = "ColorPlayer1";
            this.ColorPlayer1.Size = new System.Drawing.Size(88, 134);
            this.ColorPlayer1.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(24, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(155, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "Player1 choose color";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(603, 63);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(155, 20);
            this.label3.TabIndex = 4;
            this.label3.Text = "Player2 choose color";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // ColorPlayer2
            // 
            this.ColorPlayer2.FormattingEnabled = true;
            this.ColorPlayer2.Items.AddRange(new object[] {
            "Red",
            "Black",
            "White",
            "Blue",
            "Yellow",
            "Green",
            "Brown",
            "Purple"});
            this.ColorPlayer2.Location = new System.Drawing.Point(638, 86);
            this.ColorPlayer2.Name = "ColorPlayer2";
            this.ColorPlayer2.Size = new System.Drawing.Size(88, 134);
            this.ColorPlayer2.TabIndex = 3;
            this.ColorPlayer2.SelectedIndexChanged += new System.EventHandler(this.listBox2_SelectedIndexChanged);
            // 
            // ColorSubmitPlayer1
            // 
            this.ColorSubmitPlayer1.Location = new System.Drawing.Point(58, 226);
            this.ColorSubmitPlayer1.Name = "ColorSubmitPlayer1";
            this.ColorSubmitPlayer1.Size = new System.Drawing.Size(75, 23);
            this.ColorSubmitPlayer1.TabIndex = 5;
            this.ColorSubmitPlayer1.Text = "Submit";
            this.ColorSubmitPlayer1.UseVisualStyleBackColor = true;
            this.ColorSubmitPlayer1.Click += new System.EventHandler(this.ColorSubmitPlayer1_Click);
            // 
            // ColorSubmitPlayer2
            // 
            this.ColorSubmitPlayer2.Location = new System.Drawing.Point(638, 226);
            this.ColorSubmitPlayer2.Name = "ColorSubmitPlayer2";
            this.ColorSubmitPlayer2.Size = new System.Drawing.Size(75, 23);
            this.ColorSubmitPlayer2.TabIndex = 6;
            this.ColorSubmitPlayer2.Text = "Submit";
            this.ColorSubmitPlayer2.UseVisualStyleBackColor = true;
            this.ColorSubmitPlayer2.Click += new System.EventHandler(this.ColorSubmitPlayer2_Click);
            // 
            // player2Roll
            // 
            this.player2Roll.Enabled = false;
            this.player2Roll.Location = new System.Drawing.Point(502, 86);
            this.player2Roll.Name = "player2Roll";
            this.player2Roll.Size = new System.Drawing.Size(75, 23);
            this.player2Roll.TabIndex = 7;
            this.player2Roll.Text = "Roll";
            this.player2Roll.UseVisualStyleBackColor = true;
            this.player2Roll.Click += new System.EventHandler(this.player2Roll_Click);
            // 
            // player1Roll
            // 
            this.player1Roll.Enabled = false;
            this.player1Roll.Location = new System.Drawing.Point(195, 86);
            this.player1Roll.Name = "player1Roll";
            this.player1Roll.Size = new System.Drawing.Size(75, 23);
            this.player1Roll.TabIndex = 8;
            this.player1Roll.Text = "Roll";
            this.player1Roll.UseVisualStyleBackColor = true;
            this.player1Roll.Click += new System.EventHandler(this.player1Roll_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(183, 143);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(100, 50);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 9;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Location = new System.Drawing.Point(489, 143);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(100, 50);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox2.TabIndex = 10;
            this.pictureBox2.TabStop = false;
            // 
            // StartGame
            // 
            this.StartGame.Enabled = false;
            this.StartGame.Location = new System.Drawing.Point(343, 362);
            this.StartGame.Name = "StartGame";
            this.StartGame.Size = new System.Drawing.Size(75, 23);
            this.StartGame.TabIndex = 11;
            this.StartGame.Text = "Start Game";
            this.StartGame.UseVisualStyleBackColor = true;
            this.StartGame.Click += new System.EventHandler(this.StartGame_Click);
            // 
            // playerFirstTxt
            // 
            this.playerFirstTxt.AutoSize = true;
            this.playerFirstTxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.playerFirstTxt.Location = new System.Drawing.Point(261, 290);
            this.playerFirstTxt.Name = "playerFirstTxt";
            this.playerFirstTxt.Size = new System.Drawing.Size(261, 31);
            this.playerFirstTxt.TabIndex = 12;
            this.playerFirstTxt.Text = "Player 1 starts first";
            this.playerFirstTxt.Visible = false;
            // 
            // StartRollMenuForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.playerFirstTxt);
            this.Controls.Add(this.StartGame);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.player1Roll);
            this.Controls.Add(this.player2Roll);
            this.Controls.Add(this.ColorSubmitPlayer2);
            this.Controls.Add(this.ColorSubmitPlayer1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.ColorPlayer2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ColorPlayer1);
            this.Controls.Add(this.label1);
            this.Name = "StartRollMenuForm";
            this.Text = "StartRollMenuForm";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox ColorPlayer1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox ColorPlayer2;
        private System.Windows.Forms.Button ColorSubmitPlayer1;
        private System.Windows.Forms.Button ColorSubmitPlayer2;
        private System.Windows.Forms.Button player2Roll;
        private System.Windows.Forms.Button player1Roll;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Button StartGame;
        private System.Windows.Forms.Label playerFirstTxt;
    }
}