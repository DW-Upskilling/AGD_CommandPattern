using Command.Abstract;
using Command.Main;

namespace Command
{
    public class HealCommand : UnitCommand
    {
        private bool willHitTarget;

        public HealCommand(CommandData commandData)
        {
            this.commandData = commandData;
            willHitTarget = WillHitTarget();
        }

        public override bool WillHitTarget() => true;

        public override void Execute() => GameService.Instance.ActionService.GetActionByType(CommandType.Attack).PerformAction(actorUnit, targetUnit, willHitTarget);
    }
}