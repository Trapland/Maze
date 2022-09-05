using System.Drawing;
using System.Windows.Forms;
using System.Xml.Schema;

namespace Maze
{
    public partial class Form1 : Form
    {
        Labirint l;
        public int x_change = 0;
        public int y_change = 0;
        public int hp = 100;
        public int medals = 0;
        public Form1()
        {
            InitializeComponent();
            Options();
            StartGame();
        }

        public void Options()
        {

            BackColor = Color.FromArgb(255, 92, 118, 137);

            int sizeX = 40;
            int sizeY = 20;

            Width = sizeX * 16 + 16;
            Height = sizeY * 16 + 40;
            StartPosition = FormStartPosition.CenterScreen;
        }

        public void StartGame()
        {
            l = new Labirint(this, 40, 20);

            this.Text = "Medals: " + medals.ToString() + "/" + l.totalmedals.ToString() + "  HP: " + hp.ToString() + "/100";
            l.Show();

        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A || e.KeyCode == Keys.Left)
            {
                x_change = -1;
                y_change = 0;
            }
            else if (e.KeyCode == Keys.D || e.KeyCode == Keys.Right)
            {
                x_change = 1;
                y_change = 0;
            }
            else if (e.KeyCode == Keys.W || e.KeyCode == Keys.Up)
            {
                x_change = 0;
                y_change = -1;
            }
            else if (e.KeyCode == Keys.S || e.KeyCode == Keys.Down)
            {
                x_change = 0;
                y_change = 1;
            }
            else
            {
                x_change = 0;
                y_change = 0;
            }
            if (l.maze[l.smileY + y_change, l.smileX + x_change].type == MazeObject.MazeObjectType.ENEMY)
            {
                hp -= 25;
            }
            if (l.maze[l.smileY + y_change, l.smileX + x_change].type == MazeObject.MazeObjectType.MEDAL)
            {
                medals++;
            }
            if (l.maze[l.smileY + y_change, l.smileX + x_change].type != l.maze[0, 0].type)
            {
                l.maze[l.smileY + y_change, l.smileX + x_change] = l.maze[l.smileY, l.smileX];
                l.images[l.smileY + y_change, l.smileX + x_change].BackgroundImage = l.images[l.smileY, l.smileX].BackgroundImage;
                l.maze[l.smileY, l.smileX] = new MazeObject(MazeObject.MazeObjectType.HALL);
                l.images[l.smileY, l.smileX].BackgroundImage = l.maze[l.smileY, l.smileX].texture;
                l.smileX += x_change;
                l.smileY += y_change;
            }
            if (l.totalmedals == medals)
            {
                MessageBox.Show("You win by collecting medals!");
                this.Close();
            }
            if (l.smileX == l.width - 1 && l.smileY == l.height - 3)
            {
                MessageBox.Show("You win!");
                this.Close();
            }
            if (hp <= 0)
            {
                MessageBox.Show("You lose(");
                this.Close();
            }

            this.Text = "Medals: " + medals.ToString() + "/" + l.totalmedals.ToString() + "  HP: " + hp.ToString() + "/100";
        }

        private void timer1_Tick(object sender, System.EventArgs e)
        {

            l.Show();
        }
    }
}
