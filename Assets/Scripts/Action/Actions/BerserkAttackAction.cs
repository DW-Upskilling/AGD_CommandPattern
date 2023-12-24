using Command.Commands;
using Command.Input;
using Command.Main;
using Command.Player;
using UnityEngine;

namespace Command.Actions
{
    public class BerserkAttackAction : IAction
    {
        private UnitController actorUnit;
        private UnitController targetUnit;
        private bool isSuccessful;

        public TargetType TargetType => TargetType.Enemy;

        public void PerformAction(UnitController actorUnit, UnitController targetUnit, bool isSuccessful)
        {
            this.actorUnit = actorUnit;
            this.targetUnit = targetUnit;
            this.isSuccessful = isSuccessful;

            actorUnit.PlayBattleAnimation(CommandType.BerserkAttack, CalculateMovePosition(targetUnit), OnActionAnimationCompleted);
        }

        public void OnActionAnimationCompleted()
        {
            GameService.Instance.SoundService.PlaySoundEffects(Sound.SoundType.BERSERK_ATTACK);

            if (isSuccessful)
                targetUnit.TakeDamage(actorUnit.CurrentPower * 2);
            else
            {
                actorUnit.TakeDamage(actorUnit.CurrentPower * 2);
                actorUnit.OnActionExecuted();
            }
        }

        public Vector3 CalculateMovePosition(UnitController targetUnit) => targetUnit.GetEnemyPosition();
    }
}