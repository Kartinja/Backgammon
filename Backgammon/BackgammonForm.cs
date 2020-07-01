using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Backgammon
{
    public partial class BackgammonForm : Form
    {


        public GameController gameController { get; set; }
        public Board board { get; set; }
        public List<PictureBox> picturePlacements = new List<PictureBox>(new PictureBox[24]);
        public List<PictureBox> pictureRemoveCheckersPlacements = new List<PictureBox>(new PictureBox[2]);




        public BackgammonForm(StartRollMenuForm form)
        {
            InitializeComponent();

            board = new Board(Color.FromName(form.submitedPlayer1Color), Color.FromName(form.submitedPlayer2Color));
            gameController = new GameController(form.submitedPlayer1Color, form.submitedPlayer2Color, board);
            if (form.player1dice > form.player2dice)
            {
                txtTurn.Text = string.Format("Turn: {0} player.", gameController.playerOne.color.ToString());
                gameController.playerOne.isMyTurn = true;
                gameController.playerTwo.isMyTurn = false;

            }
            else
            {
                txtTurn.Text = string.Format("Turn: {0} player.", gameController.playerTwo.color.ToString());
                gameController.playerTwo.isMyTurn = true;
                gameController.playerOne.isMyTurn = false;
            }

            //inicijalizacija na picturePlacements
            initializePictureBoxListReference();
            InitializePictureBoxPlacements();

        }
        public BackgammonForm(Board board)
        {
            this.board = board; ;
        }
        private void InitializePictureBoxPlacements()
        {
            for (int i = 0; i < 24; i++)
            {
                picturePlacements[i].Visible = true;
                picturePlacements[i].Click += new EventHandler(this.PlacementPictureBox_Click);
                picturePlacements[i].BackColor = Color.Transparent;
                //picturePlacements[i].Parent = pictureBoxBoard;
            }
            pictureRemoveCheckersPlacements[0].Visible = true;
            pictureRemoveCheckersPlacements[0].BackColor = Color.Transparent;
            pictureRemoveCheckersPlacements[0].Click += new EventHandler(this.playerOneRemovedCheckers_Click);
            pictureRemoveCheckersPlacements[1].Visible = true;
            pictureRemoveCheckersPlacements[1].BackColor = Color.Transparent;
            pictureRemoveCheckersPlacements[1].Click += new EventHandler(this.playerTwoRemovedCheckers_Click);

        }

        private void PlacementPictureBox_Click(object sender, EventArgs e)
        {
            //sekoj placement picturebox ja vika ovaa methoda
            int clickedPlacement = FindClickedPlacement((PictureBox)sender);
            if (gameController.rolledDices)
            {
                if (gameController.playerInitialPlacementChoice == null)
                {
                    tryGetAndExecuteInitialMove(clickedPlacement);
                }
                else if (clickedPlacement == gameController.playerInitialPlacementChoice)
                {
                    cancelInitialMove();
                    eventHandler.Text = string.Format("You have {0} plays left", gameController.numberOfMovesLeft);
                }
                else
                {// move to here
                    tryGetAndExecuteFinalMove(clickedPlacement);
                }
            }
            else
            {
                eventHandler.Text = "You need to role the dice first!";
            }

        }
        private void playerOneRemovedCheckers_Click(object sender, EventArgs e)
        {

            if (gameController.playerOne.checkersAtBar > 0 && gameController.playerOne.isMyTurn)
            {
                if (gameController.rolledDices)
                {
                    gameController.setPlayerInitialMove(-1);

                }
                else
                {
                    eventHandler.Text = " You need to roll the dice firsts!";
                }
            }
            else if (gameController.playerOne.canRemoveCheckers(board))
            {
                if (gameController.playerInitialPlacementChoice != null && gameController.isLegalRemoveMove(gameController.dice.diceOne))
                {
                    gameController.setRemoveCheckerMove(gameController.dice.diceOne);

                    if (gameController.playerOne.GetCheckersAtHome(board) == 0)
                    {
                        MessageBox.Show(string.Format("Player {0} won!", gameController.playerOne.color.ToString()));
                        StartRollMenuForm startRollMenuForm = new StartRollMenuForm();
                        startRollMenuForm.Show();
                        this.Close();
                    }
                    refreshUIAfterMove();
                }
                if (gameController.playerInitialPlacementChoice != null && gameController.isLegalRemoveMove(gameController.dice.diceTwo))
                {
                    gameController.setRemoveCheckerMove(gameController.dice.diceTwo);

                    if (gameController.playerOne.GetCheckersAtHome(board) == 0)
                    {
                        MessageBox.Show(string.Format("Player {0} won!", gameController.playerOne.color.ToString()));
                        StartRollMenuForm startRollMenuForm = new StartRollMenuForm();
                        startRollMenuForm.Show();
                        
                        this.Close();
                    }
                    refreshUIAfterMove();
                }
            }
        }
        private void playerTwoRemovedCheckers_Click(object sender, EventArgs e)
        {
            if (gameController.playerTwo.checkersAtBar > 0 && gameController.playerTwo.isMyTurn)
            {
                if (gameController.rolledDices)
                {
                    gameController.setPlayerInitialMove(24);

                }
                else
                {
                    eventHandler.Text = " You need to roll the dice firsts!";
                }
            }
            else if (gameController.playerTwo.canRemoveCheckers(board))
            {
                if (gameController.playerInitialPlacementChoice != null && gameController.isLegalRemoveMove(gameController.dice.diceOne))
                {
                    gameController.setRemoveCheckerMove(gameController.dice.diceOne);

                    if (gameController.playerTwo.GetCheckersAtHome(board) == 0)
                    {
                        MessageBox.Show(string.Format("Player {0} won!", gameController.playerTwo.color.ToString()));
                        StartRollMenuForm startRollMenuForm = new StartRollMenuForm();
                        startRollMenuForm.Show();
                        this.Close();
                    }
                    refreshUIAfterMove();
                }
                if (gameController.playerInitialPlacementChoice != null && gameController.isLegalRemoveMove(gameController.dice.diceTwo))
                {
                    gameController.setRemoveCheckerMove(gameController.dice.diceTwo);

                    if (gameController.playerTwo.GetCheckersAtHome(board) == 0)
                    {
                        MessageBox.Show(string.Format("Player {0} won!", gameController.playerTwo.color.ToString()));
                        StartRollMenuForm startRollMenuForm = new StartRollMenuForm();
                        startRollMenuForm.Show();
                        this.Close();

                    }
                    refreshUIAfterMove();
                }
            }
        }

        private void tryGetAndExecuteFinalMove(int clickedPlacement)
        {
            if (gameController.isLegalFinalMove(clickedPlacement))
            {
                handleLegalFinalMove(clickedPlacement);
            }
            else if (gameController.isLegalFinalMoveEat(clickedPlacement))
            {
                handleLegalFinalMoveEat(clickedPlacement);
            }
            else if (clickedPlacement == gameController.playerInitialPlacementChoice)
            {
                cancelInitialMove();
            }
            else
            {
                MessageBox.Show(string.Format("You can't move there."));
            }

        }

        private void handleLegalFinalMoveEat(int clickedPlacement)
        {
            gameController.setFinalMoveEat(clickedPlacement);
            if (gameController.playerHasAvailableMoves() && gameController.numberOfMovesLeft > 0)
            {
                eventHandler.Text = string.Format("You have {0} moves left", gameController.numberOfMovesLeft);
            }

            /* if (gameController.numberOfMovesLeft == 0)
             {
                 gameController.swapTurns();

             }*/
            refreshUIAfterMove();
        }

        private void handleLegalFinalMove(int clickedPlacement)
        {
            gameController.setPlayerFinalMove(clickedPlacement, false);
            if (gameController.playerHasAvailableMoves() && gameController.numberOfMovesLeft > 0)
            {
                eventHandler.Text = string.Format("You have {0} moves left", gameController.numberOfMovesLeft);
            }/*else if (!gameController.playerHasAvailableMoves() && gameController.numberOfMovesLeft == 0)
            {                 
                gameController.swapTurns();
            }*/
            refreshUIAfterMove();

        }

        private void cancelInitialMove()
        {
            gameController.setPlayerInitialMove(null);
            //da vidime mozhe ushe nesho

        }
        private void refreshUIAfterMove()
        {
            gameController.board.update();
            
            UIPanel.Refresh();

            if (gameController.numberOfMovesLeft == 0)
            {

                gameController.swapTurns();
                txtTurn.Text = string.Format("Turn: {0} player.", gameController.playerOne.isMyTurn ? gameController.playerOne.color.ToString() : gameController.playerTwo.color.ToString());
                eventHandler.Text = "Roll the dice.";
            }
            if (gameController.numberOfMovesLeft > 0 && !gameController.playerHasAvailableMoves())
            {
                MessageBox.Show("You have no legal moves left");
                gameController.swapTurns();
            }
            this.Refresh();
        }
        private void EvaluateInitialAvailableMoves()
        {
            if (gameController.playerHasAvailableMoves() || gameController.playerHasAvailableRemoveMoves())
            {
                gameController.UpdateMovesLeft();// ako ima pravo da se dvizhi;
            }
            else // ako nema pravo da se dvizhi
            {
                MessageBox.Show(string.Format("You have no available moves left."));
                //                gameController.swapTurns();
                refreshUIAfterMove();
            }
        }

        private void tryGetAndExecuteInitialMove(int clickedPlacement)
        {
            if (gameController.isLegalInitialMove(clickedPlacement))
            {
                gameController.setPlayerInitialMove(clickedPlacement);
                eventHandler.Text = string.Format("Waiting for you to finish your move");

            }
            else if ((gameController.playerOne.isMyTurn && gameController.playerOne.checkersAtBar > 0) ||
                   (gameController.playerTwo.isMyTurn && gameController.playerTwo.checkersAtBar > 0))
            {
                eventHandler.Text = "You can't move checkers if you have checkers on the bar";
            }
            else
            {
                eventHandler.Text = "You don't have checkers at that placement";
            }

        }

        private int FindClickedPlacement(PictureBox clickedPictureBox)
        {
            for (int i = 0; i <= 23; i++)
            {
                if ((picturePlacements[i].Left <= clickedPictureBox.Left) && (picturePlacements[i].Top <= clickedPictureBox.Top) && (picturePlacements[i].Bottom >= clickedPictureBox.Bottom) && (picturePlacements[i].Right >= clickedPictureBox.Right))
                {
                    return i;
                }
            }
            return -1;
        }

        public void initializePictureBoxListReference()
        {
            // kade treba da bidat picture placements;
            List<Point> plLocations = new List<Point>(new Point[24]);

            picturePlacements[0] = pbPlacement0;
            picturePlacements[1] = pbPlacement1;
            picturePlacements[2] = pbPlacement2;
            picturePlacements[3] = pbPlacement3;
            picturePlacements[4] = pbPlacement4;
            picturePlacements[5] = pbPlacement5;
            picturePlacements[6] = pbPlacement6;
            picturePlacements[7] = pbPlacement7;
            picturePlacements[8] = pbPlacement8;
            picturePlacements[9] = pbPlacement9;
            picturePlacements[10] = pbPlacement10;
            picturePlacements[11] = pbPlacement11;
            picturePlacements[12] = pbPlacement12;
            picturePlacements[13] = pbPlacement13;
            picturePlacements[14] = pbPlacement14;
            picturePlacements[15] = pbPlacement15;
            picturePlacements[16] = pbPlacement16;
            picturePlacements[17] = pbPlacement17;
            picturePlacements[18] = pbPlacement18;
            picturePlacements[19] = pbPlacement19;
            picturePlacements[20] = pbPlacement20;
            picturePlacements[21] = pbPlacement21;
            picturePlacements[22] = pbPlacement22;
            picturePlacements[23] = pbPlacement23;
            pictureRemoveCheckersPlacements[0] = pictureBoxPlayerOneRemovedCheckers;
            pictureRemoveCheckersPlacements[1] = pictureBoxPlayerTwoRemovedCheckers;


        }


        private void btnRoll_Click(object sender, EventArgs e)
        {


            if (!gameController.rolledDices)
            {
                gameController.rollDices();
                updateDiceImages(gameController.dice.diceOne, pictureBoxDice1);
                updateDiceImages(gameController.dice.diceTwo, pictureBoxDice2);
            }

            EvaluateInitialAvailableMoves();
            eventHandler.Text = string.Format("You have {0} plays left", gameController.numberOfMovesLeft);


        }

        private void updateDiceImages(int dice, PictureBox diePictureBox)
        {
            if (dice == 1)
            {
                diePictureBox.Image = global::Backgammon.Properties.Resources.Die1;

            }
            if (dice == 2)
            {
                diePictureBox.Image = global::Backgammon.Properties.Resources.Die2;

            }
            if (dice == 3)
            {
                diePictureBox.Image = global::Backgammon.Properties.Resources.Die3;
            }
            if (dice == 4)
            {
                diePictureBox.Image = global::Backgammon.Properties.Resources.Die4;
            }
            if (dice == 5)
            {
                diePictureBox.Image = global::Backgammon.Properties.Resources.Die5;
            }
            if (dice == 6)
            {
                diePictureBox.Image = global::Backgammon.Properties.Resources.Die6;
            }


        }


        //Crtanje
        //cratenj goren del
        private void pbPlacement0_Paint(object sender, PaintEventArgs e)
        {
            if (board.placements[0].numberOfCheckers == 0)
            {
                labelNumberOfCheckers0.Visible = false;
            }
            else
            {
                Graphics g = e.Graphics;
                Checkers checkers = new Checkers(board.placements[0].colorOfCheckers);
                //Checkers checkers = new Checkers(Color.Red);
                Point location = new Point();
                location.X = 0;
                location.Y = 0;

                for (int i = 0; i < board.placements[0].numberOfCheckers; i++)
                {
                    if (i < 5)
                    {
                        checkers.draw(g, location);

                        location.Y += 51;
                    }

                }
                labelNumberOfCheckers0.Text = String.Format("{0}", board.placements[0].numberOfCheckers);
                labelNumberOfCheckers0.Visible = true;
            }



        }

        private void pbPlacement1_Paint(object sender, PaintEventArgs e)
        {
            if (board.placements[1].numberOfCheckers == 0)
            {
                labelNumberOfCheckers1.Visible = false;
            }
            else
            {
                Graphics g = e.Graphics;
                Checkers checkers = new Checkers(board.placements[1].colorOfCheckers);
                //Checkers checkers = new Checkers(Color.Red);
                Point location = new Point();
                location.X = 0;
                location.Y = 0;


                for (int i = 0; i < board.placements[1].numberOfCheckers; i++)
                {
                    if (i < 5)
                    {
                        checkers.draw(g, location);

                        location.Y += 51;
                    }

                }
                labelNumberOfCheckers1.Text = String.Format("{0}", board.placements[1].numberOfCheckers);
                labelNumberOfCheckers1.Visible = true;
            }


        }
        private void pbPlacement2_Paint(object sender, PaintEventArgs e)
        {
            if (board.placements[2].numberOfCheckers == 0)
            {
                labelNumberOfCheckers2.Visible = false;
            }
            else
            {
                Graphics g = e.Graphics;
                Checkers checkers = new Checkers(board.placements[2].colorOfCheckers);
                //Checkers checkers = new Checkers(Color.Red);
                Point location = new Point();
                location.X = 0;
                location.Y = 0;

                for (int i = 0; i < board.placements[2].numberOfCheckers; i++)
                {
                    if (i < 5)
                    {
                        checkers.draw(g, location);

                        location.Y += 51;
                    }

                }
                labelNumberOfCheckers2.Text = String.Format("{0}", board.placements[2].numberOfCheckers);
                labelNumberOfCheckers2.Visible = true;

            }
        }

        private void pbPlacement3_Paint(object sender, PaintEventArgs e)
        {
            if (board.placements[3].numberOfCheckers == 0)
            {
                labelNumberOfCheckers3.Visible = false;
            }
            else
            {
                Graphics g = e.Graphics;
                Checkers checkers = new Checkers(board.placements[3].colorOfCheckers);
                //Checkers checkers = new Checkers(Color.Red);
                Point location = new Point();
                location.X = 0;
                location.Y = 0;

                for (int i = 0; i < board.placements[3].numberOfCheckers; i++)
                {
                    if (i < 5)
                    {
                        checkers.draw(g, location);

                        location.Y += 51;
                    }

                }
                labelNumberOfCheckers3.Text = String.Format("{0}", board.placements[3].numberOfCheckers);
                labelNumberOfCheckers3.Visible = true;

            }
        }

        private void pbPlacement4_Paint(object sender, PaintEventArgs e)
        {
            if (board.placements[4].numberOfCheckers == 0)
            {
                labelNumberOfCheckers4.Visible = false;
            }
            else
            {
                Graphics g = e.Graphics;
                Checkers checkers = new Checkers(board.placements[4].colorOfCheckers);
                //Checkers checkers = new Checkers(Color.Red);
                Point location = new Point();
                location.X = 0;
                location.Y = 0;

                for (int i = 0; i < board.placements[4].numberOfCheckers; i++)
                {
                    if (i < 5)
                    {
                        checkers.draw(g, location);

                        location.Y += 51;
                    }

                }
                labelNumberOfCheckers4.Text = String.Format("{0}", board.placements[4].numberOfCheckers);
                labelNumberOfCheckers4.Visible = true;

            }
        }

        private void pbPlacement5_Paint(object sender, PaintEventArgs e)
        {
            if (board.placements[5].numberOfCheckers == 0)
            {
                labelNumberOfCheckers5.Visible = false;
            }
            else
            {
                Graphics g = e.Graphics;
                Checkers checkers = new Checkers(board.placements[5].colorOfCheckers);
                //Checkers checkers = new Checkers(Color.Red);
                Point location = new Point();
                location.X = 0;
                location.Y = 0;

                for (int i = 0; i < board.placements[5].numberOfCheckers; i++)
                {
                    if (i < 5)
                    {
                        checkers.draw(g, location);

                        location.Y += 51;
                    }

                }
                labelNumberOfCheckers5.Text = String.Format("{0}", board.placements[5].numberOfCheckers);
                labelNumberOfCheckers5.Visible = true;

            }
        }

        private void pbPlacement6_Paint(object sender, PaintEventArgs e)
        {
            if (board.placements[6].numberOfCheckers == 0)
            {
                labelNumberOfCheckers6.Visible = false;
            }
            else
            {
                Graphics g = e.Graphics;
                Checkers checkers = new Checkers(board.placements[6].colorOfCheckers);
                //Checkers checkers = new Checkers(Color.Red);
                Point location = new Point();
                location.X = 0;
                location.Y = 0;

                for (int i = 0; i < board.placements[6].numberOfCheckers; i++)
                {
                    if (i < 5)
                    {
                        checkers.draw(g, location);

                        location.Y += 51;
                    }

                }
                labelNumberOfCheckers6.Text = String.Format("{0}", board.placements[6].numberOfCheckers);
                labelNumberOfCheckers6.Visible = true;

            }
        }

        private void pbPlacement7_Paint(object sender, PaintEventArgs e)
        {
            if (board.placements[7].numberOfCheckers == 0)
            {
                labelNumberOfCheckers7.Visible = false;
            }
            else
            {
                Graphics g = e.Graphics;
                Checkers checkers = new Checkers(board.placements[7].colorOfCheckers);
                //Checkers checkers = new Checkers(Color.Red);
                Point location = new Point();
                location.X = 0;
                location.Y = 0;

                for (int i = 0; i < board.placements[7].numberOfCheckers; i++)
                {
                    if (i < 5)
                    {
                        checkers.draw(g, location);

                        location.Y += 51;
                    }

                }
                labelNumberOfCheckers7.Text = String.Format("{0}", board.placements[7].numberOfCheckers);
                labelNumberOfCheckers7.Visible = true;

            }
        }

        private void pbPlacement8_Paint(object sender, PaintEventArgs e)
        {
            if (board.placements[8].numberOfCheckers == 0)
            {
                labelNumberOfCheckers8.Visible = false;
            }
            else
            {
                Graphics g = e.Graphics;
                Checkers checkers = new Checkers(board.placements[8].colorOfCheckers);
                //Checkers checkers = new Checkers(Color.Red);
                Point location = new Point();
                location.X = 0;
                location.Y = 0;

                for (int i = 0; i < board.placements[8].numberOfCheckers; i++)
                {
                    if (i < 5)
                    {
                        checkers.draw(g, location);

                        location.Y += 51;
                    }

                }
                labelNumberOfCheckers8.Text = String.Format("{0}", board.placements[8].numberOfCheckers);
                labelNumberOfCheckers8.Visible = true;

            }
        }

        private void pbPlacement9_Paint(object sender, PaintEventArgs e)
        {
            if (board.placements[9].numberOfCheckers == 0)
            {
                labelNumberOfCheckers9.Visible = false;
            }
            else
            {
                Graphics g = e.Graphics;
                Checkers checkers = new Checkers(board.placements[9].colorOfCheckers);
                //Checkers checkers = new Checkers(Color.Red);
                Point location = new Point();
                location.X = 0;
                location.Y = 0;

                for (int i = 0; i < board.placements[9].numberOfCheckers; i++)
                {
                    if (i < 5)
                    {
                        checkers.draw(g, location);

                        location.Y += 51;
                    }

                }
                labelNumberOfCheckers9.Text = String.Format("{0}", board.placements[9].numberOfCheckers);
                labelNumberOfCheckers9.Visible = true;

            }
        }

        private void pbPlacement10_Paint(object sender, PaintEventArgs e)
        {
            if (board.placements[10].numberOfCheckers == 0)
            {
                labelNumberOfCheckers10.Visible = false;
            }
            else
            {
                Graphics g = e.Graphics;
                Checkers checkers = new Checkers(board.placements[10].colorOfCheckers);
                //Checkers checkers = new Checkers(Color.Red);
                Point location = new Point();
                location.X = 0;
                location.Y = 0;

                for (int i = 0; i < board.placements[10].numberOfCheckers; i++)
                {
                    if (i < 5)
                    {
                        checkers.draw(g, location);

                        location.Y += 51;
                    }

                }
                labelNumberOfCheckers10.Text = String.Format("{0}", board.placements[10].numberOfCheckers);
                labelNumberOfCheckers10.Visible = true;

            }
        }

        private void pbPlacement11_Paint(object sender, PaintEventArgs e)
        {
            if (board.placements[11].numberOfCheckers == 0)
            {
                labelNumberOfCheckers11.Visible = false;
            }
            else
            {
                Graphics g = e.Graphics;
                Checkers checkers = new Checkers(board.placements[11].colorOfCheckers);
                //Checkers checkers = new Checkers(Color.Red);
                Point location = new Point();
                location.X = 0;
                location.Y = 0;

                for (int i = 0; i < board.placements[11].numberOfCheckers; i++)
                {
                    if (i < 5)
                    {
                        checkers.draw(g, location);

                        location.Y += 51;
                    }

                }
                labelNumberOfCheckers11.Text = String.Format("{0}", board.placements[11].numberOfCheckers);
                labelNumberOfCheckers11.Visible = true;

            }
        }

        //crtanje dolen del
        private void pbPlacement12_Paint(object sender, PaintEventArgs e)
        {
            if (board.placements[12].numberOfCheckers == 0)
            {
                labelNumberOfCheckers12.Visible = false;
            }
            else
            {
                Graphics g = e.Graphics;
                Checkers checkers = new Checkers(board.placements[12].colorOfCheckers);
                //Checkers checkers = new Checkers(Color.Red);
                Point location = new Point();
                location.X = 0;
                location.Y = 255;

                for (int i = 0; i < board.placements[12].numberOfCheckers; i++)
                {
                    if (i < 5)
                    {
                        checkers.draw(g, location);

                        location.Y -= 51;
                    }

                }
                labelNumberOfCheckers12.Text = String.Format("{0}", board.placements[12].numberOfCheckers);
                labelNumberOfCheckers12.Visible = true;
            }
        }

        private void pbPlacement13_Paint(object sender, PaintEventArgs e)
        {
            if (board.placements[13].numberOfCheckers == 0)
            {
                labelNumberOfCheckers13.Visible = false;
            }
            else
            {
                Graphics g = e.Graphics;
                Checkers checkers = new Checkers(board.placements[13].colorOfCheckers);
                //Checkers checkers = new Checkers(Color.Red);
                Point location = new Point();
                location.X = 0;
                location.Y = 255;

                for (int i = 0; i < board.placements[13].numberOfCheckers; i++)
                {
                    if (i < 5)
                    {
                        checkers.draw(g, location);

                        location.Y -= 51;
                    }

                }
                labelNumberOfCheckers13.Text = String.Format("{0}", board.placements[13].numberOfCheckers);
                labelNumberOfCheckers13.Visible = true;
            }

        }

        private void pbPlacement14_Paint(object sender, PaintEventArgs e)
        {
            if (board.placements[14].numberOfCheckers == 0)
            {
                labelNumberOfCheckers14.Visible = false;
            }
            else
            {
                Graphics g = e.Graphics;
                Checkers checkers = new Checkers(board.placements[14].colorOfCheckers);
                //Checkers checkers = new Checkers(Color.Red);
                Point location = new Point();
                location.X = 0;
                location.Y = 255;

                for (int i = 0; i < board.placements[14].numberOfCheckers; i++)
                {
                    if (i < 5)
                    {
                        checkers.draw(g, location);

                        location.Y -= 51;
                    }

                }
                labelNumberOfCheckers14.Text = String.Format("{0}", board.placements[14].numberOfCheckers);
                labelNumberOfCheckers14.Visible = true;
            }
        }

        private void pbPlacement15_Paint(object sender, PaintEventArgs e)
        {
            if (board.placements[15].numberOfCheckers == 0)
            {
                labelNumberOfCheckers15.Visible = false;
            }
            else
            {
                Graphics g = e.Graphics;
                Checkers checkers = new Checkers(board.placements[15].colorOfCheckers);
                //Checkers checkers = new Checkers(Color.Red);
                Point location = new Point();
                location.X = 0;
                location.Y = 255;

                for (int i = 0; i < board.placements[15].numberOfCheckers; i++)
                {
                    if (i < 5)
                    {
                        checkers.draw(g, location);

                        location.Y -= 51;
                    }

                }
                labelNumberOfCheckers15.Text = String.Format("{0}", board.placements[15].numberOfCheckers);
                labelNumberOfCheckers15.Visible = true;
            }
        }

        private void pbPlacement16_Paint(object sender, PaintEventArgs e)
        {
            if (board.placements[16].numberOfCheckers == 0)
            {
                labelNumberOfCheckers16.Visible = false;
            }
            else
            {
                Graphics g = e.Graphics;
                Checkers checkers = new Checkers(board.placements[16].colorOfCheckers);
                //Checkers checkers = new Checkers(Color.Red);
                Point location = new Point();
                location.X = 0;
                location.Y = 255;

                for (int i = 0; i < board.placements[16].numberOfCheckers; i++)
                {
                    if (i < 5)
                    {
                        checkers.draw(g, location);

                        location.Y -= 51;
                    }

                }
                labelNumberOfCheckers16.Text = String.Format("{0}", board.placements[16].numberOfCheckers);
                labelNumberOfCheckers16.Visible = true;
            }
        }

        private void pbPlacement17_Paint(object sender, PaintEventArgs e)
        {
            if (board.placements[17].numberOfCheckers == 0)
            {
                labelNumberOfCheckers17.Visible = false;
            }
            else
            {
                Graphics g = e.Graphics;
                Checkers checkers = new Checkers(board.placements[17].colorOfCheckers);
                //Checkers checkers = new Checkers(Color.Red);
                Point location = new Point();
                location.X = 0;
                location.Y = 255;

                for (int i = 0; i < board.placements[17].numberOfCheckers; i++)
                {
                    if (i < 5)
                    {
                        checkers.draw(g, location);

                        location.Y -= 51;
                    }

                }
                labelNumberOfCheckers17.Text = String.Format("{0}", board.placements[17].numberOfCheckers);
                labelNumberOfCheckers17.Visible = true;
            }
        }

        private void pbPlacement18_Paint(object sender, PaintEventArgs e)
        {
            if (board.placements[18].numberOfCheckers == 0)
            {
                labelNumberOfCheckers18.Visible = false;
            }
            else
            {
                Graphics g = e.Graphics;
                Checkers checkers = new Checkers(board.placements[18].colorOfCheckers);
                //Checkers checkers = new Checkers(Color.Red);
                Point location = new Point();
                location.X = 0;
                location.Y = 255;

                for (int i = 0; i < board.placements[18].numberOfCheckers; i++)
                {
                    if (i < 5)
                    {
                        checkers.draw(g, location);

                        location.Y -= 51;
                    }

                }
                labelNumberOfCheckers18.Text = String.Format("{0}", board.placements[18].numberOfCheckers);
                labelNumberOfCheckers18.Visible = true;
            }
        }

        private void pbPlacement19_Paint(object sender, PaintEventArgs e)
        {
            if (board.placements[19].numberOfCheckers == 0)
            {
                labelNumberOfCheckers19.Visible = false;
            }
            else
            {
                Graphics g = e.Graphics;
                Checkers checkers = new Checkers(board.placements[19].colorOfCheckers);
                //Checkers checkers = new Checkers(Color.Red);
                Point location = new Point();
                location.X = 0;
                location.Y = 255;

                for (int i = 0; i < board.placements[19].numberOfCheckers; i++)
                {
                    if (i < 5)
                    {
                        checkers.draw(g, location);

                        location.Y -= 51;
                    }

                }
                labelNumberOfCheckers19.Text = String.Format("{0}", board.placements[19].numberOfCheckers);
                labelNumberOfCheckers19.Visible = true;
            }
        }

        private void pbPlacement20_Paint(object sender, PaintEventArgs e)
        {
            if (board.placements[20].numberOfCheckers == 0)
            {
                labelNumberOfCheckers20.Visible = false;
            }
            else
            {
                Graphics g = e.Graphics;
                Checkers checkers = new Checkers(board.placements[20].colorOfCheckers);
                //Checkers checkers = new Checkers(Color.Red);
                Point location = new Point();
                location.X = 0;
                location.Y = 255;

                for (int i = 0; i < board.placements[20].numberOfCheckers; i++)
                {
                    if (i < 5)
                    {
                        checkers.draw(g, location);

                        location.Y -= 51;
                    }

                }
                labelNumberOfCheckers20.Text = String.Format("{0}", board.placements[20].numberOfCheckers);
                labelNumberOfCheckers20.Visible = true;
            }
        }

        private void pbPlacement21_Paint(object sender, PaintEventArgs e)
        {
            if (board.placements[21].numberOfCheckers == 0)
            {
                labelNumberOfCheckers21.Visible = false;
            }
            else
            {
                Graphics g = e.Graphics;
                Checkers checkers = new Checkers(board.placements[21].colorOfCheckers);
                //Checkers checkers = new Checkers(Color.Red);
                Point location = new Point();
                location.X = 0;
                location.Y = 255;

                for (int i = 0; i < board.placements[21].numberOfCheckers; i++)
                {
                    if (i < 5)
                    {
                        checkers.draw(g, location);

                        location.Y -= 51;
                    }

                }
                labelNumberOfCheckers21.Text = String.Format("{0}", board.placements[21].numberOfCheckers);
                labelNumberOfCheckers21.Visible = true;
            }
        }
        private void pbPlacement22_Paint(object sender, PaintEventArgs e)
        {
            if (board.placements[22].numberOfCheckers == 0)
            {
                labelNumberOfCheckers22.Visible = false;
            }
            else
            {
                Graphics g = e.Graphics;
                Checkers checkers = new Checkers(board.placements[22].colorOfCheckers);
                //Checkers checkers = new Checkers(Color.Red);
                Point location = new Point();
                location.X = 0;
                location.Y = 255;

                for (int i = 0; i < board.placements[22].numberOfCheckers; i++)
                {
                    if (i < 5)
                    {
                        checkers.draw(g, location);

                        location.Y -= 51;
                    }

                }
                labelNumberOfCheckers22.Text = String.Format("{0}", board.placements[22].numberOfCheckers);
                labelNumberOfCheckers22.Visible = true;
            }
        }


        private void pbPlacement23_Paint(object sender, PaintEventArgs e)
        {
            if (board.placements[23].numberOfCheckers == 0)
            {
                labelNumberOfCheckers23.Visible = false;
            }
            else
            {
                Graphics g = e.Graphics;
                Checkers checkers = new Checkers(board.placements[23].colorOfCheckers);
                //Checkers checkers = new Checkers(Color.Red);
                Point location = new Point();
                location.X = 0;
                location.Y = 255;

                for (int i = 0; i < board.placements[23].numberOfCheckers; i++)
                {
                    if (i < 5)
                    {
                        checkers.draw(g, location);

                        location.Y -= 51;
                    }

                }
                labelNumberOfCheckers23.Text = String.Format("{0}", board.placements[23].numberOfCheckers);
                labelNumberOfCheckers23.Visible = true;
            }
        }

        private void pictureBoxRemovedPlayerTwo_Paint(object sender, PaintEventArgs e)
        {
            pictureRemoveCheckersPlacements[1].Visible = true;
            Graphics g = e.Graphics;
            Checkers checkers = new Checkers(gameController.playerTwo.color);
            //Checkers checkers = new Checkers(Color.Red);
            Point location = new Point();
            location.X = 0;
            location.Y = 0;

            for (int i = 0; i < gameController.playerTwo.checkersAtBar; i++)
            {
                if (i < 5)
                {
                    checkers.draw(g, location);

                    location.Y += 51;
                }

            }
            //mozhe da napisham nekade kolku tochno ima butnati

        }

        private void pictureBoxRemovedPlayerOne_Paint(object sender, PaintEventArgs e)
        {

            pictureRemoveCheckersPlacements[0].Visible = true;
            Graphics g = e.Graphics;
            Checkers checkers = new Checkers(gameController.playerOne.color);
            //Checkers checkers = new Checkers(Color.Red);
            Point location = new Point();
            location.X = 0;
            location.Y = 255;

            for (int i = 0; i < gameController.playerOne.checkersAtBar; i++)
            {
                if (i < 5)
                {
                    checkers.draw(g, location);

                    location.Y -= 51;
                }

            }
            //mozhe da napisham nekade kolku tochno ima butnati

        }

        private void button2_Click(object sender, EventArgs e)
        {
            StartRollMenuForm startRollMenuForm = new StartRollMenuForm();
            startRollMenuForm.Show();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (gameController.playerOne.isMyTurn)
            {
                MessageBox.Show(string.Format("Player {0} won!", gameController.playerTwo.color.ToString()));
            }
            else
            {
                MessageBox.Show(string.Format("Player {0} won!", gameController.playerOne.color.ToString()));
            }
            StartRollMenuForm startRollMenuForm = new StartRollMenuForm();
            startRollMenuForm.Show();
            this.Close();
        }





        //Crtanje zavrshuva

    }
}
