using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helicopter_Game
{
    class ImageBase : IDisposable
    {

        bool disposed = false;
        private int x;
        private int y;

        public int left { get { return x; } set { x = value; } }
        public int top { get { return y; } set { y = value; } }

        Bitmap bitmap;

        public ImageBase(Bitmap resource)
        {
            bitmap = resource;
        }

        public void DrawImage(Graphics g)
        {
            g.DrawImage(bitmap, x, y);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed) {
                return;
            }

            if (disposing)
            {
                bitmap.Dispose();
            }

            disposed = true;
        }
    }
}
