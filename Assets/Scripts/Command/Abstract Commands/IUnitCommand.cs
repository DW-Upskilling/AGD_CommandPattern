using Command.Player;

namespace Command.Abstract
{
    public abstract class UnitCommand : ICommand
    {
        public CommandData commandData;

        protected UnitController actorUnit;
        protected UnitController targetUnit;

        public abstract void Execute();

        public abstract bool WillHitTarget();

        public void SetActorUnit(UnitController actorUnit) => this.actorUnit = actorUnit;

        public void SetTargetUnit(UnitController targetUnit) => this.targetUnit = targetUnit;
    }

    public struct CommandData
    {
        public int ActorUnitID;
        public int TargetUnitID;
        public int ActorPlayerID;
        public int TargetPlayerID;

        public CommandData(int ActorUnitID, int TargetUnitID, int ActorPlayerID, int TargetPlayerID)
        {
            this.ActorUnitID = ActorUnitID;
            this.TargetUnitID = TargetUnitID;
            this.ActorPlayerID = ActorPlayerID;
            this.TargetPlayerID = TargetPlayerID;
        }
    }
}