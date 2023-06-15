using dodge_practice.Properties;
using System;
using System.Resources;

namespace dodge_practice
{
    public partial class SpaceScreen : Form
    {
        Resources Resources { get { return Resources; } }

        Player player;
        List<Bullet> bullets = new List<Bullet>();

        int difficulty = 10;
        bool difficultyRising = false;
        bool GodMode = true;

        void SpawnBullet(bool isFirstTime)
        {
            bullets.Add(Bullet.Spawn(panel1, player, isFirstTime));
            panel1.Controls.Add(bullets[bullets.Count - 1]);
            //Console.WriteLine($"{bullets[bullets.Count - 1].X}\t{bullets[bullets.Count - 1].Y}\t");
        }
        void SpawnBullet()
        {
            SpawnBullet(false);
        }
        void SpawnBullet(string bulletType)
        {
            // bulletType should be "homing" here.
            // another bulletType can be added.
            bullets.Add(HomingBullet.Spawn(panel1, player));
            panel1.Controls.Add(bullets[bullets.Count - 1]);
        }
        public SpaceScreen()
        {
            InitializeComponent();

            player = new Player(Width / 2, Height / 2, 1.5);
            Bullet.MaxSpeed = 2.5;
            for (int i = 0; i < difficulty; i++)
            {
                SpawnBullet(true);
            }
            panel1.Controls.Add(player);
            infoTimer.Start();
            moveTimer.Start();
            spawnTimer.Start();
        }
        DateTime startTime;
        DateTime survivedTime;
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
                    spawnTimer.Stop();
                    MessageBox.Show("Hit!");
                }

                if (bullets[i].isOutOf(panel1))
                {
                    bullets[i].Dispose();
                    Bullet.countDodge++;
                    bullets.RemoveAt(i);
                }

            }
        }

        private void spawnTimer_tick(object sender, EventArgs e)
        {
            Random random = new Random(DateTime.Now.Millisecond);
            while (bullets.Count < difficulty)
            {
                if (bullets.Count % 2 == 0)
                {
                    SpawnBullet("homing");
                    continue;
                }

                SpawnBullet();
            }


            if ((spawnTimer.Interval > 1) && (difficulty < Bullet.countDodge) && difficultyRising)
            {
                difficulty++;
                spawnTimer.Interval--;
            }

        }


        private void WASD_Checker(object sender, PreviewKeyDownEventArgs e)
        {

            /*switch (e.KeyCode)
            {
                case Keys.W:
                case Keys.Up:
                    PlayerStatus.speedY = -PlayerStatus.speedDefault;
                    keyPress_vert++;
                    break;
                case Keys.S:
                case Keys.Down:
                    PlayerStatus.speedY = PlayerStatus.speedDefault;
                    keyPress_vert++;
                    break;
                case Keys.A:
                case Keys.Left:
                    PlayerStatus.speedX = -PlayerStatus.speedDefault;
                    keyPress_horz++;
                    break;
                case Keys.D:
                case Keys.Right:
                    PlayerStatus.speedX = PlayerStatus.speedDefault;
                    keyPress_horz++;
                    break;
                default: break;
            }*/

            if (e.KeyCode == Keys.W || e.KeyCode == Keys.Up)
            {
                player.speedY = -player.speedPrecise;
            }
            if (e.KeyCode == Keys.S || e.KeyCode == Keys.Down)
            {
                player.speedY = player.speedPrecise;
            }
            if (e.KeyCode == Keys.A || e.KeyCode == Keys.Left)
            {
                player.speedX = -player.speedPrecise;
            }
            if (e.KeyCode == Keys.D || e.KeyCode == Keys.Right)
            {
                player.speedX = player.speedPrecise;
            }
        }
        private void WASD_Checker(object sender, KeyEventArgs e)
        {
            /*switch (e.KeyCode)
            {
                case Keys.W:
                case Keys.Up:
                    PlayerStatus.speedY = -PlayerStatus.speedDefault;
                    keyPress_vert++;
                    break;
                case Keys.S:
                case Keys.Down:
                    PlayerStatus.speedY = PlayerStatus.speedDefault;
                    keyPress_vert++;
                    break;
                case Keys.A:
                case Keys.Left:
                    PlayerStatus.speedX = -PlayerStatus.speedDefault;
                    keyPress_horz++;
                    break;
                case Keys.D:
                case Keys.Right:
                    PlayerStatus.speedX = PlayerStatus.speedDefault;
                    keyPress_horz++;
                    break;
                default: break;
            }*/

            if (e.KeyCode == Keys.W || e.KeyCode == Keys.Up)
            {
                player.speedY = -player.speedPrecise;
            }
            if (e.KeyCode == Keys.S || e.KeyCode == Keys.Down)
            {
                player.speedY = player.speedPrecise;
            }
            if (e.KeyCode == Keys.A || e.KeyCode == Keys.Left)
            {
                player.speedX = -player.speedPrecise;
            }
            if (e.KeyCode == Keys.D || e.KeyCode == Keys.Right)
            {
                player.speedX = player.speedPrecise;
            }
        }
        private void WASD_keyup(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                case Keys.Up:
                    player.speedY = 0;
                    break;
                case Keys.S:
                case Keys.Down:
                    player.speedY = 0;
                    break;
                case Keys.A:
                case Keys.Left:
                    player.speedX = 0;
                    break;
                case Keys.D:
                case Keys.Right:
                    player.speedX = 0;
                    break;
                default: break;
            }
        }


    }
}