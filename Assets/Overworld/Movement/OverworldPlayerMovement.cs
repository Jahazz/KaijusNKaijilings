using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Utils;
using static UnityEngine.InputSystem.InputAction;

public class OverworldPlayerMovement : MonobehaviourWithEvents
{
    [field: SerializeField]
    private Rigidbody CharacterRigidbody { get; set; }
    [field: SerializeField]
    private InputActionAsset InputActions { get; set; }
    [field: SerializeField]
    private Transform MovementVector { get; set; }
    [field: SerializeField]
    private float MovementSpeedFactor { get; set; }

    private InputAction MovementActions { get; set; }
    private Vector3 CachedMovementVector { get; set; } = Vector3.zero;

    private const string MOVEMENT_ACTION_NAME = "Move";

    protected override void Start ()
    {
        MovementActions = InputActions.FindAction(MOVEMENT_ACTION_NAME);
        base.Start();
    }

    protected virtual void FixedUpdate ()
    {
        UpdateMovement();
    }

    protected override void AttachToEvents ()
    {
        base.AttachToEvents();

        MovementActions.performed += HandleMovementActionPerformed;
        MovementActions.canceled += HandleMovementActionCanceled;
    }

    private void HandleMovementActionCanceled (CallbackContext obj)
    {
        CachedMovementVector = Vector3.zero;
    }

    protected override void DetachFromEvents ()
    {
        base.DetachFromEvents();
        MovementActions.performed += HandleMovementActionPerformed;
        MovementActions.canceled -= HandleMovementActionCanceled;
    }

    private void HandleMovementActionPerformed (CallbackContext callbackContext)
    {
        Vector2 inputVector = callbackContext.ReadValue<Vector2>();
        CachedMovementVector = new Vector3(inputVector.x, 0, inputVector.y);
    }

    private void UpdateMovement ()
    {
        Vector3 currentVelocity = CachedMovementVector * MovementSpeedFactor;
        CharacterRigidbody.AddForce(MovementVector.TransformDirection(currentVelocity) - CharacterRigidbody.velocity,ForceMode.Acceleration);
    }

    
}
