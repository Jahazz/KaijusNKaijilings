using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public Player CurrentPlayer { get; private set; } = new Player ();

    protected virtual void Start ()
    {
        CurrentPlayer.EntitiesInEquipment.Add(SingletonContainer.Instance.EntityManager.RequestRandomEntity(7));
        CurrentPlayer.EntitiesInEquipment.Add(SingletonContainer.Instance.EntityManager.RequestRandomEntity(1));
        CurrentPlayer.EntitiesInEquipment.Add(SingletonContainer.Instance.EntityManager.RequestRandomEntity(3));
    }
}
