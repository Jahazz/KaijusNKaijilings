using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaInteractor : BaseInteractor
{
    private void OnTriggerEnter (Collider other)
    {
        if (other.gameObject == SingletonContainer.Instance.OverworldPlayerCharacterManager.OverworldCharacterMovement.gameObject)
        {
            InteractionEvent.Invoke();
        }
    }
}
