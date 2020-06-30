using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backgammon
{
    public class Checkers
    {
        public Color color { get; set; }
        public static int radius = 25;
        public Checkers(Color color)
        {
            this.color = color;

        }
        public Checkers()
        {

        }
        public void draw(Graphics g, Point location)
        {
            Brush brush = new SolidBrush(color);
            g.FillEllipse(brush, location.X, location.Y, 2 * radius, 2 * radius);
            Pen pen = new Pen(Color.Black,3f);
            g.DrawEllipse(pen, location.X, location.Y, 2 * radius, 2 * radius);
            brush.Dispose();
        }

    }
}
