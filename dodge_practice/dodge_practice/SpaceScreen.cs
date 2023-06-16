using dodge_practice.Properties;
using System;
using System.Resources;

namespace dodge_practice
{
    public partial class SpaceScreen : Form
    {
        ////////////
        // Assets //
        ////////////

        //Resources Resources { get { return Resources; } }
        Player player;
        List<Bullet> bullets = new List<Bullet>();

        ////////////////////
        // Configurations //
        ////////////////////

        int difficulty = 25;
        int homingRatio = 20;
        bool difficultyRising = true;
        bool GodMode = true;

        //////////////////////
        // Respawn Settings //
        //////////////////////

        void SpawnBullet(string bulletType, bool isFirstTime)
        {
            switch (bulletType)
            {
                case "HomingBullet":
                    bullets.Add(HomingBullet.Spawn(panel1, player, isFirstTime));
                    break;
                case "Bullet":
                    bullets.Add(Bullet.Spawn(panel1, player, isFirstTime));
                    break;
                default:
                    break;
            }
            Bullet.countSpawn++;
            panel1.Controls.Add(bullets[bullets.Count - 1]);
        }
        void InitBullets()
        {
            InitBullets(true, false);
        }
        void InitBullets(bool isSpecialAllowed, bool isFirstTime)
        {
            while (bullets.Count < difficulty)
            {
                if (isSpecialAllowed && (Bullet.countSpawn % homingRatio == 0))
                {
                    SpawnBullet("HomingBullet", isFirstTime);
                    continue;
                }

                SpawnBullet("Bullet", isFirstTime);
            }
        }

        //////////////////////
        // Main Constructor //
        //////////////////////

        public SpaceScreen()
        {
            InitializeComponent();

            player = new Player(Width / 2, Height / 2, 1.5);
            Bullet.MaxSpeed = 2.5;

            InitBullets(false, true);
            panel1.Controls.Add(player);
            infoTimer.Start();
            moveTimer.Start();
            difficultyTimer.Start();
        }
        DateTime startTime;
        DateTime survivedTime;

        //////////////////////////
        // Timer event Settings //
        //////////////////////////

        private void infoTimer_tick(object sender, EventArgs e)
        {
            InfoBoard.Text =
                $"X\t{player.X}\t{player.speedX}\n" +
                $"Y\t{player.Y}\t{player.speedY}";
            ScoreBoard.Text = "bullet\t" + bullets.Count + "\nscore\t" + Bullet.countDodge;

        }
        private void moveTimer_tick(object sender, EventArgs e)
        {
            player.Fly();

            for (int i = bullets.Count - 1; i >= 0; i--)
            {

                bullets[i].Fly();

                if (bullets[i].isHitBy(player) && !GodMode)
                {
                    moveTimer.Stop();
                    difficultyTimer.Stop();
                    MessageBox.Show("Hit!");
                    restart();
                }

                if (bullets[i].isOutOf(panel1))
                {
                    bullets[i].Dispose();
                    Bullet.countDodge++;
                    bullets.RemoveAt(i);
                }

                InitBullets();
            }
        }
        private void restart()
        {
            throw new NotImplementedException();
        }

        private void difficultyTimer_tick(object sender, EventArgs e)
        {
            Random random = new Random(DateTime.Now.Millisecond);

            if ((difficultyTimer.Interval > 1) && (difficulty < Bullet.countDodge) && difficultyRising)
            {
                difficulty++;
                difficultyTimer.Interval--;
            }

        }

        /////////////////////////////
        // Keyboard event Settings //
        /////////////////////////////
        bool[] wsadStatus = new bool[4];
        
        private void WASD_Checker(object sender, EventArgs ee)
        {
            Keys detectedKey;
            if (ee is KeyEventArgs) { detectedKey = (ee as KeyEventArgs).KeyCode; }
            else if (ee is PreviewKeyDownEventArgs) { detectedKey = (ee as PreviewKeyDownEventArgs).KeyCode; }
            else { return; }

            if (detectedKey == Keys.W || detectedKey == Keys.Up)
            {
                player.speedY = wsadStatus[1]? 0: -player.speedPrecise;
                wsadStatus[0] = true;
            }
            if (detectedKey == Keys.S || detectedKey == Keys.Down)
            {
                player.speedY = wsadStatus[0] ? 0 : player.speedPrecise;
                wsadStatus[1] = true;
            }
            if (detectedKey == Keys.A || detectedKey == Keys.Left)
            {
                player.speedX = wsadStatus[3] ? 0 : -player.speedPrecise;
                wsadStatus[2] = true;
            }
            if (detectedKey == Keys.D || detectedKey == Keys.Right)
            {
                player.speedX = wsadStatus[2] ? 0 : player.speedPrecise;
                wsadStatus[3] = true;
            }
        }
        private void WASD_keyup(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W: case Keys.Up:
                    player.speedY = wsadStatus[1]? player.speedPrecise: 0;
                    wsadStatus[0] = false;
                    break;
                case Keys.S: case Keys.Down:
                    player.speedY = wsadStatus[0] ? -player.speedPrecise : 0;
                    wsadStatus[1] = false;
                    break;
                case Keys.A: case Keys.Left:
                    player.speedX = wsadStatus[3] ? player.speedPrecise : 0;
                    wsadStatus[2] = false;
                    break;
                case Keys.D: case Keys.Right:
                    player.speedX = wsadStatus[2] ? -player.speedPrecise : 0;
                    wsadStatus[3] = false;
                    break;
                default: break;
            }
            
        }


    }
}