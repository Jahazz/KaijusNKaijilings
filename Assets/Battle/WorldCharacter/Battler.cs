using System.Collections.Generic;
using UnityEngine;
using UltEvents;

namespace BattleCore.OverworldCharacter
{
    public class Battler : MonoBehaviour
    {
        [field: SerializeField]
        private Player PlayerData { get; set; }
        [field: SerializeField]
        private List<EntityLevelPair> PlayerEntitiesCollection { get; set; }
        [field: SerializeField]
        private UltEvent OnBattleWon { get; set; }
        [field: SerializeField]
        private UltEvent OnBattleLost { get; set; }
        [field: SerializeField]
        private UltEvent OnBattleUnresolved { get; set; }

        public void InitialzieBattle ()
        {
            if (SingletonContainer.Instance.OverworldPlayerCharacterManager.CurrentPlayerState == PlayerState.IN_OVERWORLD)
            {
                PlayerData.EntitiesInEquipment = new System.Collections.ObjectModel.ObservableCollection<Entity>();

                foreach (EntityLevelPair pair in PlayerEntitiesCollection)
                {
                    PlayerData.EntitiesInEquipment.Add(SingletonContainer.Instance.EntityManager.RequestEntity(pair.Entity, pair.Level));
                }

                SingletonContainer.Instance.BattleScreenManager.Initialize(SingletonContainer.Instance.PlayerManager.CurrentPlayer, PlayerData, HandleOnBattleFinished);
            }
        }

        public void HandleOnBattleFinished (BattleResultType battleResult)
        {
            switch (battleResult)
            {
                case BattleResultType.NONE:
                    break;
                case BattleResultType.VICTORY:
                    OnBattleWon.Invoke();
                    break;
                case BattleResultType.DEFEAT:
                    OnBattleLost.Invoke();
                    break;
                case BattleResultType.UNRESOLVED:
                    OnBattleUnresolved.Invoke();
                    break;
                default:
                    break;
            }
        }
    }
}

