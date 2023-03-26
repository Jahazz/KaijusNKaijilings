using MVC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleScreenModel : BaseModel<BattleScreenView>
{
    private Battle CurrentBattle { get; set; }

    public void QueuePlayerSkillUsage (Entity caster, SkillScriptableObject skill)
    {
        CurrentBattle.GetPlayerBattleParticipant().QueueAttackAction(caster, CurrentBattle.GetOtherBattleParticipant().CurrentEntity.PresentValue, skill);
    }

    protected override void AttachToEvents ()
    {
        BattleFactory.OnBattleCreation += HandleOnBattleCreated;
    }

    private void HandleOnBattleCreated (Battle createdBattle)
    {
        CurrentBattle = createdBattle;

        foreach (BattleParticipant battleParticipant in createdBattle.BattleParticipantsCollection)
        {
            battleParticipant.CurrentEntity.OnVariableChange += (Entity entity) => OnPlayerEntityChanged(battleParticipant.Player, entity);
            OnPlayerEntityChanged(battleParticipant.Player, battleParticipant.CurrentEntity.PresentValue);
        }

        createdBattle.OnEntityDeath += HandleOnEntityDeath;
        createdBattle.OnSkillUsage += HandleOnEntitySkillUsage;
    }

    private void HandleOnEntitySkillUsage (AttackBattleAction attackAction)
    {
        CurrentView.PlayAnimationAsEntity(attackAction.Caster, AnimationType.ATTACK);
        CurrentView.PlayAnimationAsEntity(attackAction.Target, AnimationType.GET_HIT);
    }

    private void HandleOnEntityDeath (Entity entity)
    {
        CurrentView.PlayAnimationAsEntity(entity, AnimationType.DIE);
    }

    private void OnPlayerEntityChanged (Player owner, Entity newValue)
    {
        if (owner.IsNPC == false)
        {
            CurrentView.SpawnEntityInPlayerPosition(newValue, true);
            SetupSkills(newValue);
        }
        else
        {
            CurrentView.SpawnEntityInSecondPosition(newValue, false);
        }
    }

    private void SetupSkills (Entity entity)//TODO: add item skill
    {
        CurrentView.BindSkillToButtons(new List<SkillScriptableObject>(entity.SelectedSkillsCollection), entity);
    }

    protected override void DetachFromEvents ()
    {
        base.DetachFromEvents();
    }

}
