using MVC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BattleCore.UI
{
    public class BattleScreenController : BaseController<BattleScreenModel, BattleScreenView>
    {
        public void InitializeBattle (Player firstPlayer, Player secondPlayer)
        {
            BattleFactory.StartNewBattle(firstPlayer, secondPlayer);
        }

        public void UseSkill (Entity caster, SkillScriptableObject skill)
        {
            CurrentModel.QueuePlayerSkillUsage(caster, skill);
        }

        public void DisplaySKillTooltip (Vector2 position, SkillScriptableObject mousedOverSkill)
        {

        }

        public void HideSkillTooltip ()
        {

        }

        public void ChangeEntity ()
        {
            CurrentModel.ChangeEntity();
        }

        public bool IsInBattle ()
        {
            return CurrentModel.IsInBattle();
        }

        public void CleanupBattle ()
        {
            CurrentModel.CleanupBattle();
        }
    }
}

