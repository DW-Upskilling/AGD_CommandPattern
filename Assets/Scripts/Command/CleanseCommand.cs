using Command.Abstract;
using Command.Main;

namespace Command
{
    public class CleanseCommand : UnitCommand
    {
        private bool willHitTarget;

        public CleanseCommand(CommandData commandData)
        {
            this.commandData = commandData;
            willHitTarget = WillHitTarget();
        }

        public override bool WillHitTarget() => true;

        public override void Execute() => GameService.Instance.ActionService.GetActionByType(CommandType.Attack).PerformAction(actorUnit, targetUnit, willHitTarget);
    }
}