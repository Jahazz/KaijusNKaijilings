using BattleCore;
using System.Collections.Generic;
using Tooltips;
using UnityEngine;

namespace StatusEffects.BattlegroundStatusEffects
{
    public abstract class BaseScriptableBattlegroundStatusEffect : BaseScriptableStatusEffect
    {
        [field: SerializeField]
        public List<TypeDataScriptable> StatusEffectType { get; private set; }
        [field: SerializeField]
        public bool Cleansable { get; private set; }
        [field: SerializeField]
        public bool RemovedAtEndOfCombat { get; private set; }

        public abstract void ApplyStatus (Battle currentBattle);

        protected virtual void OnValidate ()
        {
            TooltipType = TooltipType.BATTLEGROUND_STATUS_EFFECT;
        }
    }
}

