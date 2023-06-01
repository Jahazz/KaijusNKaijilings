using BattleCore;

namespace StatusEffects.BattlegroundStatusEffects
{
    public abstract class BaseScriptableBattlegroundStatusEffect : BaseScriptableStatusEffect
    {
        public bool Cleansable { get; private set; }
        public bool RemovedAtEndOfCombat { get; private set; }
        public abstract void ApplyStatus (Battle currentBattle);
    }
}

