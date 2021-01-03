using Microsoft.Xna.Framework.Graphics;
using Shitemon.BattleSystem;
using System;

namespace Shitemon.Test
{
    class MoveQ : QueueObject
    {
        public Move Move { get; set; }
        public Mon Target { get; set; }
        public Mon User { get; set; }

        public MoveResult MoveResult { get; set; }


        public void Foo(object o, EventArgs e)
        {
        }

        bool invoked;

        public override void Update(float delta)
        {
            if(!invoked)
            {
                this.MoveResult = Move.moveDelegate.Invoke(new MoveArgs(Move, User, Target));
            }
        }

        public override void Render(SpriteBatch spriteBatch)
        {
        }
    }
}
