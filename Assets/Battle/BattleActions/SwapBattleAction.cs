namespace BattleCore.Actions
{
    public class SwapBattleAction : BaseBattleAction
    {
        public Entity EntityToSwapTo { get; set; }
        public BattleParticipant ActionOwner { get; set; }

        public SwapBattleAction (BattleParticipant actionOwner, Entity entityToSwapTo)
        {
            ActionType = BattleActionType.SWAP;
            ActionOwner = actionOwner;
            EntityToSwapTo = entityToSwapTo;
        }
    }
}

