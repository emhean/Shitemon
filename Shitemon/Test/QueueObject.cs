using Microsoft.Xna.Framework.Graphics;
using System;

namespace Shitemon.Test
{
    class QueueObject
    {
        public bool Expired { get; set; }
        public event EventHandler<EventArgs> Expiring;

        public void OnExpiring()
        {
            if (!Expired)
            {
                Expiring?.Invoke(this, EventArgs.Empty);
                Expired = true;
            }
        }

        public QueueTimer Timer { get; set; }

        public virtual void Update(float delta)
        {
        }

        public virtual void Render(SpriteBatch spriteBatch)
        {
        }
    }
}
