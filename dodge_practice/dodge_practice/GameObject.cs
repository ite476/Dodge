using dodge_practice.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace dodge_practice
{
    
    public class GameObject : PictureBox
    {
        public int speedX = 0;
        public int speedY = 0;      
        public GameObject(int X, int Y)
        {
            this.Location = new Point(X, Y);
            this.X = X; this.Y = Y;
        }
        //      On Construct        //
        public double X { get; set; }
        public double Y { get; set; }
        public double speedX_pr = 0;
        public double speedY_pr = 0;
        public void Fly(double dX, double dY)
        {
            X += dX; Y += dY;
            this.Location = new Point((int)X, (int)Y);
        }

        public bool isHitBy(GameObject another)
        {
            if (
            ((this.Location.X + this.Width > another.Location.X) && (this.Location.X < another.Location.X + another.Width))
            && ((this.Location.Y + this.Height > another.Location.Y) && (this.Location.Y < another.Location.Y + another.Height))
            )
                return true;
            else return false;
        }

        public bool isOutOf(Panel stage)
        {
            int buffer = 3;
            if (
                this.Location.X > stage.Size.Width + (buffer * this.Size.Width) 
                || this.Location.X < -(buffer * this.Size.Width)
                || this.Location.Y > stage.Size.Height + (buffer * this.Size.Height) 
                || this.Location.Y < -(buffer * this.Size.Height)
                ) return true;
            else return false;

        }
    }
    public class Player : GameObject
    {
        public int speedDefault = 2;
        public double speedPrecise = 1.5;
        public bool isAlive = true;

        public Player(int X, int Y) : base(X, Y)
        {
            ReadyPlayerOne();
        }

        public Player(int X, int Y, double? speed)  : base(X, Y)
        {
            ReadyPlayerOne();
            if (speed != null) { this.speedPrecise = (double)speed; }
        }
        private void ReadyPlayerOne()
        {
            this.Size = new Size(10, 10);
            this.Image = Resources.player;
        }
        
                
    }
    public class Bullet : GameObject
    {
        public static Size defaultSize = new Size(4, 4);
        public static Image defaultImage = Resources.bullet;

        public static int countDodge = 0;
        public static double MaxSpeed = 3;

        
        public Bullet(int X, int Y, double speedX, double speedY) : base(X, Y)
        {
            this.Size = Bullet.defaultSize;
            this.Image = Bullet.defaultImage;
            this.speedX_pr = speedX;
            this.speedY_pr = speedY;
        }

        public static Bullet Spawn(Panel stage, GameObject target)
        {
            return Spawn(stage, target, false);
        }
        public static Bullet Spawn(Panel stage, GameObject target, bool isFirstTime)
        {
            Random random = new Random(DateTime.Now.Millisecond);
            int deciderDirection = random.Next(0, 4);
            double spawnLocationRatio = random.NextDouble();
            
            double minSpeedRatio = 0.5;
            double totalspeed = (minSpeedRatio + (1 - minSpeedRatio) * random.NextDouble()) * Bullet.MaxSpeed;
            double speedX_pr = random.NextDouble() * totalspeed;
            double speedY_pr = Math.Sqrt(Math.Pow(totalspeed, 2) - Math.Pow(speedX_pr, 2));

            int spawnX = 0, spawnY = 0;

            if (deciderDirection < 2)
            {
                spawnX = 0; 
            }
            if (deciderDirection % 2 == 0)
            {
                spawnY = 0;
            }

            
            switch (deciderDirection)
            {
                case 0: // top
                    spawnX = isFirstTime ?
                        (int)((0.1 + 0.8 * spawnLocationRatio) * stage.Width) / 100 :
                        (int)(spawnLocationRatio * stage.Width) / 100;
                    spawnY = isFirstTime? 
                        (stage.Size.Height / 10) :
                        0;
                    break;
                case 1: // left
                    spawnX = isFirstTime ?
                        (stage.Size.Width / 10) :
                        0;
                    spawnY = isFirstTime ?
                        (int)((0.1 + 0.8 * spawnLocationRatio) * stage.Height) / 100 :
                        (int)(spawnLocationRatio * stage.Height) / 100;
                    break;
                case 2: // bottom
                    spawnX = isFirstTime ?
                        (int)((0.1 + 0.8 * spawnLocationRatio) * stage.Width) / 100 :
                        (int)(spawnLocationRatio * stage.Width) / 100;
                    spawnY = isFirstTime ?
                        stage.Size.Height - (stage.Size.Height / 10) :
                        stage.Size.Height;
                    break;
                case 3: // right
                    spawnX = isFirstTime ?
                        stage.Size.Width - (stage.Size.Width / 10) :
                        stage.Size.Width;
                    spawnY = isFirstTime ?
                        (int)((0.1 + 0.8 * spawnLocationRatio) * stage.Height) / 100 :
                        (int)(spawnLocationRatio * stage.Height) / 100;
                    break;
                default: break;
            }

            if (target.Location.X < spawnX) { speedX_pr *= -1; }
            if (target.Location.Y < spawnY) { speedY_pr *= -1; }

            Bullet _bullet = new Bullet(spawnX, spawnY, speedX_pr, speedY_pr);
            stage.Controls.Add(_bullet);
            return _bullet;
        }

    }
    
        
}
