using BattleCore;
using UnityEngine;

namespace StatusEffects
{
    [CreateAssetMenu(fileName = nameof(AshesToAshes), menuName = "ScriptableObjects/StatusEffects/" + nameof(AshesToAshes))]
    public class AshesToAshes : BaseScriptableStatusEffect
    {
        public override void ApplyStatus (BattleParticipant casterOwner, Entity caster, Entity target, Battle currentBattle)
        {
            throw new System.NotImplementedException();
        }
    }
}

