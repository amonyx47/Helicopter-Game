using Helicopter_Game.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helicopter_Game
{
    class Boat : ImageBase
    {

        public event EventHandler SomethingHappened;

        public Boat() : base(Resources.Boat)
        {
            boundingBox.X = left;
            boundingBox.Y = top + 30;
            boundingBox.Width = 100;
            boundingBox.Height = 30;
        }

        private Rectangle boundingBox = new Rectangle();

        public void Update(int x, int y)
        {
            left = x;
            top = y;
            boundingBox.X = left ;
            boundingBox.Y = top + 30;
        }

        public bool Hit(int x, int y)
        {
            Rectangle rect = new Rectangle(x, y, 50, 30);
            if (boundingBox.IntersectsWith(rect))
            {
                return true;
            }

            return false;
        }

        public void DrawRectangleBox(Graphics g)
        {
            g.DrawRectangle(new Pen(Color.Red), boundingBox);
        }
    }
}
