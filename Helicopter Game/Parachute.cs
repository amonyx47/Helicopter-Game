using Helicopter_Game.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helicopter_Game
{
    class Parachute : ImageBase
    {
        public Parachute() : base(Resources.parachute)
        {
            boundingBox.X = left;
            boundingBox.Y = top + 25;
            boundingBox.Width = 25;
            boundingBox.Height = 25;
        }

        private Rectangle boundingBox = new Rectangle();
        internal bool visible = true;
        internal bool dead = false;
        internal bool saved = false;

        public void Update(int x, int y)
        {
            left = x;
            top = y;
            boundingBox.X = left;
            boundingBox.Y = top + 25;
        }

        public bool Hit(int x, int y)
        {
            Rectangle rect = new Rectangle(x, y, 1, 1);
            if (boundingBox.Contains(rect))
            {
                return true;
            }

            return false;
        }

        public void DrawRectangleBox(Graphics g) {
            g.DrawRectangle(new Pen(Color.Red), boundingBox);
        }

        internal bool isVisible()
        {
            return visible;
        }

        internal bool isDead()
        {
            return dead;
        }
    }
}
