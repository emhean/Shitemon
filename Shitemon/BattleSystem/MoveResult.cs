namespace Shitemon.BattleSystem
{
    public class MoveResult
    {
        public MoveResult(bool missed, bool critical_hit, int outputDamage, int outputEffect)
        {
            this.Hit = missed;
            this.CriticalHit = critical_hit;
            this.OutputDamage = outputDamage;
            this.OutputEffect = outputEffect;
        }

        /// <summary>
        /// Whether it hit or missed.
        /// </summary>
        public bool Hit { get; set; }

        public bool CriticalHit { get; set; }
        public bool Lethal { get; set; }
        public int OutputDamage { get; set; }
        public int OutputEffect { get; set; }
    }
}
