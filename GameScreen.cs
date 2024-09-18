using Raylib_cs;
using System.Numerics;

namespace pong
{
    enum GameScene
    {
        LOGO = 0,
        TITLE = 1,
        GAMEPLAY = 2,
        ENDING = 3,
        PAUSE = 4,
        EXIT = 5
    }

    internal class GameScreen(int with, int height)
    {
        readonly int with = with;
        readonly int height = height;
        public GameScene ActualScene { get; set; } = GameScene.LOGO;

        public void DrawLogo()
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.Black);

            Raylib.DrawText("Logo", (with / 2) - 50, height / 2, 100, Color.White);

            Raylib.EndDrawing();
        }

        public GameScene DrawTitle(SoundEngine soundEngine)
        {
            Vector2 mousePosition = Raylib.GetMousePosition();
            Rectangle rectPlay = new((with / 2) - 100, (height / 2) - 10, 300, 75);
            Rectangle rectExit = new((with / 2) - 100, ((height / 2) + 100) - 10, 300, 75);

            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.Black);

            Raylib.DrawText("PONG", (with / 2) - 100, height / 4, 100, Color.White);

            Raylib.DrawRectangleLinesEx(rectPlay, 1.0f, Color.Black);
            Raylib.DrawText("Play", (with / 2) - 50, height / 2, 50, Color.White);

            Raylib.DrawRectangleLinesEx(rectExit, 1.0f, Color.Black);
            Raylib.DrawText("Exit", (with / 2) - 50, height / 2 + 100, 50, Color.White);

            Raylib.DrawText("by MigueldeMollet", (int)(with * 0.92), (int)(height * 0.94), 15, Color.White);

            if (Raylib.CheckCollisionPointRec(mousePosition, rectPlay))
            {
                Raylib.DrawRectangleLinesEx(rectPlay, 1.0f, Color.White);
            }
            else if (Raylib.CheckCollisionPointRec(mousePosition, rectExit))
            {
                Raylib.DrawRectangleLinesEx(rectExit, 1.0f, Color.White);
            }

            if (Raylib.IsMouseButtonPressed(MouseButton.Left))
            {
                if (Raylib.CheckCollisionPointRec(mousePosition, rectPlay))
                {
                    soundEngine.PlayClick();
                    return GameScene.GAMEPLAY;
                }
                else if (Raylib.CheckCollisionPointRec(mousePosition, rectExit))
                {
                    soundEngine.PlayClick();
                    return GameScene.EXIT;
                }
            }

            Raylib.EndDrawing();

            return GameScene.TITLE;
        }

        public void DrawGame(Game game)
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.Black);

            int numberLines = height / 10;
            int centerScreen = with / 2;

            Color color;

            for (int i = 0; i <= numberLines; i++)
            {
                if (i % 2 == 0) color = Color.Red;
                else color = Color.White;

                int startY = i * numberLines;
                Raylib.DrawRectangle(centerScreen - 3, startY, 6, numberLines, color);
            }

            Raylib.DrawText(game.Player1.Score.ToString(), centerScreen - (centerScreen / 4), 40, 100, Color.Magenta);
            Raylib.DrawText(game.Player2.Score.ToString(), centerScreen + (centerScreen / 4), 40, 100, Color.Magenta);

            game.Player1.Draw();
            game.Player2.Draw();
            game.Ball.Draw();

            Raylib.EndDrawing();
        }

        public GameScene DrawMenuPause(SoundEngine soundEngine)
        {
            Vector2 mousePosition = Raylib.GetMousePosition();
            Rectangle rectResume = new((with / 2) - 100, (height / 2) - 10, 400, 75);
            Rectangle rectGoBack = new((with / 2) - 100, ((height / 2) + 100) - 10, 400, 75);

            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.Black);

            Raylib.DrawText("Pause", (with / 2) - 100, height / 4, 100, Color.White);

            Raylib.DrawRectangleLinesEx(rectResume, 1.0f, Color.Black);
            Raylib.DrawText("Resume", (with / 2) - 50, height / 2, 50, Color.White);

            Raylib.DrawRectangleLinesEx(rectGoBack, 1.0f, Color.Black);
            Raylib.DrawText("Go to Menu", (with / 2) - 50, height / 2 + 100, 50, Color.White);

            if (Raylib.CheckCollisionPointRec(mousePosition, rectResume))
            {
                Raylib.DrawRectangleLinesEx(rectResume, 1.0f, Color.White);
            }
            else if (Raylib.CheckCollisionPointRec(mousePosition, rectGoBack))
            {
                Raylib.DrawRectangleLinesEx(rectGoBack, 1.0f, Color.White);
            }

            if (Raylib.IsMouseButtonReleased(MouseButton.Left))
            {
                if (Raylib.CheckCollisionPointRec(mousePosition, rectResume))
                {
                    soundEngine.PlayClick();
                    return GameScene.GAMEPLAY;
                }
                else if (Raylib.CheckCollisionPointRec(mousePosition, rectGoBack))
                {
                    soundEngine.PlayClick();
                    return GameScene.TITLE;
                }
            }

            Raylib.EndDrawing();

            return GameScene.PAUSE;
        }

        public void DrawEnding(string player)
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.Black);

            Raylib.DrawText("Congratulations!!", (with / 2) - 420, (height / 4), 100, Color.White);
            Raylib.DrawText($"{player} Wins.", (with / 2) - 200, (height / 2) - 20, 50, Color.White);
            Raylib.DrawText("TAP SPACE to TITLE SCREEN", (with / 2) - 200, height - (height / 4) , 20, Color.Gray);

            Raylib.EndDrawing();
        }
    }
}
