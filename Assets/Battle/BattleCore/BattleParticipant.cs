using BattleCore.Actions;
using System.Linq;
using Utils;

namespace BattleCore
{
    public class BattleParticipant
    {
        public Player Player { get; private set; }
        public ObservableVariable<Entity> CurrentEntity { get; set; } = new ObservableVariable<Entity>();
        public ObservableVariable<BaseBattleAction> SelectedBattleAction { get; private set; } = new ObservableVariable<BaseBattleAction>();

        public BattleParticipant (Player player)
        {
            Player = player;
        }

        public void QueueAttackAction (Entity caster, BattleParticipant skillTarget, SkillScriptableObject skill)
        {
            SelectedBattleAction.PresentValue = new AttackBattleAction(caster, skillTarget, skill);
        }

        public void QueueSwapAction (Entity entityToSwapTo)
        {
            SelectedBattleAction.PresentValue = new SwapBattleAction(this, entityToSwapTo);
        }

        public void SelectFirstAliveEntity ()
        {
            CurrentEntity.PresentValue = GetFirstAliveEntity();
        }

        public bool AreAllEntitiesOfParticipantDefeated ()
        {
            bool isEveryoneDead = true;

            foreach (Entity entity in Player.EntitiesInEquipment)
            {
                if (entity.IsAlive.PresentValue == true)
                {
                    isEveryoneDead = false;
                    break;
                }
            }

            return isEveryoneDead;
        }

        public Entity GetFirstAliveEntity ()
        {
            return Player.EntitiesInEquipment.First(entity => entity.IsAlive.PresentValue == true);
        }
    }
}