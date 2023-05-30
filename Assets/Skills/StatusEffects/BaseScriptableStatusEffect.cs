using UnityEngine;

namespace StatusEffects
{
    public class BaseScriptableStatusEffect : ScriptableObject
    {
        [field: SerializeField]
        public string Name { get; private set; }
        [field: SerializeField]
        public string Description { get; private set; }
        [field: SerializeField]
        public Sprite Image { get; set; }
    }
}