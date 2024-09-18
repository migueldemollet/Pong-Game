using Raylib_cs;

namespace pong
{
    internal class SoundEngine
    {
        Sound blip;
        Sound wall;
        Sound score;
        Sound click;

        public SoundEngine()
        {
            blip = Raylib.LoadSound("./Resources/blip.wav");
            wall = Raylib.LoadSound("./Resources/wall.mp3");
            score = Raylib.LoadSound("./Resources/score.mp3");
            click = Raylib.LoadSound("./Resources/click.mp3");
        }

        ~SoundEngine()
        {
            Raylib.UnloadSound(blip);
            Raylib.UnloadSound(wall);
            Raylib.UnloadSound(score);
        }

        public void PlayBlip()
        {
            Raylib.PlaySound(blip);
        }

        public void PlayWall()
        {
            Raylib.PlaySound(wall);
        }

        public void PlayScore()
        {
            Raylib.PlaySound(score);
        }

        public void PlayClick()
        {
            Raylib.PlaySound(click);
        }


    }
}
