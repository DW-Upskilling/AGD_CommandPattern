using Command.Player;

namespace Command.AbstractCommands
{
    public abstract class UnitCommand : ICommand
    {
        // Fields to store information related to the command.
        public int ActorUnitID;
        public int TargetUnitID;
        public int ActorPlayerID;
        public int TargetPlayerID;

        // References to the actor and target units, accessible by subclasses.
        protected UnitController actorUnit;
        protected UnitController targetUnit;

        /// <summary>
        /// Abstract method to execute the unit command. Must be implemented by concrete subclasses.
        /// </summary>
        public abstract void Execute();

        /// <summary>
        /// Abstract method to determine whether the command will successfully hit its target.
        /// Must be implemented by concrete subclasses.
        /// </summary>
        public abstract bool WillHitTarget();
    }
}