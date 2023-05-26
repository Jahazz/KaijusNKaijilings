using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [field: SerializeField]
    public Player CurrentPlayer { get; private set; } = new Player (false);

    protected virtual void Start ()
    {
        CurrentPlayer.EntitiesInEquipment.Add(SingletonContainer.Instance.EntityManager.RequestRandomEntity(7));
    }
}
