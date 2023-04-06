using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;
using System.Linq;

public class Battle
{
    public delegate void OnEntityDeathParams (Entity entity, BattleParticipant owner);
    public event OnEntityDeathParams OnEntityDeath;
    public delegate void OnEntitySkillUsageParams (AttackBattleAction attackAction);
    public event OnEntitySkillUsageParams OnSkillUsage;
    public delegate void OnAllActionsSelectedParams ();
    public event OnAllActionsSelectedParams OnAllActionsSelected;

    public List<BattleParticipant> BattleParticipantsCollection { get; private set; } = new List<BattleParticipant>();
    public ObservableVariable<BattleState> CurrentBattleState { get; set; } = new ObservableVariable<BattleState>(BattleState.ACTION_CHOOSE);

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

        ChooseActions();
    }

    public void ExecuteAttackAction (AttackBattleAction attackAction)
    {
        attackAction.Skill.UseSkill(attackAction.Caster, attackAction.Target.CurrentEntity.PresentValue);
        OnSkillUsage?.Invoke(attackAction);
    }

    private void HandleSelectedCurrentAction (BaseBattleAction newValue)
    {
        if (CurrentBattleState.PresentValue == BattleState.ACTION_CHOOSE && newValue != null && AreAllActionsSelected() == true)
        {
            OnAllActionsSelected?.Invoke();
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
        BattleParticipant enemy = GetNPCBattleParticipant();
        enemy.QueueAttackAction(enemy.CurrentEntity.PresentValue, GetPlayerBattleParticipant(), enemy.CurrentEntity.PresentValue.SelectedSkillsCollection[0]);
    }

    private void WrapUp ()
    {
        CurrentBattleState.PresentValue = BattleState.WRAP_UP;
    }

    public BattleParticipant GetPlayerBattleParticipant ()
    {
        return GetBattleParticipant(false);
    }

    public BattleParticipant GetNPCBattleParticipant ()
    {
        return GetBattleParticipant(true);
    }

    public BattleParticipant GetOtherBattleParticipant(BattleParticipant otherBattleParticipant)
    {
        return BattleParticipantsCollection.Where(n => n != otherBattleParticipant).FirstOrDefault();
    }

    private BattleParticipant GetBattleParticipant (bool isNPC)
    {
        return BattleParticipantsCollection.Where(n => n.Player.IsNPC == isNPC).FirstOrDefault();
    }

    private void HandleCurrentEntityChanged (Entity AddedEntity, BattleParticipant entityOwner)
    {
        AddedEntity.IsAlive.OnVariableChange += (_) => OnEntityDeath.Invoke(AddedEntity, entityOwner);
    }
}
