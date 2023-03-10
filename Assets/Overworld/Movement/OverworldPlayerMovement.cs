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

    private bool IsInAir = false;

    private const string MOVEMENT_ACTION_NAME = "Move";

    protected override void Start ()
    {
        MovementActions = InputActions.FindAction(MOVEMENT_ACTION_NAME);
        base.Start();
    }

    protected virtual void FixedUpdate ()
    {
        CheckIfPlayerIsInAir();
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
        Vector3 velocityOfTransform = MovementVector.TransformDirection(CachedMovementVector * MovementSpeedFactor);
        Vector3 forceWithoutY = velocityOfTransform - CharacterRigidbody.velocity;

        CharacterRigidbody.AddForce(forceWithoutY, ForceMode.Impulse);

        if (IsInAir == true)
        {
            CharacterRigidbody.AddForce(Vector3.down*20, ForceMode.Impulse);
        }
    }

    private void CheckIfPlayerIsInAir ()
    {
        float distanceOffset = 1.0f;
        if (Physics.Raycast(transform.position+ Vector3.up, Vector3.down, out RaycastHit hit, 5.0f))
        {
            IsInAir = hit.distance > 0.5f + distanceOffset;
            if (hit.distance > 0.5f + distanceOffset)
            {
                Debug.Log("ie");
            }
            else
            {
                Debug.Log("ground");
            }
        }
    }


}
