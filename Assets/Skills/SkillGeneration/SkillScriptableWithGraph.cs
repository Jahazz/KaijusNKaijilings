using BattleCore;
using Unity.VisualScripting;
using UnityEngine;

namespace Skills
{
    [CreateAssetMenu(fileName = nameof(SkillScriptableWithGraph), menuName = "ScriptableObjects/Skills/" + nameof(SkillScriptableWithGraph))]
    public class SkillScriptableWithGraph : SkillScriptableObject
    {
        [field: SerializeField]
        public ScriptGraphAsset SkillGraph { get; private set; }

        private ScriptMachine CurrentScriptMachine { get; set; }

        public override void UseSkill (BattleParticipant casterOwner, Entity caster, Entity target, Battle currentBattle)
        {
            base.UseSkill(casterOwner, caster, target, currentBattle);

            SkillInputWrapper wrapper = new SkillInputWrapper(casterOwner, caster, target, currentBattle, this);
            CurrentScriptMachine = SingletonContainer.Instance.BattleScreenManager.ScriptMachine;

            CurrentScriptMachine.nest.SwitchToMacro(SkillGraph);

            EventBus.Trigger(EventNames.SkillUsage, wrapper);
        }
    }
    public static class EventNames
    {
        public static string SkillUsage = "SkillUsage";
    }
}
