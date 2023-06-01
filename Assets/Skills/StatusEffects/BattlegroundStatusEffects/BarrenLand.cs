using BattleCore;
using UnityEngine;

namespace StatusEffects.BattlegroundStatusEffects
{
    [CreateAssetMenu(fileName = nameof(BarrenLand), menuName = "ScriptableObjects/StatusEffects/" + nameof(BarrenLand))]
    public class BarrenLand : BaseScriptableBattlegroundStatusEffect
    {
        public override void ApplyStatus (Battle currentBattle)
        {

        }
    }
}

