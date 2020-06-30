using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Backgammon
{
    public partial class StartRollMenuForm : Form
    {
        public StartRollMenuForm()
        {
            InitializeComponent();
        }
        

        
        

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
        public String submitedPlayer1Color { get; set; }
        public String submitedPlayer2Color { get; set; }
        private void ColorSubmitPlayer1_Click(object sender, EventArgs e)
        {
            
            
            
            submitedPlayer1Color = ColorPlayer1.SelectedItem.ToString();
            ColorPlayer1.Enabled = false;
            if (submitedPlayer1Color!=null && submitedPlayer1Color.Length!=0 && submitedPlayer2Color != null && submitedPlayer2Color.Length != 0 && submitedPlayer1Color==submitedPlayer2Color) 
            {
                MessageBox.Show(String.Format("You must choose a different color"));
                ColorPlayer1.Enabled = true;
            }
            playersReady();
            
        }

        private void ColorSubmitPlayer2_Click(object sender, EventArgs e)
        {
            submitedPlayer2Color = ColorPlayer2.SelectedItem.ToString();
            ColorPlayer2.Enabled = false;
            if (submitedPlayer1Color != null && submitedPlayer1Color.Length != 0 && submitedPlayer2Color != null && submitedPlayer2Color.Length != 0 && submitedPlayer1Color == submitedPlayer2Color)
            {
                MessageBox.Show(String.Format("You must choose a different color"));
                ColorPlayer2.Enabled = true ;
            }
            playersReady();
        }
        public String submitedColorPlayer1()
        {
            return ColorPlayer1.SelectedItem.ToString();
        }
        private void playersReady()
        {
            if (submitedPlayer1Color != null && submitedPlayer1Color.Length != 0 && submitedPlayer2Color != null && submitedPlayer2Color.Length != 0 && submitedPlayer1Color != submitedPlayer2Color)
            {
                player1Roll.Enabled = true;
                player2Roll.Enabled = true;
                ColorSubmitPlayer1.Enabled = false;
                ColorSubmitPlayer2.Enabled = false;

            }
            
        }

        public Boolean player1Rolled { get;set; }
        
        public Boolean player2Rolled { get;set; }
        
        public int player1dice { get; set; }
        public int player2dice { get; set; }
        private void player1Roll_Click(object sender, EventArgs e)
        {
            Dice dice = new Dice();
            dice.rollDices();
            player1dice = dice.diceOne;
            updateDiceImages(dice.diceOne, pictureBox1);
            player1Roll.Enabled = false;
            player1Rolled = true;
            if (player1Roll.Enabled == false && player2Roll.Enabled == false && player1dice == player2dice)
            {
                MessageBox.Show(String.Format("One player must roll higher!"));
                player1Roll.Enabled = true; //kopcheto
                player2Roll.Enabled = true;
                player1Rolled = false; 
                player2Rolled = false;
            }
            else
            {
                startingPlayer();
            }
        }

        private void player2Roll_Click(object sender, EventArgs e)
        {
            Dice dice = new Dice();
            dice.rollDices();
            player2dice = dice.diceOne;
            updateDiceImages(dice.diceOne, pictureBox2);
            player2Roll.Enabled = false;
            player2Rolled = true;
            if (player1Roll.Enabled == false && player2Roll.Enabled == false && player1dice == player2dice)
            {
                MessageBox.Show(String.Format("One player must roll higher!"));
                player1Roll.Enabled = true;
                player2Roll.Enabled = true; // kopcheto
                player1Rolled = false; //bool
                player2Rolled = false;
            }
            else
            {
                startingPlayer();
            }
        }
        private void startingPlayer()
        {
            //if (player1dice > player2dice && player1dice!=0 && player1dice!=null &&  player2dice != 0 && player2dice != null)
            if(player1dice>player2dice && player1Rolled & player2Rolled)
            {
                playerFirstTxt.Text = "Player 1 starts first";
                playerFirstTxt.Visible = true;
                StartGame.Enabled = true;
            }
            else if (player1dice < player2dice && player1Rolled & player2Rolled)
            {
                playerFirstTxt.Text = "Player 2 starts first";
                playerFirstTxt.Visible = true;
                StartGame.Enabled = true;
            }
        }
        
        private void updateDiceImages(int dice, PictureBox pictureBox)
        {
            if (dice == 1)
            {
                pictureBox.Image = global::Backgammon.Properties.Resources.Die1;

            }
            if (dice == 2)
            {
                pictureBox.Image = global::Backgammon.Properties.Resources.Die2;

            }
            if (dice == 3)
            {
                pictureBox.Image = global::Backgammon.Properties.Resources.Die3;
            }
            if (dice == 4)
            {
                pictureBox.Image = global::Backgammon.Properties.Resources.Die4;
            }
            if (dice == 5)
            {
                pictureBox.Image = global::Backgammon.Properties.Resources.Die5;
            }
            if (dice == 6)
            {
                pictureBox.Image = global::Backgammon.Properties.Resources.Die6;
            }

        }

        private void StartGame_Click(object sender, EventArgs e)
        {
            this.Hide();
            BackgammonForm backgammonForm = new BackgammonForm(this);             
            backgammonForm.Show();
            
        }
    }
}
