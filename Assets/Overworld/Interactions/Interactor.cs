using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class Interactor : BaseInteractor
{
    [field: SerializeField]
    private Outline InteractorOutline { get; set; }

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
