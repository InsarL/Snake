using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace MegaSnake
{
    class Game
    {
        public Keys snakeDirection;
        public int GameFieldSize = 20;
        private List<Point> snakeBodyParts = new List<Point>();
        public Point snakeHead;
        public Point apple;
        public static Random random = new Random();
        public event Action Defeat;

        public void Draw(Graphics graphics)
        {
            const int CellSize = 20;

            for (int i = 0; i <= GameFieldSize; i++)
            {
                graphics.DrawLine(Pens.Black, i * CellSize, 0, i * CellSize, GameFieldSize * CellSize);
                graphics.DrawLine(Pens.Black, 0, i * CellSize, GameFieldSize * CellSize, i * CellSize);
            }

            foreach (Point bodyPart in snakeBodyParts)
                graphics.FillRectangle(Brushes.Green, bodyPart.X * CellSize, bodyPart.Y * CellSize, CellSize, CellSize);

            graphics.FillRectangle(Brushes.Red, snakeHead.X * CellSize, snakeHead.Y * CellSize, CellSize, CellSize);

            graphics.FillRectangle(Brushes.Chocolate, apple.X * CellSize, apple.Y * CellSize, CellSize, CellSize);
        }

        public void Restart()
        {
            apple = new Point(random.Next(0, GameFieldSize), random.Next(0, GameFieldSize));
            snakeHead = new Point(GameFieldSize / 2, GameFieldSize / 2);
            snakeBodyParts.Clear();
            snakeBodyParts.Add(snakeHead);
            snakeDirection = Keys.A;
        }

        public Keys SnakesDirection(Keys key)
        {
            if (key == Keys.Down ||
            key == Keys.Up ||
            key == Keys.Right ||
            key == Keys.Left)
           snakeDirection = key;

            if (snakeBodyParts.Count > 0)
            {
                if (key == Keys.Down && snakeDirection == Keys.Up)
                    return Keys.None;
                if (key == Keys.Up && snakeDirection == Keys.Down)
                    return Keys.None;
                if (key == Keys.Right && snakeDirection == Keys.Left)
                    return Keys.None;
                if (key == Keys.Left && snakeDirection == Keys.Right)
                    return Keys.None;
            }

            return snakeDirection;
        }

        public void Update(Keys key)
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
                snakeDirection = Keys.A;
                Defeat();
            }

            for (int i = 1; i < snakeBodyParts.Count; i++)
            {
                if (snakeBodyParts[i] == snakeHead)
                {
                    snakeDirection = Keys.A;
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
        }
    }
}
