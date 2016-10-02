using Helicopter_Game.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helicopter_Game
{
    class Helicoper : ImageBase
    {
        public Helicoper() : base(Resources.helicopter)
        {
            boundingBox.X = left + 100;
            boundingBox.Y = top - 1;
            boundingBox.Width = 200;
            boundingBox.Height = 100;
        }

        private Rectangle boundingBox = new Rectangle();

        public void Update(int x, int y) {
            left = x;
            top = y;
            boundingBox.X = left + 100;
            boundingBox.Y = top - 1;
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
    }
}
