using StatusEffects.EntityStatusEffects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StatusEffects
{
    public class BaseStatusEffect<StatusEffectType>
    {
        public delegate void OnStatusEffectRemovedParams ();
        public event OnStatusEffectRemovedParams OnStatusEffectRemoved;
        public StatusEffectType BaseData { get; private set; }

        public void InvokeOnRemoved ()
        {
            OnStatusEffectRemoved.Invoke();
        }

        public BaseStatusEffect (StatusEffectType effectData)
        {
            BaseData = effectData;
        }
    }
}

