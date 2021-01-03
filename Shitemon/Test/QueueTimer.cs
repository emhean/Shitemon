namespace Shitemon.Test
{
    class QueueTimer
    {
        public float t;
        public float duration;
        public bool Expired => (t > duration);
    }
}
