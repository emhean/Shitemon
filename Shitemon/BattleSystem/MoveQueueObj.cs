using System;

namespace Shitemon.BattleSystem
{
    public class MoveQueueObj
    {
        // If animation is over then we invoke the move
        public BattleAnimation[] Animations { get; set; }
        public Move Move { get; set; }
        public Mon Target { get; set; }
        public Mon User { get; set; }
        public bool Expired { get; set; }
        public MoveResult MoveResult { get; set; }

        public event EventHandler<EventArgs> AnimStarted, AnimEnded;
        public event EventHandler<EventArgs> Removed;

        bool anim_started;
        public void OnAnimStarted()
        {
            if (!anim_started)
            {
                AnimStarted?.Invoke(this, EventArgs.Empty);
                anim_started = true;
            }
        }

        public void OnAnimEnded()
        {
            AnimEnded?.Invoke(this, EventArgs.Empty);
        }

        //public event EventHandler<EventArgs> Invoking, Invoked;
        //public void OnInvoking()
        //{
        //    Invoking?.Invoke(this, EventArgs.Empty);
        //}

        //public void OnInvoked()
        //{
        //    Invoked?.Invoke(this, EventArgs.Empty);
        //}

        bool removed;
        public void OnRemoved()
        {
            if (!removed)
            {
                Removed?.Invoke(this, EventArgs.Empty);
                removed = true;

                Expired = true;
            }

        }

    }
}
