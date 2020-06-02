using System;
using System.Windows.Forms;

namespace MegaSnake
{
    public partial class Form1 : Form
    {
        private Game game = new Game();

        public Form1()
        {
            InitializeComponent();
            game.Restart();
            game.Defeat += OnDefeat;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            game.SnakesDirection(Keys.KeyCode);
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            game.Draw(e.Graphics);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            game.Update(Keys.KeyCode);
            pictureBox1.Refresh();
        }

        void OnDefeat()
        {
            timer1.Stop();
            MessageBox.Show("Game Over");
            game.Restart();
            timer1.Start();
        }
    }
}


