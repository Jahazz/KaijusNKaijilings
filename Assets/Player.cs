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
    [field: SerializeField]
    public string Name { get; private set; }

    private const string IS_TALKING_ANIMATOR_VARIABLE_NAME = "IsTalking";

    public Player(bool isNPC)
    {
        IsNPC = isNPC;
    }

    public void SetTalking (bool isTalking)
    {
        OverworldAnimator.SetBool(IS_TALKING_ANIMATOR_VARIABLE_NAME,isTalking);
    }
}
