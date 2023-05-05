using System.Collections.Generic;
using Utils;
using System.Linq;
using BattleCore.Actions;
using BattleCore.ActionQueue;
using System;
using System.Collections;
using BattleCore.ScreenEntity;
using UnityEngine;

namespace BattleCore
{
    public class Battle
    {
        public delegate void OnEntityDeathParams (Entity entity, BattleParticipant owner);
        public event OnEntityDeathParams OnEntityDeath;
        public delegate void OnBattleFinishedParams (BattleResultType battleResult);
        public event OnBattleFinishedParams OnBattleFinished;
        public delegate void OnPlayerEntitySwapRequestParams (Action<Entity> callback);
        public event OnPlayerEntitySwapRequestParams OnPlayerEntitySwapRequest;

        //  Events for status effects
        //      start of turn
        //      before creature attacks
        //      after creature attacks
        //      end of turn
        public delegate IEnumerator OnTurnEndParams ();
        public event OnTurnEndParams OnTurnEnd;

        public Func<Entity, IEnumerator> ViewEntitySwapAction { get; set; }//shopuld be more generalised and not bound with view, but for now it will suffice? idk
        public Func<Entity, IEnumerator> ViewPlayAnimationAsEntity { get; set; }
        public Func<Entity, IEnumerator> ViewWaitForAnimationFinished { get; set; }
        public Func<Player, Entity, IEnumerator> ViewSwapEntity { get; set; }

        public List<BattleParticipant> BattleParticipantsCollection { get; private set; } = new List<BattleParticipant>();
        public ObservableVariable<BattleState> CurrentBattleState { get; private set; } = new ObservableVariable<BattleState>(BattleState.NONE);
        private BattleActionResolver BattleActionResolver { get; set; }

        public Battle (List<Player> participantsCollection)
        {
            foreach (Player playerParticipant in participantsCollection)
            {
                BattleParticipant participant = new BattleParticipant(playerParticipant);
                BattleParticipantsCollection.Add(participant);

                participant.CurrentEntity.OnVariableChange += (entity) => HandleCurrentEntityChanged(entity, participant);
                participant.SelectedBattleAction.OnVariableChange += HandleSelectedCurrentAction;

                participant.SelectFirstAliveEntity();
            }

            SetBattleState(BattleState.ACTION_CHOOSE);
        }

        public BattleParticipant GetPlayerBattleParticipant ()
        {
            return GetBattleParticipant(false);
        }

        public BattleParticipant GetNPCBattleParticipant ()
        {
            return GetBattleParticipant(true);
        }

        public BattleParticipant GetOtherBattleParticipant (BattleParticipant otherBattleParticipant)
        {
            return BattleParticipantsCollection.Where(n => n != otherBattleParticipant).FirstOrDefault();
        }

        public bool CheckIsBattleOver (out BattleResultType battleResult)
        {
            bool output = false;

            if (GetPlayerBattleParticipant().AreAllEntitiesOfParticipantDefeated() == true)
            {
                battleResult = BattleResultType.DEFEAT;
                output = true;
            }
            else if (GetNPCBattleParticipant().AreAllEntitiesOfParticipantDefeated() == true)
            {
                battleResult = BattleResultType.VICTORY;
                output = true;
            }
            else
            {
                battleResult = BattleResultType.NONE;
            }

            return output;
        }

        private void HandleSelectedCurrentAction (BaseBattleAction newValue)
        {
            if (CurrentBattleState.PresentValue == BattleState.ACTION_CHOOSE && newValue != null && AreAllActionsSelected() == true)
            {
                AllActionsSelected();
            }
        }

        private bool AreAllActionsSelected ()
        {
            bool output = true;

            foreach (BattleParticipant participant in BattleParticipantsCollection)
            {
                if (participant.SelectedBattleAction.PresentValue == null)
                {
                    output = false;
                    break;
                }
            }

            return output;
        }

        private BattleParticipant GetBattleParticipant (bool isNPC)
        {
            return BattleParticipantsCollection.Where(n => n.Player.IsNPC == isNPC).FirstOrDefault();
        }

        private void HandleCurrentEntityChanged (Entity AddedEntity, BattleParticipant entityOwner)
        {
            AddedEntity.IsAlive.OnVariableChange += (_) => HandleEntityDeath(AddedEntity, entityOwner);
        }

        private void HandleEntityDeath (Entity deadEntity, BattleParticipant entityOwner)
        {
            OnEntityDeath.Invoke(deadEntity, entityOwner);

            if (BattleActionResolver != null)
            {
                BattleActionResolver.ActionQueueHandler.RemoveActionsWithPredicate(AttackActionRemovalPredicate);

                bool AttackActionRemovalPredicate (QueuedAction target)
                {
                    bool output = false;

                    if (target.ActionIsBasedOn.ActionType == BattleActionType.ATTACK)
                    {
                        AttackBattleAction attackAction = target.ActionIsBasedOn as AttackBattleAction;

                        output = attackAction.Caster == deadEntity;
                    }

                    return output;
                }

            }
        }

        private void SetBattleState (BattleState newValue)
        {
            CurrentBattleState.PresentValue = newValue;

            switch (newValue)
            {
                case BattleState.ACTION_CHOOSE:
                    ActionChoose();
                    break;
                case BattleState.ACTION_RESOLVE:
                    ActionResolve();
                    break;
                case BattleState.WRAP_UP:
                    SingletonContainer.Instance.StartCoroutine(WaitUntilIEnumeratorEventIsFullyResolved(OnTurnEnd, WrapUp));
                    break;
                default:
                    break;
            }
        }

        private void ActionChoose ()
        {
            // ENEMY ACTION FAKE CHOOSE
            BattleParticipant enemy = GetNPCBattleParticipant();
            enemy.QueueAttackAction(enemy.CurrentEntity.PresentValue, GetPlayerBattleParticipant(), enemy.CurrentEntity.PresentValue.SelectedSkillsCollection[0]);
        }

        private void ActionResolve ()
        {
            InitializeBattleResolver();
            BattleActionResolver.SortActions(BattleParticipantsCollection);
            BattleActionResolver.ResolveAllActions();
        }

        private void WrapUp ()
        {


            if (CheckIsBattleOver(out BattleResultType battleResult) == true)
            {
                OnBattleFinished?.Invoke(battleResult);
            }
            else if (GetPlayerBattleParticipant().CurrentEntity.PresentValue.IsAlive.PresentValue == false)
            {
                HandlePlayerEntitySwap();
            }
            else if (GetNPCBattleParticipant().CurrentEntity.PresentValue.IsAlive.PresentValue == false)
            {
                HandleEnemyEntitySwapIfNeeded();
            }
            else
            {
                FinishWrapUp();
            }
        }

        private void InitializeBattleResolver ()
        {
            BattleActionResolver = new BattleActionResolver(HandleOnAllActionsResolved);

            BattleActionResolver.OnAttackActionResolution += AddAttackActionToQueue;
            BattleActionResolver.OnDisengageActionResolution += AddDisengageActionToQueue;
            BattleActionResolver.OnItemActionResolution += AddItemActionToQueue;
            BattleActionResolver.OnSwapActionResolution += AddSwapActionToQueue;
        }

        private void AllActionsSelected ()
        {
            SetBattleState(BattleState.ACTION_RESOLVE);
        }

        private void HandleOnAllActionsResolved ()
        {
            DisposeOfBattleResolver();
            SetBattleState(BattleState.WRAP_UP);
        }

        private void FinishWrapUp ()
        {
            ClearChosenBattleActions();
            SetBattleState(BattleState.ACTION_CHOOSE);
        }

        private void ClearChosenBattleActions ()
        {
            foreach (BattleParticipant participant in BattleParticipantsCollection)
            {
                participant.SelectedBattleAction.PresentValue = null;
            }
        }

        private void DisposeOfBattleResolver ()
        {
            BattleActionResolver.OnAttackActionResolution -= AddAttackActionToQueue;
            BattleActionResolver.OnDisengageActionResolution -= AddDisengageActionToQueue;
            BattleActionResolver.OnItemActionResolution -= AddItemActionToQueue;
            BattleActionResolver.OnSwapActionResolution -= AddSwapActionToQueue;

            BattleActionResolver = null;
        }

        public void RequestEntitySwap (BattleParticipant participant, Entity entityToSwap, Action finishCallback)
        {
            QueueHandler swapQueue = new QueueHandler(finishCallback);
            AddSwapActionToQueue(new SwapBattleAction(participant, entityToSwap), swapQueue);
            swapQueue.InvokeActions();
        }

        private void HandleEnemyEntitySwapIfNeeded ()
        {
            BattleParticipant npc = GetNPCBattleParticipant();

            if (npc.CurrentEntity.PresentValue.IsAlive.PresentValue == false)
            {
                RequestEntitySwap(npc, npc.GetFirstAliveEntity(), FinishWrapUp);
            }
            else
            {
                FinishWrapUp();
            }
        }

        private void HandlePlayerEntitySwap ()
        {
            BattleParticipant player = GetPlayerBattleParticipant();

            OnPlayerEntitySwapRequest.Invoke(SwapPlayerEntity);

            void SwapPlayerEntity (Entity entityToSwapTo)
            {
                RequestEntitySwap(player, entityToSwapTo, HandleEnemyEntitySwapIfNeeded);
            }
        }

        private void AddSwapActionToQueue (BaseBattleAction battleAction, QueueHandler actionQueue)
        {
            SwapBattleAction swapAction = battleAction as SwapBattleAction;

            actionQueue.EnqueueFunction(battleAction, () => ViewEntitySwapAction(swapAction.ActionOwner.CurrentEntity.PresentValue));
            actionQueue.EnqueueFunction(battleAction, () => SwapEntityVariable(swapAction.ActionOwner, swapAction.EntityToSwapTo));
            actionQueue.EnqueueFunction(battleAction, () => ViewSwapEntity(swapAction.ActionOwner.Player, swapAction.EntityToSwapTo));
        }

        private void AddAttackActionToQueue (BaseBattleAction battleAction, QueueHandler actionQueue)
        {
            AttackBattleAction attackAction = battleAction as AttackBattleAction;

            actionQueue.EnqueueFunction(battleAction, () => ViewPlayAnimationAsEntity(attackAction.Caster));
            actionQueue.EnqueueFunction(battleAction, () => ExecuteAttackActionDelegate(attackAction));
            actionQueue.EnqueueFunction(battleAction, () => ViewWaitForAnimationFinished(attackAction.Caster));
        }

        private void AddDisengageActionToQueue (BaseBattleAction battleAction, QueueHandler actionQueue)
        {
            DisengageBattleAction disengageAction = battleAction as DisengageBattleAction;
        }

        private void AddItemActionToQueue (BaseBattleAction battleAction, QueueHandler actionQueue)
        {
            UseItemBattleAction useItemBattleAction = battleAction as UseItemBattleAction;
        }


        private IEnumerator SwapEntityVariable (BattleParticipant participant, Entity entityToSwapTo)
        {
            participant.CurrentEntity.PresentValue = entityToSwapTo;
            yield return null;
        }

        private IEnumerator ExecuteAttackActionDelegate (AttackBattleAction selectedAction)
        {
            selectedAction.Skill.UseSkill(selectedAction.CasterOwner, selectedAction.Caster, selectedAction.Target.CurrentEntity.PresentValue, this);
            yield return null;
        }

        //public delegate IEnumerator OnTurnEndParams ();
        //public event OnTurnEndParams OnTurnEnd;

        private IEnumerator WaitUntilIEnumeratorEventIsFullyResolved<T> (T eventToWaitFor, Action onAllEventsCompletedCallback, params object[] paramList) where T : Delegate
        {
            if (OnTurnEnd != null)
            {
                Delegate[] delegatesToInvokeCollection = eventToWaitFor.GetInvocationList();

                foreach (Delegate singleDelagate in delegatesToInvokeCollection)
                {
                    yield return (IEnumerator)singleDelagate.DynamicInvoke(paramList);
                }
            }

            onAllEventsCompletedCallback.Invoke();
        }
    }
}
