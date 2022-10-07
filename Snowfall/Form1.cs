using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Snowfall
{
    public partial class Form1 : Form
    {
        private readonly IList<Snowflake> snowflakes;
        private readonly Timer timer;
        private Graphics buffer;
        Bitmap map, snow, background, mainBitmap;
        int speed = 25;
        int n = 0;

        public Form1()
        {
            InitializeComponent();
            snowflakes = new List<Snowflake>();
            background = new Bitmap(Properties.Resources.Фон);
            map = new Bitmap(background,
               Screen.PrimaryScreen.WorkingArea.Width,
               Screen.PrimaryScreen.WorkingArea.Height);
            snow = new Bitmap(Properties.Resources.снежинка);
            mainBitmap = new Bitmap(Screen.PrimaryScreen.WorkingArea.Width,
                Screen.PrimaryScreen.WorkingArea.Height);
            buffer = Graphics.FromImage(mainBitmap);
            AddSnowflakes();
            timer = new Timer();
            timer.Interval = 250;
            timer.Tick += Timer_Tick;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            timer.Stop();
            foreach (var snowflake in snowflakes)
            {

                snowflake.Y += snowflake.Size;
                if (snowflake.Y > Screen.PrimaryScreen.WorkingArea.Height)
                {
                    snowflake.Y = -snowflake.Size;
                }
                if (snowflake.Y > 0)
                {
                    Draw();
                }
            }
            timer.Start();
        }

        private void AddSnowflakes()
        {
            var rnd = new Random();
            for (int i = 0; i < 50; i++)
            {
                snowflakes.Add(new Snowflake
                {
                    X = rnd.Next(Screen.PrimaryScreen.WorkingArea.Width),
                    Y = -rnd.Next(Screen.PrimaryScreen.WorkingArea.Height),
                    Size = rnd.Next(5, 15)
                });
            }
        }
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Draw();
        }

        private void Draw()
        {
            buffer.DrawImage(map, new Rectangle(0, 0,
                Screen.PrimaryScreen.WorkingArea.Width,
                Screen.PrimaryScreen.WorkingArea.Height));
            foreach (var snowflake in snowflakes)
            {
                if (snowflake.Y > 0)
                {
                        buffer.DrawImage(snow, new Rectangle(
                        snowflake.X,
                        snowflake.Y,
                        snowflake.Size,
                        snowflake.Size));
                }
            }


            var g = this.CreateGraphics();
            g.DrawImage(mainBitmap, 0, 0);

        }

        private void Form1_Click(object sender, EventArgs e)
        {
            if (n == 0)
            {
                timer.Start();
                n++;
                Console.WriteLine("Сейчас Start");
            }
            else if (n == 1)
            {
                speed = 25;
                n++;
                Console.WriteLine("Сейчас Start c доп скоростью");
            }
            else if (n == 2)
            {
                timer.Stop();
                speed = 0;
                n = 0;
                Console.WriteLine("Сейчас Stop");
            }
        }

    }
}
