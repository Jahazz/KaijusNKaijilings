using BattleCore;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace StatusEffects.EntityStatusEffects
{
    public abstract class BaseScriptableEntityStatusEffect : BaseScriptableStatusEffect
    {
        [field: Space]
        [field: SerializeField]
        public StatusEffectType StatusEffectType { get; private set; }
        [field: SerializeField]
        public List<TypeDataScriptable> SkilType { get; private set; }

        [field: Space]
        [field: SerializeField]
        public bool Cleansable { get; private set; }
        [field: SerializeField]
        public bool RemovedAtEndOfCombat { get; private set; }
        [field: SerializeField]
        public bool RemovedOnDeath { get; private set; }
        [field: SerializeField]
        public int MaxStacks { get; private set; }
        [field: SerializeField]
        public string SkillGUID { get; set; } = Guid.NewGuid().ToString();

        public abstract void ApplyStatus (BattleParticipant casterOwner, Entity caster, Entity target, Battle currentBattle, int numberOfStacksToAdd);
    }
}

