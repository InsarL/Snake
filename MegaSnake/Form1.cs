using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MegaSnake
{
    public partial class Form1 : Form
    {
        Point snakeHead;
        Point apple;
        static Random random;
        const int GameFieldSize = 20;
        Keys snakeDirection;
        List<Point> snakeBodyParts;

        public Form1()
        {
            InitializeComponent();
            random = new Random();
            snakeBodyParts = new List<Point>();
            Restart();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (snakeBodyParts.Count > 0)
            {
                if (e.KeyCode == Keys.Down && snakeDirection == Keys.Up)
                    return;
                if (e.KeyCode == Keys.Up && snakeDirection == Keys.Down)
                    return;
                if (e.KeyCode == Keys.Right && snakeDirection == Keys.Left)
                    return;
                if (e.KeyCode == Keys.Left && snakeDirection == Keys.Right)
                    return;
            }

            if (e.KeyCode == Keys.Down ||
                e.KeyCode == Keys.Up ||
                e.KeyCode == Keys.Right ||
                e.KeyCode == Keys.Left)
                snakeDirection = e.KeyCode;
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            const int CellSize = 20;

            for (int i = 0; i <= GameFieldSize; i++)
            {
                e.Graphics.DrawLine(Pens.Black, i * CellSize, 0, i * CellSize, GameFieldSize * CellSize);
                e.Graphics.DrawLine(Pens.Black, 0, i * CellSize, GameFieldSize * CellSize, i * CellSize);
            }

            foreach (Point bodyPart in snakeBodyParts)
                e.Graphics.FillRectangle(Brushes.Green, bodyPart.X * CellSize, bodyPart.Y * CellSize, CellSize, CellSize);

            e.Graphics.FillRectangle(Brushes.Red, snakeHead.X * CellSize, snakeHead.Y * CellSize, CellSize, CellSize);

            e.Graphics.FillRectangle(Brushes.Chocolate, apple.X * CellSize, apple.Y * CellSize, CellSize, CellSize);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (snakeDirection == Keys.Up)
                snakeHead.Y--;
            if (snakeDirection == Keys.Down)
                snakeHead.Y++;
            if (snakeDirection == Keys.Left)
                snakeHead.X--;
            if (snakeDirection == Keys.Right)
                snakeHead.X++;

            if (snakeHead.Y < 0 ||
                snakeHead.X < 0 ||
                snakeHead.Y >= GameFieldSize ||
                snakeHead.X >= GameFieldSize)
            {
                Defeat();
            }

            for (int i = 1; i < snakeBodyParts.Count; i++)
            {
                if (snakeBodyParts[i] == snakeHead)
                {
                    Defeat();
                }
            }

            if (snakeHead == apple)
            {
                do
                {
                    apple = new Point(random.Next(0, GameFieldSize), random.Next(0, GameFieldSize));
                }
                while (snakeBodyParts.Contains(apple));

            }
            else
                snakeBodyParts.RemoveAt(snakeBodyParts.Count - 1);

            snakeBodyParts.Insert(0, snakeHead);
            pictureBox1.Refresh();
        }

        void Defeat()
        {
            timer1.Stop();
            MessageBox.Show("Game Over");
            Restart();
            timer1.Start();
        }

        void Restart()
        {
            snakeDirection = Keys.A;
            apple = new Point(random.Next(0, GameFieldSize), random.Next(0, GameFieldSize));
            snakeHead = new Point(GameFieldSize / 2, GameFieldSize / 2);
            snakeBodyParts.Clear();
            snakeBodyParts.Add(snakeHead);
        }
    }
}


