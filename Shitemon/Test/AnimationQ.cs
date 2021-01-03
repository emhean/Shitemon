using Shitemon.BattleSystem;
using System;

namespace Shitemon.Test
{
    class AnimationQ : QueueObject
    {
        public BattleAnimation Animation { get; set; }

        public event EventHandler<EventArgs> AnimStarted, AnimEnded;

        public void OnAnimStarted()
        {
            if (!Animation.Expired)
            {
                Animation.Start();
                AnimStarted?.Invoke(this, EventArgs.Empty);
            }
        }

        public void OnAnimEnded()
        {
            if (!Animation.Expired)
            {
                Animation.Stop();
                AnimEnded?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
