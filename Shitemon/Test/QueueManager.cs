using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Shitemon.Test
{
    class QueueManager
    {
        List<QueueObject> q_list;

        public QueueManager()
        {
            q_list = new List<QueueObject>();
        }

        public void Queue(QueueObject queueObject)
        {
            q_list.Add(queueObject);
        }

        void ExpireFirstInQueue()
        {
            q_list[0].OnExpiring();
            q_list.RemoveAt(0);
        }

        public void Update(float delta)
        {
            if(q_list.Count > 0)
            {
                var q = q_list[0];

                if (q.Timer != null)
                {
                    if(q.Timer.Expired)
                    {
                        if (q.Expired)
                        {
                            ExpireFirstInQueue();
                            // then if statements breaks
                        }
                        else
                            q.Update(delta);
                    }
                    else
                        q.Timer.t += delta;
                }
            }
        }

        public void Render(SpriteBatch spriteBatch)
        {
            if (q_list.Count > 0)
            {
                var q = q_list[0];

                if (q.Timer != null)
                {
                    if(q.Timer.Expired)
                        q.Render(spriteBatch);
                }
            }
        }
    }
}
