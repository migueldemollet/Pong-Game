using Raylib_cs;

namespace pong
{
    internal class Game(string namePlayer1, string namePlayer2, int with, int height)
    {
        readonly int pointToWin = 11;
        public Player Player1 { get; set; } = new(namePlayer1, (int)(with * 0.05f), (height / 2), Color.Red);
        public Player Player2 { get; set; } = new(namePlayer2, (int)(with * 0.94f), (height / 2), Color.Blue);

        public Ball Ball { get; set; } = new Ball((with / 2), (height /2), Color.White);

        public string Winner { get; set; } = ""; 

        public void ScorePLayer1()
        {
            Ball.Restart(0);
            Player1.Score++;
        }

        public void ScorePLayer2()
        {
            Ball.Restart(1);
            Player2.Score++;
        }

        public bool BallCollisionPlayer(int side)
        {
            if (side == 1)
            {
                return Raylib.CheckCollisionRecs(Player1.Player2Rectangle(), Ball.Ball2Rectangle());
            }
            else
            {
                return Raylib.CheckCollisionRecs(Player2.Player2Rectangle(), Ball.Ball2Rectangle());
            }
        }
        public bool isWinner(int score)
        {
            if (score >= pointToWin) return true;

            return false;
        }
    }
}
