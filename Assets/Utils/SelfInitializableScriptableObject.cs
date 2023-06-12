using System;
using UnityEngine;

#if UNITY_EDITOR
namespace Utils
{
    public class SelfInitializableScriptableObject<ComponentType> : ScriptableObject where ComponentType : struct, IConvertible
    {
        [field: SerializeField]
        public string Name { get; private set; }
        [field: SerializeField]
        public ComponentType Type { get; private set; }
        [field: SerializeField]
        public bool IsSelfInitializationEnabled { get; set; } = true;

        private const string SCRIPTABLE_OBJECT_TYPE_NAME = "ScriptableObject";

        protected virtual void OnValidate ()
        {
            if (IsSelfInitializationEnabled == true)
            {
                Name = name;
                ComponentType type; 

                if(Enum.TryParse(Utils.TryGetTypeByDirectory(SCRIPTABLE_OBJECT_TYPE_NAME, Name), out type))
                {
                    Type = type;
                }
            }
        }
    }
}
#endif