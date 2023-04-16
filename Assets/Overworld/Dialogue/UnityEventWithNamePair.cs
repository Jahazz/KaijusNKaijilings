using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class UnityEventWithNamePair
{
    [field: SerializeField]
    public string EventName { get; private set; }
    [field: SerializeField]
    public UnityEvent AssignedEvent { get; private set; }
}
