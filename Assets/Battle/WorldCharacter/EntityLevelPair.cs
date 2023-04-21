using System;
using UnityEngine;

namespace BattleCore.OverworldCharacter
{
    [Serializable]
    public class EntityLevelPair
    {
        [field: SerializeField]
        public StatsScriptable Entity { get; private set; }
        [field: SerializeField]
        public int Level { get; private set; }
    }
}