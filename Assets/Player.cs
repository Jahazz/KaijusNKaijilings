using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

[Serializable]
public class Player
{
    [field: SerializeField]
    public ObservableCollection<Entity> EntitiesInEquipment { get; set; } = new ObservableCollection<Entity>();
}
