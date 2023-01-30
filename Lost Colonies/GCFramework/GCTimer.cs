using Microsoft.Xna.Framework;

namespace Gamecodeur
{
    public class GCTimer
    {
        double _duration;
        double _current;
        public bool Ended { get; private set; }

        public GCTimer(float pDuration)
        {
            _duration = pDuration;
            _current = 0;
            Ended = false;
        }

        public void Update(GameTime gameTime)
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            _current += dt;

            if (_current >= _duration)
            {
                Ended = true;
                _current = 0;
            }
            else
            {
                Ended = false;
            }
        }
    }
}
