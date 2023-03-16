using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

[Serializable]
public class Player
{
    [field: SerializeField]
    public Animator OverworldAnimator { get; set; }
    [field: SerializeField]
    public ObservableCollection<Entity> EntitiesInEquipment { get; set; } = new ObservableCollection<Entity>();
    [field: SerializeField]
    public bool IsNPC { get; private set; } = true;

    public Player(bool isNPC)
    {
        IsNPC = isNPC;
    }
}
