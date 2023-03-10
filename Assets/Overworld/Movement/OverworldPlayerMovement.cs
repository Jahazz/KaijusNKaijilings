using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Utils;
using static UnityEngine.InputSystem.InputAction;

public class OverworldPlayerMovement : MonoBehaviour
{
    [field: SerializeField]
    private Rigidbody CharacterRigidbody { get; set; }
    [field: SerializeField]
    private Transform MovementVector { get; set; }
    [field: SerializeField]
    private float MovementSpeedFactor { get; set; }
    private Vector3 CachedMovementVector { get; set; } = Vector3.zero;

    public void HandleMovementActionPerformed (CallbackContext callbackContext)
    {
        Vector2 inputVector = callbackContext.ReadValue<Vector2>();
        CachedMovementVector = new Vector3(inputVector.x, 0, inputVector.y);
    }

    protected virtual void FixedUpdate ()
    {
        UpdateMovement();
    }

    private void UpdateMovement ()
    {
        Vector3 velocityOfTransform = MovementVector.TransformDirection(CachedMovementVector * MovementSpeedFactor);
        Vector3 forceWithoutY = velocityOfTransform - CharacterRigidbody.velocity;

        CharacterRigidbody.AddForce(forceWithoutY, ForceMode.Impulse);
    }
}
