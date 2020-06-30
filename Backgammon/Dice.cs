using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backgammon
{
    public class Dice
    {
        public int diceOne { set; get; }
        public int diceTwo { set; get; }
        
        
        Random roll = new Random();
        public Dice()
        {          
           
        }
        public void rollDices()
        {
            diceOne = roll.Next(1, 7);
            diceTwo = roll.Next(1, 7);
           
           
        }
        public Boolean rolledDouble()
        {
            if(diceOne == diceTwo)
            {
                return true;
            }
            return false;
        }
        public void diceOnePlayed()
        {
            diceOne = 0;
        }
        public void diceTwoPlayed()
        {
            diceTwo = 0;
        }

    }
}
