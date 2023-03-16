using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battler : MonoBehaviour
{
    [field: SerializeField]
    private Player PlayerData { get; set; }
    [field: SerializeField]
    private List<EntityLevelPair> PlayerEntitiesCollection { get; set; }

    public void InitialzieBattle ()
    {
        PlayerData.EntitiesInEquipment = new System.Collections.ObjectModel.ObservableCollection<Entity>();

        foreach (EntityLevelPair pair in PlayerEntitiesCollection)
        {
            PlayerData.EntitiesInEquipment.Add(SingletonContainer.Instance.EntityManager.RequestEntity(pair.Entity,pair.Level));
        }

        SingletonContainer.Instance.BattleScreenManager.Initialize(SingletonContainer.Instance.PlayerManager.CurrentPlayer, PlayerData);
    }
}
