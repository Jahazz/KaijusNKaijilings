using MVC;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BattleCore.ActionQueue;
using BattleCore.Actions;
using BattleCore.ScreenEntity;
using BattleCore.UI.SummaryScreen;

namespace BattleCore.UI
{
    public class BattleScreenModel : BaseModel<BattleScreenView>
    {
        [field: SerializeField]
        private BattleScreenSummaryController BattleScreenSummaryController { get; set; }
        [field: SerializeField]
        private CharacterMenuController CharacterMenuController { get; set; }
        private Battle CurrentBattle { get; set; }

        public void QueuePlayerSkillUsage (Entity caster, SkillScriptableObject skill)
        {
            CurrentBattle.GetPlayerBattleParticipant().QueueAttackAction(caster, CurrentBattle.GetNPCBattleParticipant(), skill);
        }

        public void ChangeEntity ()
        {
            CharacterMenuController.OpenMenuAsEntitySelection((entity) => CurrentBattle.GetPlayerBattleParticipant().QueueSwapAction(entity));
        }

        public bool IsInBattle ()
        {
            return CurrentBattle != null;
        }

        public void CleanupBattle ()
        {

            foreach (BattleParticipant participant in CurrentBattle.BattleParticipantsCollection)
            {
                StartCoroutine(CurrentView.TryToDestroyEntityOnScene(participant.CurrentEntity.PresentValue));
            }

            CurrentView.SetBottomUIBarInteractible(true);
            CurrentView.IsBottomBarShown(false);
            CurrentBattle = null;
        }

        protected override void AttachToEvents ()
        {
            BattleFactory.OnBattleCreation += HandleOnBattleCreated;
        }

        private void HandleOnBattleCreated (Battle createdBattle)
        {
            CurrentBattle = createdBattle;

            foreach (BattleParticipant battleParticipant in CurrentBattle.BattleParticipantsCollection)
            {
                StartCoroutine(ChangePlayerEntity(battleParticipant.Player, battleParticipant.CurrentEntity.PresentValue));
            }

            CurrentBattle.OnEntityDeath += CurrentBattle_OnEntityDeath;
            CurrentBattle.CurrentBattleState.OnVariableChange += HandleOnCurrentBattleStateChange;
            CurrentBattle.OnBattleFinished += HandleOnBattleFinished;
            CurrentBattle.OnPlayerEntitySwapRequest += HandleOnPlayerEntitySwapRequest;

            CurrentBattle.ViewEntitySwapAction = CurrentView.TryToDestroyEntityOnScene;
            CurrentBattle.ViewPlayAnimationAsEntity =(entity) => CurrentView.PlayAnimationAsEntity(entity,AnimationType.ATTACK);
            CurrentBattle.ViewWaitForAnimationFinished = CurrentView.WaitUntilAnimatorIdle;
            CurrentBattle.ViewSwapEntity = ChangePlayerEntity;


            CurrentView.IsBottomBarShown(true);
        }

        private void CurrentBattle_OnEntityDeath (Entity entity, BattleParticipant owner)
        {
            CurrentView.PlayAnimationAsEntity(entity, AnimationType.DIE);
        }

        private void HandleOnPlayerEntitySwapRequest (Action<Entity> callback)
        {
            CharacterMenuController.OpenMenuAsEntitySelection(callback);
        }

        private void HandleOnBattleFinished (BattleResultType battleResult)
        {
            BattleScreenSummaryController.OpenScreen(battleResult);
        }

        private void HandleOnCurrentBattleStateChange (BattleState newValue)
        {
            CurrentView.SetBottomUIBarInteractible(newValue == BattleState.ACTION_CHOOSE);
        }



        private IEnumerator ChangePlayerEntity (Player owner, Entity newValue)
        {
            IEnumerator output;

            if (owner.IsNPC == false)
            {
                output = CurrentView.SpawnEntityInPlayerPosition(newValue, true);
                SetupSkills(newValue);
            }
            else
            {
                output = CurrentView.SpawnEntityInSecondPosition(newValue, false);
            }

            return output;
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
}
