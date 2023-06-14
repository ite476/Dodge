using dodge_practice.Properties;
using System.Resources;

namespace dodge_practice
{
    public partial class SpaceScreen : Form
    {
        Resources Resources { get { return Resources; } }

        Player player;
        List<Bullet> bullets = new List<Bullet>();

        int difficulty = 50;

        void SpawnBullet(bool isFirstTime)
        {

        }
        void SpawnBullet()
        {
            SpawnBullet(false);
        }
        public SpaceScreen()
        {
            InitializeComponent();

            player = new Player(Width /2 , Height / 2, 1.5);
            Bullet.MaxSpeed = 2;
            for (int i = 0; i < difficulty; i++)
            {
                bullets.Add(Bullet.Spawn(panel1, player, true));                
            }
            panel1.Controls.Add(player);
            infoTimer.Start();
            moveTimer.Start();
            spawnTimer.Start();
        }

        private void infoTimer_tick(object sender, EventArgs e)
        {
            label1.Text = "X\t" + player.X + "\t" + player.speedX_pr;
            label2.Text = "Y\t" + player.Y + "\t" + player.speedY_pr;
            ScoreBoard.Text = "bullet\t" + bullets.Count + "\nscore\t" + Bullet.countDodge;

        }
        private void moveTimer_tick(object sender, EventArgs e)
        {
            player.Fly(player.speedX_pr, player.speedY_pr);

            for (int i = bullets.Count - 1; i >= 0; i--)
            {

                bullets[i].Fly(bullets[i].speedX_pr, bullets[i].speedY_pr);
                /* // Hit Calculation
                if (bullets[i].isHitBy(player))
                { MessageBox.Show("Hit!"); }    
                */
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
                bullets.Add(Bullet.Spawn(panel1, player));
            }


            if ((spawnTimer.Interval > 1) && (difficulty < Bullet.countDodge))
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
                player.speedY = -player.speedDefault;
                player.speedY_pr = -player.speedPrecise;
            }
            if (e.KeyCode == Keys.S || e.KeyCode == Keys.Down)
            {
                player.speedY = player.speedDefault;
                player.speedY_pr = player.speedPrecise;
            }
            if (e.KeyCode == Keys.A || e.KeyCode == Keys.Left)
            {
                player.speedX = -player.speedDefault;
                player.speedX_pr = -player.speedPrecise;
            }
            if (e.KeyCode == Keys.D || e.KeyCode == Keys.Right)
            {
                player.speedX = player.speedDefault;
                player.speedX_pr = player.speedPrecise;
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
                player.speedY = -player.speedDefault;
                player.speedY_pr = -player.speedPrecise;
            }
            if (e.KeyCode == Keys.S || e.KeyCode == Keys.Down)
            {
                player.speedY = player.speedDefault;
                player.speedY_pr = player.speedPrecise;
            }
            if (e.KeyCode == Keys.A || e.KeyCode == Keys.Left)
            {
                player.speedX = -player.speedDefault;
                player.speedX_pr = -player.speedPrecise;
            }
            if (e.KeyCode == Keys.D || e.KeyCode == Keys.Right)
            {
                player.speedX = player.speedDefault;
                player.speedX_pr = player.speedPrecise;
            }
        }
        private void WASD_keyup(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                case Keys.Up:
                    player.speedY = 0;
                    player.speedY_pr = 0;
                    break;
                case Keys.S:
                case Keys.Down:
                    player.speedY = 0;
                    player.speedY_pr = 0;
                    break;
                case Keys.A:
                case Keys.Left:
                    player.speedX = 0;
                    player.speedX_pr = 0;
                    break;
                case Keys.D:
                case Keys.Right:
                    player.speedX = 0;
                    player.speedX_pr = 0;
                    break;
                default: break;
            }
        }




        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }


    }
}