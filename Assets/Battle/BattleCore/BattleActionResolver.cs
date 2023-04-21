using BattleCore.ActionQueue;
using BattleCore.Actions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BattleCore
{
    public class BattleActionResolver
    {
        public delegate void OnBattleActionResolutionParams (BaseBattleAction selectedAction, QueueHandler actionQueue);
        public event OnBattleActionResolutionParams OnSwapActionResolution;
        public event OnBattleActionResolutionParams OnItemActionResolution;
        public event OnBattleActionResolutionParams OnAttackActionResolution;
        public event OnBattleActionResolutionParams OnDisengageActionResolution;

        public Queue<BaseBattleAction> ResolveActionsQueue { get; private set; }
        public QueueHandler ActionQueueHandler { get; private set; }

        public BattleActionResolver (Action onActionQueueResolved)
        {
            ActionQueueHandler = new QueueHandler(onActionQueueResolved);
        }

        public void SortActions (List<BattleParticipant> battleParticipantsCollection)
        {
            ResolveActionsQueue = new Queue<BaseBattleAction>();
            //TODO: resolve dots here

            List<BattleParticipant> playersOrderedByBattleActions = battleParticipantsCollection
                .OrderBy(n => ((int)n.SelectedBattleAction.PresentValue.ActionType))
                .ThenBy(n => n.CurrentEntity.PresentValue.ModifiedStats.Initiative.PresentValue)
                .ToList();

            foreach (BattleParticipant participant in playersOrderedByBattleActions)
            {
                ResolveActionsQueue.Enqueue(participant.SelectedBattleAction.PresentValue);
            }
        }

        public void ResolveAllActions ()
        {

            while (ResolveActionsQueue.Count > 0)
            {
                BaseBattleAction attackAction = ResolveActionsQueue.Dequeue();

                if (attackAction != null)
                {
                    ResolveBattleParticipantAction(attackAction);
                }
            }

            ActionQueueHandler.InvokeActions();

        }

        public void ResolveBattleParticipantAction (BaseBattleAction selectedAction)
        {
            switch (selectedAction.ActionType)
            {
                case BattleActionType.SWAP:
                    OnSwapActionResolution?.Invoke(selectedAction, ActionQueueHandler);
                    break;
                case BattleActionType.ITEM:
                    OnItemActionResolution?.Invoke(selectedAction, ActionQueueHandler);
                    break;
                case BattleActionType.ATTACK:
                    OnAttackActionResolution?.Invoke(selectedAction, ActionQueueHandler);
                    break;
                case BattleActionType.DISENGAGE:
                    OnDisengageActionResolution?.Invoke(selectedAction, ActionQueueHandler);
                    break;
                default:
                    break;
            }
        }
    }
}