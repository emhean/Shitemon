using Microsoft.Xna.Framework.Graphics;
using System;

namespace Shitemon.Test
{
    class QueueTest
    {
        QueueManager qm;

        void foo(object o, EventArgs e)
        {
        }

        public QueueTest()
        {
            qm = new QueueManager();

            var mq = new MoveQ();

            var qo = new AnimationQ(); //QueueObject();
            qo.Expiring += mq.Foo;



            qm.Queue(qo);
        }

        public virtual void Update(float delta)
        {
        }

        public virtual void Render(SpriteBatch spriteBatch)
        {
        }
    }
}
