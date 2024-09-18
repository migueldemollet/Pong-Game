using Raylib_cs;

namespace pong
{
    internal class Ball
    {
        int startX;
        int startY;
        int x;
        int y;
        int startSpeedX;
        int startSpeedY;
        int speedX;
        int speedY;
        int side;
        
        readonly int with;
        readonly int height;
        readonly int heighScreen;

        public bool CollisionWall { get; set; }

        Random rn;
        Color color;

        public Ball(int x, int y, Color color)
        {
            this.startX = x;
            this.startY = y;

            this.x = startX;
            this.y = startY;

            this.heighScreen = y * 2;
            this.with = 20;
            this.height = 20;

            this.startSpeedX = 6;
            this.startSpeedY = 6;
            this.speedX = startSpeedX;
            this.speedY = startSpeedY;

            this.CollisionWall = false;

            this.color = Color.White;

            rn = new();
            this.side = rn.Next(2);

    }

    public int Move()
        {
            if (side == 0) x += speedX;
            else x -= speedX;

            y += speedY;

            if (y <= 0 || y + height >= heighScreen)
            {
                speedY *= -1;
                this.CollisionWall = true;
            }

            return side;
        }

        public void Draw()
        {
            Raylib.DrawRectangle(x, y, with, height, color);
        }

        public void ChangeDirection(int paddleY, int paddleHeight)
        {
            this.side = (side == 0) ? this.side = 1 : this.side = 0;
            
            int paddleCenter = paddleY + (paddleHeight / 2);
            int difference = (y + (with / 2)) - paddleCenter;

            float scalingFactor = 0.5f;
            this.speedY = (int)(difference * scalingFactor);

            int maxSpeedY = 20;
            this.speedY = Math.Clamp(speedY, -maxSpeedY, maxSpeedY);


            float scalingFactorX = 0.1f;
            int speedBoost = (int)(Math.Abs(difference) * scalingFactorX);

            speedX += speedBoost;

            int maxSpeedX = 20;
            this.speedX = Math.Clamp(speedX, 6, maxSpeedX);
        }

        public void Restart(int side)
        {
            this.side = side;

            this.x = startX;
            this.y = startY;

            this.speedX = startSpeedX;
            this.speedY = startSpeedY;
        }

        public Rectangle Ball2Rectangle()
        {
            return new(x, y, with, height);
        }
    }
}
