using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BaseInteractor : MonoBehaviour
{
    [field: SerializeField]
    protected InteractionEvent InteractionEvent { get; private set; }

}

[Serializable]
public class InteractionEvent : UnityEvent { }
