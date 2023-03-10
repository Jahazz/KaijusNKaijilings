using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class Interactor : MonoBehaviour
{
    [field: SerializeField]
    private Outline InteractorOutline { get; set; }
    [field: SerializeField]
    private InteractionEvent InteractionEvent { get; set; }

    public void SetInteractability (bool isInteractive)
    {
        InteractorOutline.enabled = isInteractive;
    }

    public virtual void Interact ()
    {
        InteractionEvent.Invoke();
        InteractorOutline.enabled = false;
    }
}

[Serializable]
public class InteractionEvent : UnityEvent { }
