using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Backgammon
{
    public enum Orientation
    {
        left,
        right
    }
    public class Player
    {
        public Color color { get; set; }

        public Boolean isMyTurn { get; set; }
        public int checkersAtBar { get; set; }
        



        public Orientation orientation { get; set; }



        public Player(Color color, Orientation orientation)
        {
            this.color = color;
            this.orientation = orientation;
            checkersAtBar = 0;

        }

        //vrakja ID na polinjata na koi shto bi mozhelo da se mrdne od mesto
        public IEnumerable<KeyValuePair<int, int>> getAvailableMoves(Dice dice, Board board)
        {
            if (checkersAtBar == 0)
            {
                List<KeyValuePair<int, int>> AvailableMoves = new List<KeyValuePair<int, int>>();
                for (int i = 0; i < board.placements.Count; i++)
                {

                    if (orientation == Orientation.left)// prv igrach
                    {
                        if (!dice.rolledDouble())// ako ima double nema potreba da se proverat dvete kocki
                        {
                            if (i - dice.diceOne >= 0 && dice.diceOne != 0) // da ne izle od mapa
                            {
                                if (isLegalInitialMove(board, i) && (isLegalFinalMove(board, i, i - dice.diceOne, dice.diceOne) || isLegalFinalMoveEat(board, i, i - dice.diceOne, dice.diceOne)))// prva kocka
                                {
                                    AvailableMoves.Add(new KeyValuePair<int, int>(i, i - dice.diceOne));

                                }
                            }
                        }

                        if (i - dice.diceTwo >= 0 && dice.diceTwo != 0)
                        {

                            if (isLegalInitialMove(board, i) && (isLegalFinalMove(board, i, i - dice.diceTwo, dice.diceTwo) || isLegalFinalMoveEat(board, i, i - dice.diceTwo, dice.diceTwo)))// vtora kocka
                            {
                                AvailableMoves.Add(new KeyValuePair<int, int>(i, i - dice.diceTwo));

                            }
                        }

                    }
                    if (orientation == Orientation.right)// vtor igrach
                    {
                        if (!dice.rolledDouble())// ako ima double nema potreba da se proverat dvete kocki
                        {
                            if (i + dice.diceOne <= 23 && dice.diceOne != 0)// da ne izleze od mapa
                            {
                                if (isLegalInitialMove(board, i) && (isLegalFinalMove(board, i, i + dice.diceOne, dice.diceOne) || isLegalFinalMoveEat(board, i, i + dice.diceOne, dice.diceOne))) // prva kocka
                                {
                                    AvailableMoves.Add(new KeyValuePair<int, int>(i, i + dice.diceOne));

                                }
                            }
                        }
                        if (i + dice.diceTwo <= 23 && dice.diceTwo != 0)
                        {
                            if (isLegalInitialMove(board, i) && (isLegalFinalMove(board, i, i + dice.diceTwo, dice.diceTwo) || isLegalFinalMoveEat(board, i, i + dice.diceTwo, dice.diceTwo)))// vtora kocka
                            {
                                AvailableMoves.Add(new KeyValuePair<int, int>(i, i + dice.diceTwo));

                            }
                        }
                    }

                }
                return AvailableMoves;
            }
            else
            {
                return getAvailableMovesFromBar(board, dice);
            }
        }

        private IEnumerable<KeyValuePair<int, int>> getAvailableMovesFromBar(Board board, Dice dice)
        {
            List<KeyValuePair<int, int>> AvailableMoves = new List<KeyValuePair<int, int>>();
            if (orientation == Orientation.left)
            {
                if (dice.diceOne != 0)
                {
                    if (isLegalFinalMove(board, 23, 23 - dice.diceOne + 1, dice.diceOne - 1) || isLegalFinalMoveEat(board, 23, 23 - dice.diceOne + 1, dice.diceOne - 1))
                    {
                        AvailableMoves.Add(new KeyValuePair<int, int>(0, dice.diceOne - 1));

                    }
                }


                if (dice.diceTwo != 0)
                {
                    if (isLegalFinalMove(board, 23, 23 - dice.diceTwo + 1, dice.diceTwo - 1) || isLegalFinalMoveEat(board, 23, 23 - dice.diceTwo + 1, dice.diceTwo - 1))
                    {
                        AvailableMoves.Add(new KeyValuePair<int, int>(23, dice.diceTwo - 1));

                    }
                }



            }
            else
            {
                if (dice.diceOne != 0)
                {
                    if (isLegalFinalMove(board, 0, 0 + dice.diceOne - 1, dice.diceOne - 1) || isLegalFinalMoveEat(board, 0, 0 + dice.diceOne - 1, dice.diceOne - 1))
                    {
                        AvailableMoves.Add(new KeyValuePair<int, int>(0, dice.diceOne - 1));

                    }
                }


                if (dice.diceTwo != 0)
                {
                    if (isLegalFinalMove(board, 0, 0 + dice.diceTwo - 1, dice.diceTwo - 1) || isLegalFinalMoveEat(board, 0, 0 + dice.diceTwo - 1, dice.diceTwo - 1))
                    {
                        AvailableMoves.Add(new KeyValuePair<int, int>(0, dice.diceTwo - 1));

                    }
                }

            }
            return AvailableMoves;
        }

        public bool isLegalInitialMove(Board board, int index)
        {
            if (board.placements[index].colorOfCheckers == color && checkersAtBar == 0)
            {
                return true;
            }
            return false;
        }

        public bool isLegalFinalMove(Board board, int fromIndex, int toIndex, int diceNumber)
        {
            if (orientation == Orientation.left)
            {

                if (fromIndex - toIndex == diceNumber && (board.placements[toIndex].colorOfCheckers == color || board.placements[toIndex].colorOfCheckers == Color.Empty))
                {
                    return true;
                }
            }
            else
            {
                if (toIndex - fromIndex == diceNumber && (board.placements[toIndex].colorOfCheckers == color || board.placements[toIndex].colorOfCheckers == Color.Empty))
                {
                    return true;
                }
            }

            return false;
        }
        public bool isLegalFinalMoveEat(Board board, int fromIndex, int toIndex, int diceNumber)
        {
            if (Math.Abs(fromIndex - toIndex) == diceNumber && board.placements[toIndex].colorOfCheckers != color && board.placements[toIndex].numberOfCheckers == 1)
            {
                return true;
            }
            return false;

        }
        public bool canRemoveCheckers(Board board)
        {
            // dali mozhe da pochnesh so vadenje
            int checkersNotAtHome = 0;
            if (orientation == Orientation.left)
            {
                for (int i = 6; i <= 23; i++)
                {
                    if (board.placements[i].colorOfCheckers == color)
                    {
                        checkersNotAtHome++;
                    }
                }
            }
            else
            {
                for (int i = 17; i >= 0; i--)
                {
                    if (board.placements[i].colorOfCheckers == color)
                    {
                        checkersNotAtHome++;
                    }
                }
            }
            if (checkersNotAtHome == 0 && checkersAtBar == 0)
            {
                return true;
            }
            return false;
        }
        public bool isLegalRemoveCheckerMove(Board board, int from, int diceNumber)
        {
            int highestPlaceChecker = 0;
            if (orientation == Orientation.left)
            {
                for (int i = 0; i < 6; i++)
                {
                    if (board.placements[i].colorOfCheckers == color)
                    {
                        highestPlaceChecker = i;
                    }
                }
                if (from - diceNumber == -1 || (highestPlaceChecker == from && from - diceNumber < -1))
                {
                    return true;
                }
            }
            else
            {
                for (int i = 23; i > 17; i--)
                {
                    if (board.placements[i].colorOfCheckers == color)
                    {
                        highestPlaceChecker = i;
                    }
                }
                if (from + diceNumber == 24 || (highestPlaceChecker == from && from + diceNumber > 24))
                {
                    return true;
                }
            }
            return false;
        }
        public IEnumerable<KeyValuePair<int, int>> getAvailableRemoveCheckerMoves(Board board, Dice dice)
        {
            List<KeyValuePair<int, int>> availabbleMoves = new List<KeyValuePair<int, int>>();
            //canremove
            if (canRemoveCheckers(board))
            {
                if (orientation == Orientation.left)
                {
                    for (int i = 0; i < 6; i++)
                    {
                        if (!dice.rolledDouble())
                        {
                            if (dice.diceOne != 0)
                            {
                                if (isLegalInitialMove(board, i) && isLegalRemoveCheckerMove(board, i, dice.diceOne))
                                {
                                    availabbleMoves.Add(new KeyValuePair<int, int>(i, dice.diceOne));
                                }
                            }
                        }
                        if (dice.diceTwo != 0)
                        {
                            if (isLegalInitialMove(board, i) && isLegalRemoveCheckerMove(board, i, dice.diceTwo))
                            {
                                availabbleMoves.Add(new KeyValuePair<int, int>(i, dice.diceTwo));
                            }
                        }
                    }
                }
                else
                {
                    for (int i = 18; i < 24; i++)
                    {
                        if (!dice.rolledDouble())
                        {
                            if (dice.diceOne != 0)
                            {
                                if (isLegalInitialMove(board, i) && isLegalRemoveCheckerMove(board, i, dice.diceOne))
                                {
                                    availabbleMoves.Add(new KeyValuePair<int, int>(i, dice.diceOne));
                                }
                            }
                        }
                        if (dice.diceTwo != 0)
                        {
                            if (isLegalInitialMove(board, i) && isLegalRemoveCheckerMove(board, i, dice.diceTwo))
                            {
                                availabbleMoves.Add(new KeyValuePair<int, int>(i, dice.diceTwo));
                            }
                        }
                    }
                }
            }
           

            return availabbleMoves;
        }
        public bool hasAvailableRemoveCheckers(Board board, Dice dice)
        {
            return getAvailableRemoveCheckerMoves(board, dice).ToList().Count > 0;
        }
        public void addCheckersToBar()
        {
            checkersAtBar++;
        }
        public void removeCheckersFromBar()
        {
            checkersAtBar--;
        }
        public bool hasAvailableMoves(Board board, Dice dice)
        {
            //return getAvailableMoves(dice, board).ToList().Count + getAvailableMovesFromBar(board, dice).ToList().Count  + getAvailableRemoveCheckerMoves(board,dice).ToList().Count > 0;
            return getAvailableMoves(dice, board).ToList().Count  + getAvailableRemoveCheckerMoves(board,dice).ToList().Count > 0;
        }
        public int GetCheckersAtHome(Board board)
        {
            int checkersAtHome = 0;
            if (orientation == Orientation.left)
            {
                for (int i = 0; i < 6; i++)
                {
                    if (board.placements[i].colorOfCheckers == color)
                    {
                        checkersAtHome++;
                    }
                }
            }
            else
            {
                for (int i = 18; i < 24; i++)
                {
                    if (board.placements[i].colorOfCheckers == color)
                    {
                        checkersAtHome++;
                    }
                }
            }
            return checkersAtHome;
        }
        

     
    }
}
