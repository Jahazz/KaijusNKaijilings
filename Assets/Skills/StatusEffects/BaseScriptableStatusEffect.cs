using System;
using UnityEngine;

namespace StatusEffects
{
    public class BaseScriptableStatusEffect : ScriptableObject, INameableGUIDableDescribable
    {
        [field: SerializeField]
        public string Name { get; set; }
        [field: SerializeField]
        public string Description { get; set; }
        [field: SerializeField]
        public Sprite Image { get; set; }
        [field: SerializeField]
        public string GUID { get; set; } = Guid.NewGuid().ToString();
    }
}