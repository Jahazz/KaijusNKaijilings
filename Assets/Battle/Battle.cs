using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;
using System.Linq;

public class Battle
{
    public delegate void OnEntityDeathParams (Entity entity);
    public event OnEntityDeathParams OnEntityDeath;
    public delegate void OnEntitySkillUsageParams (AttackBattleAction attackAction);
    public event OnEntitySkillUsageParams OnSkillUsage;

    public List<BattleParticipant> BattleParticipantsCollection { get; private set; } = new List<BattleParticipant>();

    public ObservableVariable<BattleState> CurrentBattleState { get; set; } = new ObservableVariable<BattleState>(BattleState.ACTION_CHOOSE);

    public Queue<BaseBattleAction> ResolveActionsQueue { get; private set; }

    public Battle (List<Player> participantsCollection)
    {
        foreach (Player playerParticipant in participantsCollection)
        {
            BattleParticipant participant = new BattleParticipant(playerParticipant);
            BattleParticipantsCollection.Add(participant);

            participant.CurrentEntity.OnVariableChange += HandleCurrentEntityChanged;
            participant.SelectedBattleAction.OnVariableChange += HandleSelectedCurrentAction;

            participant.SelectFirstAliveEntity();
        }

        ChooseActions();
    }

    public void ExecuteAttackAction (AttackBattleAction attackAction)
    {
        attackAction.Skill.UseSkill(attackAction.Caster, attackAction.Target);
        OnSkillUsage?.Invoke(attackAction);
    }

    //public BaseBattleAction ResolveNextAction ()
    //{
    //    BaseBattleAction actionToResolve = null;

    //    if (ResolveActionsQueue.Count>0)
    //    {
    //        actionToResolve = ResolveActionsQueue.Dequeue();
    //        ExecuteBattleParticipantAction(actionToResolve);
    //    }
    //    else
    //    {
    //        WrapUp();
    //    }

    //    return actionToResolve;
    //}

    private void HandleSelectedCurrentAction (BaseBattleAction newValue)
    {
        if (CurrentBattleState.PresentValue == BattleState.ACTION_CHOOSE && newValue != null && AreAllActionsSelected() == true)
        {
            SortActions();
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

    private void ChooseActions ()
    {
        CurrentBattleState.PresentValue = BattleState.ACTION_CHOOSE;

        // ENEMY ACTION FAKE CHOOSE
        BattleParticipant enemy = GetOtherBattleParticipant();
        enemy.QueueAttackAction(enemy.CurrentEntity.PresentValue, GetPlayerBattleParticipant().CurrentEntity.PresentValue, enemy.CurrentEntity.PresentValue.SelectedSkillsCollection[0]);
    }

    private void SortActions ()
    {
        ResolveActionsQueue = new Queue<BaseBattleAction>();
        //TODO: resolve dots here

        List<BattleParticipant> playersOrderedByBattleActions = BattleParticipantsCollection
            .OrderBy(n => ((int)n.SelectedBattleAction.PresentValue.ActionType))
            .ThenBy(n => n.CurrentEntity.PresentValue.ModifiedStats.Initiative.PresentValue)
            .ToList();

        foreach (BattleParticipant participant in playersOrderedByBattleActions)
        {
            ResolveActionsQueue.Enqueue(participant.SelectedBattleAction.PresentValue);
        }

        CurrentBattleState.PresentValue = BattleState.ACTION_RESOLVE;
    }

    private void WrapUp ()
    {
        CurrentBattleState.PresentValue = BattleState.WRAP_UP;
    }

    public BattleParticipant GetPlayerBattleParticipant ()
    {
        return GetBattleParticipant(false);
    }

    public BattleParticipant GetOtherBattleParticipant ()
    {
        return GetBattleParticipant(true);
    }

    private BattleParticipant GetBattleParticipant (bool isNPC)
    {
        return BattleParticipantsCollection.Where(n => n.Player.IsNPC == isNPC).FirstOrDefault();
    }

    private void HandleCurrentEntityChanged (Entity AddedEntity)
    {
        AddedEntity.IsAlive.OnVariableChange += (_) => OnEntityDeath.Invoke(AddedEntity);
    }
}
