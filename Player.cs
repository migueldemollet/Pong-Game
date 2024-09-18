using Raylib_cs;

namespace pong
{
    internal class Player
    {
        int x;
        int y;
        readonly int minY;
        readonly int maxY;
        readonly int velocity;
        readonly int with;
        readonly int height;

        Color color;

        public string Name { get; }
        public int Score { get; set; }

        public Player(string name, int x, int y, Color color)
        {
            this.Name = name;
            this.x = x;
            this.y = y;

            this.Score = 0;
            this.minY = 0;
            this.maxY = y * 2;
            this.velocity = 15;
            this.with = 20;
            this.height = with * 10;

            this.color = color;
        }

        public Rectangle Player2Rectangle()
        {
            return new(x, y, with, height);
        }

        public void MoveUp()
        {
            y -= velocity;
            if (y < minY) this.y = minY;
        }

        public void MoveDown()
        {
            y += velocity;
            if (y + height > maxY) this.y = maxY - height;
        }

        public void Draw()
        {
            Raylib.DrawRectangle(x, y, with, height, color);
        }
    }
}
