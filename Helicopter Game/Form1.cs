using Helicopter_Game.Properties;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Helicopter_Game
{
    public partial class HeliGame : Form
    {

        int score;
        Font scoreFont = new Font("Arial", 12);
        SolidBrush scoreBrush = new SolidBrush(Color.White);
        Point scoreLocation = new Point(1, 1);

        Helicoper helicopter;
        PalmTree palmTree;
        Boat boat;
        Shark shark;

        Random rand = new Random();

        long ticks = 0;

        ArrayList parachuters = new ArrayList();
        private int deadParachuters;

        public HeliGame()
        {
            InitializeComponent();
            helicopter = new Helicoper(){ left = 200, top = 0};
            palmTree = new PalmTree() { left = 350, top = 25 };
            boat = new Boat() { left = 25, top = 200 };
            shark = new Shark() { left = 250, top = 300 };

            timerGameLoop.Start();
        }

        private void timerGameLoop_Tick(object sender, EventArgs e)
        {
            if(ticks % 30 == 0)
            {
                helicopter.Update(helicopter.left + rand.Next(-5, 5), helicopter.top);
            }

            if(ticks % 50 == 0)
            {
                Parachute parachuteTrooper = new Parachute() { left = helicopter.left + 100, top = helicopter.top + 75};
                parachuters.Add(parachuteTrooper);
            }

            foreach(Parachute parachuteTrooper in parachuters){

                if(shark.top > 390)
                {
                    shark.visible = false;
                }

                if(parachuteTrooper.top > 400)
                {
                    parachuteTrooper.visible = false;
                }

                if(parachuteTrooper.top > 240 && !parachuteTrooper.saved)
                {
                    shark.visible = true;
                    parachuteTrooper.dead = true;
                    shark.Update(parachuteTrooper.left - 90, parachuteTrooper.top + 25);
                }

                parachuteTrooper.Update(parachuteTrooper.left + rand.Next(-10, 10), parachuteTrooper.top + rand.Next(1, 5));
                if(boat.Hit(parachuteTrooper.left, parachuteTrooper.top))
                {
                    parachuteTrooper.visible = false;
                    parachuteTrooper.dead = false;
                    parachuteTrooper.saved = true;
                }
            }

            ticks++;
            this.Refresh();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            helicopter.DrawImage(g);
            palmTree.DrawImage(g);
            boat.DrawImage(g);

            if (shark.visible)
            {
                shark.DrawImage(g);
            }

            score = 0;
            deadParachuters = 0;
            foreach(Parachute parachuteTrooper in parachuters){
                if (parachuteTrooper.saved)
                {
                    score++;
                }
                else if (parachuteTrooper.isDead())
                {
                    deadParachuters++;
                }

                if(deadParachuters > 3)
                {
                    gameOver();
                    break;
                }

                if (parachuteTrooper.isVisible())
                {
                    parachuteTrooper.DrawImage(g);
                }

                if (parachuteTrooper.isDead() && !parachuteTrooper.saved)
                {
                    ImageBase blood = new ImageBase(Resources.blood) { left = parachuteTrooper.left + 5, top = parachuteTrooper.top + 25};
                    blood.DrawImage(g);
                    for(int i = 0; i < deadParachuters; i++)
                    {
                        ImageBase staticShark = new ImageBase(Resources.staticShark) { left = i * 55 + 420, top = 270 };
                        staticShark.DrawImage(g);
                    }
                }
            }

            g.DrawString("Score: " + score, scoreFont, scoreBrush, scoreLocation);

            base.OnPaint(e);
        }

        private void gameOver()
        {
            timerGameLoop.Stop();
            MessageBox.Show("You lost more than 3 of our fellow paratroopers. Shame on you.",
    "Game over");
            
        }

        private void HeliGame_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Right)
            {
                //Console.WriteLine("right key pressed");
                if (boat.left + 100 < 450)
                {
                    boat.Update(boat.left + 10, boat.top);
                }
            }

            if(e.KeyCode == Keys.Left)
            {
                //Console.WriteLine("left key pressed");
                if (boat.left > 0)
                {
                    boat.Update(boat.left - 10, boat.top);
                }
            }
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams handleParam = base.CreateParams;
                handleParam.ExStyle |= 0x02000000;   // WS_EX_COMPOSITED       
                return handleParam;
            }
        }

    }
}
