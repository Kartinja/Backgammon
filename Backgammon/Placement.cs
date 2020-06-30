using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backgammon
{
    public class Placement    
    {      
        public int ID { get; set; }
        public int numberOfCheckers { get; set; }
        
        public Color colorOfCheckers { get; set; }


        public Placement(int numberOfCheckers, Color colorOfCheckers,int ID)
        {
            this.numberOfCheckers = numberOfCheckers;
            this.colorOfCheckers = colorOfCheckers;
            this.ID = ID;
            
        }
        public Placement(int ID)
        {
            this.ID = ID;
            this.numberOfCheckers = 0;
            this.colorOfCheckers = Color.Empty;
        }

    }
}
