using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backgammon
{
    public class Board
    {
        public List<Placement> placements { get; set; }

        public Checkers checkersPlayerOne { get; set; }
        public Checkers checkersPlayerTwo { get; set; }


        public Board(Color playerOneColor, Color playerTwoColor)
        {
            placements = new List<Placement>(new Placement[24]);
            checkersPlayerOne = new Checkers(playerOneColor);
            checkersPlayerTwo = new Checkers(playerTwoColor);

            initializeBoard(playerOneColor, playerTwoColor);

        }
        // pochetok na tabla
        public void initializeBoard(Color playerOneColor, Color playerTwoColor)
        {


            for (int i = 0; i < 24; i++)
            {
                if (i == 0)
                {
                    placements[i] = new Placement(2, playerOneColor, i);

                }
                else if (i == 5)
                {
                    placements[i] = new Placement(5, playerTwoColor, i);
                }
                else if (i == 7)
                {
                    placements[i] = new Placement(3, playerTwoColor, i);
                }
                else if (i == 11)
                {
                    placements[i] = new Placement(5, playerOneColor, i);
                }
                else if (i == 12)
                {
                    placements[i] = new Placement(5, playerTwoColor, i);
                }
                else if (i == 16)
                {
                    placements[i] = new Placement(3, playerOneColor, i);
                }
                else if (i == 18)
                {
                    placements[i] = new Placement(5, playerOneColor, i);
                }
                else if (i == 23)
                {
                    placements[i] = new Placement(2, playerTwoColor, i);
                }
                else
                {
                    placements[i] = new Placement(i);
                }
            }
            /* for (int i = 0; i < 24; i++)
             {
                 if (i == 1)
                 {
                     placements[i] = new Placement(2, playerTwoColor, i);

                 }
                 else if (i == 2)
                 {
                     placements[i] = new Placement(5, playerTwoColor, i);
                 }
                 else if (i == 3)
                 {
                     placements[i] = new Placement(3, playerTwoColor, i);
                 }
                 else if (i == 4)
                 {
                     placements[i] = new Placement(5, playerTwoColor, i);
                 }
                 else if (i == 20)
                 {
                     placements[i] = new Placement(5, playerOneColor, i);
                 }
                 else if (i == 21)
                 {
                     placements[i] = new Placement(3, playerOneColor, i);
                 }
                 else if (i == 19)
                 {
                     placements[i] = new Placement(5, playerOneColor, i);
                 }
                 else if (i == 22)
                 {
                     placements[i] = new Placement(2, playerOneColor, i);
                 }
                 else
                 {
                     placements[i] = new Placement(i);
                 }
             }*/

        }

        //sekoe dvizhenje
        public void update()
        {
            for (int ID = 0; ID < 24; ID++)
            {
                if (placements[ID].numberOfCheckers > 0)
                {
                    int numberOfCheckers = placements[ID].numberOfCheckers;
                    Color colorOfCheckers = placements[ID].colorOfCheckers;
                    placements[ID] = new Placement(numberOfCheckers, colorOfCheckers, ID);
                }
                else
                {
                    placements[ID] = new Placement(ID);
                }
            }
        }
    }
}
