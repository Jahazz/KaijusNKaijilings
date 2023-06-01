using BattleCore;
using UnityEngine;

namespace StatusEffects.BattlegroundStatusEffects
{
    [CreateAssetMenu(fileName = nameof(Storm), menuName = "ScriptableObjects/StatusEffects/" + nameof(Storm))]
    public class Storm : BaseScriptableBattlegroundStatusEffect
    {
        public override void ApplyStatus (Battle currentBattle)
        {

        }
    }
}

