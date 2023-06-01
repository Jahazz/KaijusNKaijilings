using BattleCore;
using UnityEngine;

namespace StatusEffects.BattlegroundStatusEffects
{
    [CreateAssetMenu(fileName = nameof(Blizzard), menuName = "ScriptableObjects/StatusEffects/" + nameof(Blizzard))]
    public class Blizzard : BaseScriptableBattlegroundStatusEffect
    {
        public override void ApplyStatus (Battle currentBattle)
        {

        }
    }
}

