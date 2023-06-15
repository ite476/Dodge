using dodge_practice.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace dodge_practice
{
    // GameObject :: PictureBox that can possibly move during the session
    public class GameObject : PictureBox
    {
        public static Size defaultSize = new Size(1, 1);
        public static Image? defaultImage;

        public double X { get; set; }
        public double Y { get; set; }
        public double speedX = 0;
        public double speedY = 0;
        public GameObject(int X, int Y)
        {
            this.SetLocation(X, Y);
        }
        public GameObject(double X, double Y)
        {
            this.SetLocation(X, Y);
        }

        // Calculate realnumber cordinate and return into int cordinate //
        public void SetLocation(double X, double Y)
        {
            this.Location = new Point((int)X, (int)Y);
            this.X = X; this.Y = Y;
        }
        public void SetAsset()
        {
            this.Size = Bullet.defaultSize;
            this.Image = Bullet.defaultImage;
        }
        public void SetSpeed(double speedX, double speedY)
        {
            this.speedX = speedX;
            this.speedY = speedY;
        }
        public virtual void Fly()
        {
            X += this.speedX; Y += this.speedY;
            this.SetLocation(X, Y);
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
            int buffer = 1;
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
        public static new Size defaultSize = new Size(4, 4);
        public static new Image defaultImage = Resources.bullet;

        public static int countDodge = 0;
        public static double MaxSpeed = 3; 

        public Bullet(int X, int Y, double speedX, double speedY) : base(X, Y)
        {
            this.SetAsset();
            this.SetSpeed(speedX, speedY);
        }
        public Bullet(int X, int Y, double speedX, double speedY, bool isForHeir) : base(X, Y)
        {
            this.SetSpeed(speedX, speedY);
        }

        //      On Construct        //
        protected static void RandomSpeed(Random random, double minSpeedRatio, out double speedX, out double speedY)
        {
            double totalspeed = (minSpeedRatio + (1 - minSpeedRatio) * random.NextDouble()) * Bullet.MaxSpeed;
            speedX = random.NextDouble() * totalspeed;
            speedY = Math.Sqrt(Math.Pow(totalspeed, 2) - Math.Pow(speedX, 2));
        }
        protected static void RandomLocation(Random random, Panel stage, out int X, out int Y)
        {
            double WidthRatio = random.NextDouble();
            double HeightRatio = random.NextDouble();
            X = (int)(stage.Width * WidthRatio);
            Y = (int)(stage.Height * HeightRatio);
        }
        
        protected static void RandomizedSpawn(Panel stage, GameObject target, bool isFirstTime, out int _X, out int _Y, out double _speedX, out double _speedY)
        {
            Random random = new Random();
            int deciderDirection = random.Next(0, 4);
            double speedX, speedY;
            RandomSpeed(random, 0.5, out speedX, out speedY);

            int spawnX = 0, spawnY = 0;
            RandomLocation(random, stage, out spawnX, out spawnY);

            switch (deciderDirection)
            {
                case 0: // top
                    spawnY = 0;
                    if (isFirstTime) spawnY += (stage.Size.Height / 10);
                    break;
                case 1: // left
                    spawnX = 0;
                    if (isFirstTime) spawnX += (stage.Size.Width / 10);
                    break;
                case 2: // bottom
                    spawnY = stage.Size.Height;
                    if (isFirstTime) spawnY -= (stage.Size.Height / 10);
                    break;
                case 3: // right
                    spawnX = stage.Size.Width;
                    if (isFirstTime) spawnX -= (stage.Size.Width / 10);
                    break;
                default: break;
            }

            if (target.Location.X < spawnX) { speedX *= -1; }
            if (target.Location.Y < spawnY) { speedY *= -1; }

            _X = spawnX; _Y = spawnY;
            _speedX = speedX; _speedY = speedY;
        }
        public static Bullet Spawn(Panel stage, GameObject target)
        {
            return Spawn(stage, target, false);
        }
        /// <summary>
        /// spawning NEW Bullet on the stage fly toward "target" in randomized speed.
        /// 
        /// this Spawn method does not actually "aim" at the target, 
        /// but just get random speed and direction and then adjust the direction if it's not to the target at all.
        /// 
        /// </summary>
        /// <param name="stage"></param>
        /// where the Bullet belongs to.
        /// 
        /// <param name="target"></param>
        /// The Bullets will fly toward target's location.
        /// 
        /// <param name="isFirstTime"></param>
        /// when It's the first time spawning(in the beginning of the game), the bullets will spawn closer to the center.
        /// otherwise, they'll spawn on the border.
        /// <returns></returns>
        public static Bullet Spawn(Panel stage, GameObject target, bool isFirstTime)
        {
            int spawnX, spawnY;
            double speedX, speedY;

            RandomizedSpawn(stage, target, isFirstTime, out spawnX, out spawnY, out speedX, out speedY);

            return new Bullet(spawnX, spawnY, speedX, speedY);
        }

    }
    
    public class HomingBullet : Bullet
    {
        public static new Image defaultImage = Resources.homingBullet;
        public static double defaultAcceleration = 0.005;

        public double acceleration;
        GameObject target;
        
        HomingBullet(int X, int Y,  double speedX, double speedY, GameObject target) : base(X, Y, speedX, speedY, true)
        {
            this.SetAsset();
            this.acceleration = defaultAcceleration;
            this.target = target;
        }
        HomingBullet(int X, int Y,  double speedX, double speedY, double acceleration, GameObject target) : base(X, Y, speedX, speedY)
        {
            this.acceleration = acceleration;
            this.target = target;
        }

        public override void Fly()
        {
            //bool needTurnX = false;
            //bool needTurnY = false;
            
            /*if (this.speedX * (this.target.Location.X - this.Location.X) < 0) { needTurnX = true; }
            if (this.speedY * (this.target.Location.Y - this.Location.Y) < 0) { needTurnY = true; }

            if(needTurnX && needTurnY)
            {
                
            }
            else
            {
                if (needTurnX)
                {
                    Math.Sign(speedX)
                }
                if (needTurnY)
                {

                }
            }*/

            if (this.Location.X > this.target.Location.X) {
                speedX -= acceleration; }
            else{
                speedX += acceleration; }

            if (this.Location.Y > this.target.Location.Y) { 
                speedY -= acceleration; }
            else { 
                speedY += acceleration; }

            double velocity = (Math.Pow(speedX,2) + Math.Pow(speedY,2));
            if (velocity > Math.Pow(MaxSpeed,2)) 
            {
                speedX = speedX * (MaxSpeed / velocity);
                speedY = speedY * (MaxSpeed / velocity);
            }

            this.X += speedX; this.Y += speedY;
            this.SetLocation(X, Y);
        }

        public static new HomingBullet Spawn(Panel stage, GameObject target)
        {
            int spawnX, spawnY;
            double speedX, speedY; 
            RandomizedSpawn(stage, target, false, out spawnX, out spawnY, out speedX, out speedY);
            return new HomingBullet(spawnX, spawnY, speedX, speedY, target);
        }


    }
    

    // To do list

    // 
    //
    // Consider if it's more efficient separate GameObject.Asset as a struct, or a ConFiguration class.
    //
    // Consider to make a struct holding GameObject's Cordinate related varibles.
    //   ==> named like Physics?
    //
    // Make Replay Option 
    // 
    // Organize codes looks more consistent.
    //
    // Separate the Whole Game as a class Dodge. So that it can be run on other Windows Forms.
    // Consider attatch other games into ONE platform. 
    //
    // Add 2 Players Mode
}
