namespace Shitemon.BattleSystem
{
    /// <summary>
    /// Event args for Moves.
    /// </summary>
    public class MoveArgs
    {
        public MoveArgs(Move moveUsed, Mon user, Mon target)
        {
            this.MoveUsed = moveUsed;
            this.User = user;
            this.Target = target;
        }

        public Move MoveUsed { get; }
        public Mon User { get; }
        public Mon Target { get; }
    }
}
