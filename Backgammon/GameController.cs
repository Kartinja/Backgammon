using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Backgammon
{
    public class GameController
    {


        public Player playerOne { get; set; }
        public Player playerTwo { get; set; }
        public Board board { get; set; }
        public int? playerInitialPlacementChoice { get; private set; }
        public Boolean haveClickedMove { get; set; }

        public int numberOfMovesLeft { get; set; }

        public Dice dice { get; set; }

        public Boolean rolledDices { get; set; }

        //Gi inicijalizira klasite

        public GameController(String player1Color, String player2Color, Board board)
        {
            playerOne = new Player(Color.FromName(player1Color), Orientation.right);
            playerTwo = new Player(Color.FromName(player2Color), Orientation.left);
            this.board = board;
            dice = new Dice();

        }
        public GameController(Board board)
        {
            this.board = board;
        }
        public bool playerHasAvailableMoves()
        {
            if (playerOne.isMyTurn)
            {
                return playerOne.hasAvailableMoves(board, dice);
            }
            else
            {
                return playerTwo.hasAvailableMoves(board, dice);
            }
        }
        public bool playerHasAvailableRemoveMoves()
        {
            if (playerOne.isMyTurn)
            {
                return playerOne.hasAvailableRemoveCheckers(board, dice);
            }
            else
            {
                return playerTwo.hasAvailableRemoveCheckers(board, dice);
            }
        }

        public bool isLegalFinalMoveEat(int toPlacement)
        {
            if (playerOne.isMyTurn)
            {
                if (dice.rolledDouble())
                {
                    return playerOne.isLegalFinalMoveEat(board, playerInitialPlacementChoice.Value, toPlacement, dice.diceOne);
                }
                else
                {

                    return playerOne.isLegalFinalMoveEat(board, playerInitialPlacementChoice.Value, toPlacement, dice.diceOne) ||
                           playerOne.isLegalFinalMoveEat(board, playerInitialPlacementChoice.Value, toPlacement, dice.diceTwo);
                }
            }
            else
            {
                if (dice.rolledDouble())
                {
                    return playerTwo.isLegalFinalMoveEat(board, playerInitialPlacementChoice.Value, toPlacement, dice.diceOne);
                }
                else
                {

                    return playerTwo.isLegalFinalMoveEat(board, playerInitialPlacementChoice.Value, toPlacement, dice.diceOne) ||
                           playerTwo.isLegalFinalMoveEat(board, playerInitialPlacementChoice.Value, toPlacement, dice.diceTwo);
                }
            }
        }
        public bool isLegalInitialMove(int fromPlacement)
        {
            if (playerOne.isMyTurn)
            {
                return playerOne.isLegalInitialMove(board, fromPlacement);
            }
            else // player2
            {
                return playerTwo.isLegalInitialMove(board, fromPlacement);
            }
        }
        public bool isLegalFinalMove(int toPlacement)
        {
            if (playerOne.isMyTurn)
            {
                if (dice.rolledDouble())
                {
                    return playerOne.isLegalFinalMove(board, playerInitialPlacementChoice.Value, toPlacement, dice.diceOne);
                }
                else
                {
                    return playerOne.isLegalFinalMove(board, playerInitialPlacementChoice.Value, toPlacement, dice.diceOne) ||
                           playerOne.isLegalFinalMove(board, playerInitialPlacementChoice.Value, toPlacement, dice.diceTwo);
                }
            }
            else //player2
            {
                if (dice.rolledDouble())
                {
                    return playerTwo.isLegalFinalMove(board, playerInitialPlacementChoice.Value, toPlacement, dice.diceOne);
                }
                else
                {
                    return playerTwo.isLegalFinalMove(board, playerInitialPlacementChoice.Value, toPlacement, dice.diceOne) ||
                           playerTwo.isLegalFinalMove(board, playerInitialPlacementChoice.Value, toPlacement, dice.diceTwo);
                }
            }

        }

        internal bool isLegalRemoveMove(int diceNumber)
        {
            if (playerOne.isMyTurn)
            {

                return playerOne.isLegalRemoveCheckerMove(board, playerInitialPlacementChoice.Value, diceNumber);

            }
            else //player2
            {
                return playerTwo.isLegalRemoveCheckerMove(board, playerInitialPlacementChoice.Value, diceNumber);
            }
        }
        public void setRemoveCheckerMove(int cube)
        {
            if (playerOne.isMyTurn)
            {
                resetAppropriateCube(24);
            }
            else
            {
                resetAppropriateCube(-1);
            }


            board.placements[playerInitialPlacementChoice.Value].numberOfCheckers--;

            // update triangle.
            if (board.placements[playerInitialPlacementChoice.Value].numberOfCheckers == 0)
            {
                board.placements[playerInitialPlacementChoice.Value].colorOfCheckers = Color.Empty;
            }

            playerInitialPlacementChoice = null;

            numberOfMovesLeft--;

        }
        public void setPlayerInitialMove(int? index)
        {
            playerInitialPlacementChoice = index;
        }
        public void setPlayerFinalMove(int index, bool eaten)
        {
            resetAppropriateCube(index);
            if (!eaten)
            {// ako e izeden nema potreba da go menuvash brojot na checkers na toa mesto
                board.placements[index].numberOfCheckers++;
            }
            if (playerInitialPlacementChoice >= 0 && playerInitialPlacementChoice <= 23)// normalno dvizhenje
            {
                board.placements[playerInitialPlacementChoice.Value].numberOfCheckers--;
                if (board.placements[playerInitialPlacementChoice.Value].numberOfCheckers == 0)
                {
                    board.placements[playerInitialPlacementChoice.Value].colorOfCheckers = Color.Empty;
                }
            }
            else// ako stavash izvaden chechker
            {
                if (playerOne.isMyTurn)
                {
                    playerOne.removeCheckersFromBar();
                }
                else
                {
                    playerTwo.removeCheckersFromBar();
                }
            }

            if (board.placements[index].numberOfCheckers == 1)
            {
                board.placements[index].colorOfCheckers = playerOne.isMyTurn ? playerOne.color : playerTwo.color;
            }

            playerInitialPlacementChoice = null;
            numberOfMovesLeft--;

        }


        public void setFinalMoveEat(int index)
        {
            if (playerOne.isMyTurn)
            {
                playerTwo.addCheckersToBar();
            }
            else
            {
                playerOne.addCheckersToBar();
            }
            setPlayerFinalMove(index, true);
        }
        public void swapTurns()
        {
            playerOne.isMyTurn = !playerOne.isMyTurn;
            playerTwo.isMyTurn = !playerTwo.isMyTurn;
            rolledDices = false;

        }
        public void rollDices()
        {
            dice.rollDices();
            //rolledDices = true;

        }
        public void UpdateMovesLeft()
        {
            if (!rolledDices)
            {
                if (dice.rolledDouble())
                {
                    numberOfMovesLeft = 4;
                }
                else
                {
                    numberOfMovesLeft = 2;
                }
                rolledDices = true;
            }

        }
        private void resetAppropriateCube(int toIndex)
        {
            if (!dice.rolledDouble())
            {
                if (playerOne.isMyTurn)
                {
                    if (toIndex == 24)
                    {
                        if (toIndex - playerInitialPlacementChoice <= dice.diceOne)
                        {
                            if (toIndex - playerInitialPlacementChoice > dice.diceTwo)
                            {
                                dice.diceOnePlayed();
                            }                               

                        }
                        else
                        {
                            dice.diceTwoPlayed();
                        }

                    }
                    else if (toIndex - playerInitialPlacementChoice == dice.diceOne)
                    {
                        dice.diceOnePlayed();
                    }
                    else
                    {
                        dice.diceTwoPlayed();
                    }
                }
                else
                {
                    if (toIndex == -1)
                    {
                        //playerInitialPlacementChoice == dice.diceOne -1
                        if (-1 >= playerInitialPlacementChoice - dice.diceOne)
                        {
                            if (playerInitialPlacementChoice - dice.diceTwo > -1)
                            {
                                dice.diceOnePlayed();
                            }
                                
                        }
                        else
                        {
                            dice.diceTwoPlayed();
                        }
                    }
                    else if (playerInitialPlacementChoice - toIndex == dice.diceOne)
                    {
                        dice.diceOnePlayed();
                    }
                    else
                    {
                        dice.diceTwoPlayed();
                    }
                }
            }
        }
    }
}
