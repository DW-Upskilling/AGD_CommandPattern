using Command.Main;
using Command.Player;
using Command.Actions;
using Command.Abstract;

namespace Command.Input
{
    public class InputService
    {
        private MouseInputHandler mouseInputHandler;

        private InputState currentState;
        private CommandType selectedCommandType;
        private TargetType targetType;

        public InputService()
        {
            mouseInputHandler = new MouseInputHandler(this);
            SetInputState(InputState.INACTIVE);
            SubscribeToEvents();
        }

        public void SetInputState(InputState inputStateToSet) => currentState = inputStateToSet;

        private void SubscribeToEvents() => GameService.Instance.EventService.OnActionSelected.AddListener(OnActionSelected);

        public void UpdateInputService()
        {
            if(currentState == InputState.SELECTING_TARGET)
                mouseInputHandler.HandleTargetSelection(targetType);
        }

        public void OnActionSelected(CommandType selectedActionType)
        {
            this.selectedCommandType = selectedActionType;
            SetInputState(InputState.SELECTING_TARGET);
            TargetType targetType = SetTargetType(selectedActionType);
            ShowTargetSelectionUI(targetType);
        }

        private void ShowTargetSelectionUI(TargetType selectedTargetType)
        {
            int playerID = GameService.Instance.PlayerService.ActivePlayerID;
            GameService.Instance.UIService.ShowTargetOverlay(playerID, selectedTargetType);
        }

        private TargetType SetTargetType(CommandType selectedActionType) => targetType = GameService.Instance.ActionService.GetTargetTypeForAction(selectedActionType);

        public void OnTargetSelected(UnitController targetUnit)
        {
            SetInputState(InputState.EXECUTING_INPUT);

            UnitCommand commandToProcess = CreateUnitCommand(targetUnit);

            GameService.Instance.ProcessUnitCommand(commandToProcess);
        }

        private CommandData CreateCommandData(UnitController unitController)
        {
            return new CommandData(
                GameService.Instance.PlayerService.ActiveUnitID,
                unitController.UnitID,
                GameService.Instance.PlayerService.ActivePlayerID,
                unitController.Owner.PlayerID
            );
        }

        private UnitCommand CreateUnitCommand(UnitController unitController) {
            CommandData commandData = CreateCommandData(unitController);

            return this.selectedCommandType switch { 
                CommandType.Attack => new AttackCommand(commandData), 
                CommandType.Heal => new HealCommand(commandData),
                CommandType.AttackStance => new AttackStanceCommand(commandData),
                CommandType.Cleanse => new CleanseCommand(commandData),
                CommandType.BerserkAttack => new BerserkAttackCommand(commandData),
                CommandType.Meditate => new MeditateCommand(commandData),
                CommandType.ThirdEye => new ThirdEyeCommand(commandData),
                _ => throw new System.Exception($"No Command found of type: {this.selectedCommandType}")
            };
        }
    }
}