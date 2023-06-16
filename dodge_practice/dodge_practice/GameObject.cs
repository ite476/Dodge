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
    
    public class GameAsset
    {
        private static Dictionary<string, int> AssetIndex = new Dictionary<string, int>{
            { "dodge_practice.Player", 0 },
            { "dodge_practice.Bullet", 1 },
            { "dodge_practice.HomingBullet", 2 },

            { "dodge_practice.GameObject", -1 },
            };
        private static Size[] SizeByKey = { 
            new Size(10, 10), 
            new Size(4, 4), 
            new Size(4, 4) 
        };
        private static Image[] ImageByKey = { 
            Resources.player, 
            Resources.bullet, 
            Resources.homingBullet 
        };

        public static Size GetSize(string key) { return SizeByKey[AssetIndex[key]]; }
        public static Image GetImage(string key) {  return ImageByKey[AssetIndex[key]]; }
    }
    public class GameObject : PictureBox
    {
        //////////////////////////////
        // Cordinate, Velocity Info //
        //////////////////////////////
        
        public double X { get; set; }
        public double Y { get; set; }
        public double speedTotal = 0;
        public double speedX = 0;
        public double speedY = 0;

        /////////////////
        // Constructor //
        /////////////////
        
        public GameObject(int X, int Y)
        {
            this.SetAsset(this.GetType().ToString());
            this.SetLocation(X, Y);
        }
        public GameObject(double X, double Y)
        {
            this.SetAsset(this.GetType().ToString());
            this.SetLocation(X, Y);
        }
        public GameObject(double X, double Y, double speedX, double speedY) : this(X, Y)
        {
            this.SetAsset(this.GetType().ToString());
            this.SetLocation(X, Y);
            this.SetSpeed(speedX, speedY);
        }

        /////////////////////////
        // Initializer Methods //
        // DONT OVERRIDE       //
        /////////////////////////

        protected void SetLocation(double X, double Y)
        {
            this.Location = new Point((int)X, (int)Y);
            this.X = X; this.Y = Y;
        }
        protected void SetAsset(string GameObjectType) 
        {
            this.Size = GameAsset.GetSize(GameObjectType);
            this.Image = GameAsset.GetImage(GameObjectType);
        }
        protected void SetSpeed(double speedX, double speedY)
        {
            this.speedX = speedX;
            this.speedY = speedY;
        }

        ///////////////////////
        // Relocating Method //
        ///////////////////////
        
        public virtual void Fly()
        {
            X += this.speedX; Y += this.speedY;
            this.SetLocation(X, Y);
        }

        ///////////////////
        // Status Method //
        ///////////////////
        
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
        /////////////////
        // Player Info //
        /////////////////
        
        public int speedDefault = 2;
        public double speedPrecise = 1.5;
        public bool isAlive = true;

        ///////////////////////////////
        // Constructor & Init Method //
        ///////////////////////////////
        
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
        ///////////////////////////////////////////
        // Bullet Unique Info ( and Statistics ) //
        ///////////////////////////////////////////

        public static int countDodge = 0;
        public static int countSpawn = 0;
        public static double MaxSpeed = 3;

        ///////////////////////////////////////////
        // Constructor & Randomized Init Methods //
        ///////////////////////////////////////////

        protected Bullet(int X, int Y, double speedX, double speedY) : base(X, Y, speedX, speedY) { }
        protected static void RandomSpeed(Random random, double minSpeedRatio, out double speedX, out double speedY)
        {
            double speedTotal = Math.Pow(
                (minSpeedRatio + (1 - minSpeedRatio) * random.NextDouble()) * Bullet.MaxSpeed,
                2);
            double XYratio = random.NextDouble();
            speedX = Math.Sqrt(XYratio * speedTotal);
            speedY = Math.Sqrt((1-XYratio) * speedTotal);
        }
        protected static void RandomLocation(Random random, Panel stage, out int X, out int Y)
        {
            double WidthRatio = random.NextDouble();
            double HeightRatio = random.NextDouble();
            X = (int)(stage.Width * WidthRatio);
            Y = (int)(stage.Height * HeightRatio);
        }
        protected static void RandomInit(Panel stage, GameObject target, bool isFirstTime, out int X, out int Y, out double speedX, out double speedY)
        {
            Random random = new Random();
            int deciderDirection = random.Next(0, 4);
            double lspeedX, lspeedY;
            int lX, lY;

            RandomSpeed(random, 0.6, out lspeedX, out lspeedY);           
            RandomLocation(random, stage, out lX, out lY);

            switch (deciderDirection)
            {
                case 0: // top
                    lY = 0;
                    if (isFirstTime) lY += (stage.Size.Height / 10);
                    break;
                case 1: // left
                    lX = 0;
                    if (isFirstTime) lX += (stage.Size.Width / 10);
                    break;
                case 2: // bottom
                    lY = stage.Size.Height;
                    if (isFirstTime) lY -= (stage.Size.Height / 10);
                    break;
                case 3: // right
                    lX = stage.Size.Width;
                    if (isFirstTime) lX -= (stage.Size.Width / 10);
                    break;
                default: break;
            }

            if (target.Location.X < lX) { lspeedX *= -1; }
            if (target.Location.Y < lY) { lspeedY *= -1; }

            X = lX; Y = lY;
            speedX = lspeedX; speedY = lspeedY;            
        }

        /////////////////////////////
        // Public Construct Method //
        /////////////////////////////
        
        public static Bullet Spawn(Panel stage, GameObject target)
        {
            return Spawn(stage, target, false);
        }
        public static Bullet Spawn(Panel stage, GameObject target, bool isFirstTime)
        {
            int spawnX, spawnY;
            double speedX, speedY;

            RandomInit(stage, target, isFirstTime, out spawnX, out spawnY, out speedX, out speedY);

            return new Bullet(spawnX, spawnY, speedX, speedY);
        }

    }
    public class HomingBullet : Bullet
    {
        ///////////////////////
        // HomingBullet Info //
        ///////////////////////
        
        public static double defaultAcceleration = 0.005;
        public double acceleration = 0;
        GameObject target;

        /////////////////
        // Constructor //
        /////////////////
        protected HomingBullet(int X, int Y,  double speedX, double speedY, GameObject target) : base(X, Y, speedX, speedY)
        {
            this.acceleration = defaultAcceleration; 
            this.target = target;
        }
        protected HomingBullet(int X, int Y,  double speedX, double speedY, double acceleration, GameObject target) : base(X, Y, speedX, speedY)
        {
            this.acceleration = acceleration;
            this.target = target;
        }

        ////////////////////////////////
        // Overrided Fly() Method     //
        // Added Homing Function Here //
        ////////////////////////////////
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

            speedTotal = (Math.Pow(speedX,2) + Math.Pow(speedY,2));
            if (speedTotal > Math.Pow(MaxSpeed,2)) 
            {
                speedX = speedX * Math.Sqrt((MaxSpeed / speedTotal));
                speedY = speedY * Math.Sqrt((MaxSpeed / speedTotal));
            }

            this.X += speedX; this.Y += speedY;
            this.SetLocation(X, Y);
        }

        /////////////////////////////
        // Public Construct Method //
        /////////////////////////////
        public static new HomingBullet Spawn(Panel stage, GameObject target, bool isFirstTime)
        {
            int spawnX, spawnY;
            double speedX, speedY; 
            RandomInit(stage, target, isFirstTime, out spawnX, out spawnY, out speedX, out speedY);
            return new HomingBullet(spawnX, spawnY, speedX, speedY, target);
        }

    }
    

    /* To do list

    // 
    //
    // Consider if it's more efficient separate GameObject.Asset as a struct, or a ConFiguration class.
    //
    /// Done // Consider to make a struct holding GameObject's Cordinate related varibles.
    /// By Comments Separation
    //
    // Make Replay Option 
    // 
    /// Done // Organize codes looks more consistent.
    //
    // Separate the Whole Game as a class Dodge. So that it can be run on other Windows Forms.
    // Consider attatch other games into ONE platform. 
    //
    // Add 2 Players Mode
    */
}
