using BattleCore;
using System;
using System.Collections.Generic;
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
        [field: SerializeField]
        public string SkillGUID { get; set; } = Guid.NewGuid().ToString();

        public abstract void ApplyStatus (Battle currentBattle);
    }
}

