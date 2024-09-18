using pong;
using Raylib_cs;

static GameScene update(Game game, SoundEngine soundEngine)
{
    if (Raylib.IsKeyDown(KeyboardKey.W)) game.Player1.MoveUp();
    if (Raylib.IsKeyDown(KeyboardKey.S)) game.Player1.MoveDown();
    if (Raylib.IsKeyDown(KeyboardKey.Up)) game.Player2.MoveUp();
    if (Raylib.IsKeyDown(KeyboardKey.Down)) game.Player2.MoveDown();
    if (Raylib.IsKeyDown(KeyboardKey.Escape)) return GameScene.PAUSE;

    int side = game.Ball.Move();

    if (game.Ball.CollisionWall)
    {
        soundEngine.PlayWall();
        game.Ball.CollisionWall = false;
    }

    Rectangle playerRect = new();
    Rectangle ballRect = game.Ball.Ball2Rectangle();

    if (game.BallCollisionPlayer(side))
    {
        soundEngine.PlayBlip();

        if (side == 1)
        {
            playerRect = game.Player1.Player2Rectangle();
            game.Ball.ChangeDirection((int)playerRect.Y, (int)playerRect.Height);
        }
        else
        {
            playerRect = game.Player2.Player2Rectangle();
            game.Ball.ChangeDirection((int)playerRect.Y, (int)playerRect.Height);
        }
    }
    else
    {
        if (ballRect.X >= Raylib.GetScreenWidth())
        {
            soundEngine.PlayScore();
            game.ScorePLayer1();
        } 
        else if (ballRect.X <= 0)
        {
            soundEngine.PlayScore();
            game.ScorePLayer2();
        }
    }

    if (game.isWinner(game.Player1.Score))
    {
        game.Winner = game.Player1.Name;
        return GameScene.ENDING;
    }
    else if (game.isWinner(game.Player2.Score))
    {
        game.Winner = game.Player2.Name;
        return GameScene.ENDING;
    }

    return GameScene.GAMEPLAY;
}


Raylib.InitWindow(0, 0, "Pong");
Raylib.InitAudioDevice();

Raylib.SetTargetFPS(60);

int with = Raylib.GetScreenWidth();
int height = Raylib.GetScreenHeight();

Game game = new("Player1", "player2", with, height);
SoundEngine soundEngine = new();
GameScreen gameScreen = new(with, height);

bool running = true;
int framesCounter = 0;

while(running)
{
    switch(gameScreen.ActualScene)
    {
        case GameScene.LOGO:
            
            while (framesCounter < 120)
            {
                gameScreen.DrawLogo();
                framesCounter++;
            }
            gameScreen.ActualScene = GameScene.TITLE;
            break;

        case GameScene.TITLE:
            while (gameScreen.ActualScene == GameScene.TITLE)
            {
                gameScreen.ActualScene = gameScreen.DrawTitle(soundEngine);
            }
            game = new("Player1", "player2", with, height);
            break;

        case GameScene.GAMEPLAY:
            while (gameScreen.ActualScene == GameScene.GAMEPLAY)
            {
                gameScreen.DrawGame(game);
                gameScreen.ActualScene = update(game, soundEngine);
            }
            break;

        case GameScene.ENDING:
            while (!Raylib.IsKeyDown(KeyboardKey.Space))
            {
                gameScreen.DrawEnding(game.Winner);
            }
            gameScreen.ActualScene = GameScene.TITLE;
            break;

        case GameScene.PAUSE:
            while (gameScreen.ActualScene == GameScene.PAUSE)
            {
                gameScreen.ActualScene = gameScreen.DrawMenuPause(soundEngine);
            }
            break;

        case GameScene.EXIT:
            running = false;
            break;
    }
}

Raylib.CloseAudioDevice();
Raylib.CloseWindow();