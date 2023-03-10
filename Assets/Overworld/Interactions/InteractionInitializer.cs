using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class InteractionInitializer : MonoBehaviour
{
    [field: SerializeField]
    private float InteractionRange { get; set; }

    private Interactor CurrentInteractor { get; set; }

    private const string INTERACTIBLE_TAG = "Interactible";

    public void HandleInteraction (CallbackContext callbackContext)
    {
        if (callbackContext.phase == UnityEngine.InputSystem.InputActionPhase.Performed)
        {
            if (CurrentInteractor != null)
            {
                CurrentInteractor.Interact();
            }
        }
    }

    protected virtual void Update()
    {
        SetCurrentInteractor(GetClosestInteractor());
    }

    private Interactor GetClosestInteractor ()
    {
        Vector3 center = transform.position;
        Collider[] colliders = Physics.OverlapSphere(transform.position, InteractionRange);
        Collider[] interactibleColliders = colliders.Where(collider => collider.CompareTag(INTERACTIBLE_TAG)).ToArray();
        Collider closestInteractibleCollider = interactibleColliders.OrderBy(collider => (center - collider.transform.position).sqrMagnitude).FirstOrDefault();

        return closestInteractibleCollider != null ? closestInteractibleCollider.GetComponent<Interactor>() : null;
    }

    private void SetCurrentInteractor (Interactor target)
    {
        if (target != CurrentInteractor)
        {
            SetInteractabilityIfAble(false);

            CurrentInteractor = target;

            SetInteractabilityIfAble(true);
        }
    }

    private void SetInteractabilityIfAble (bool interactabilityState)
    {
        if (CurrentInteractor != null)
        {
            CurrentInteractor.SetInteractability(interactabilityState);
        }
    }
}
