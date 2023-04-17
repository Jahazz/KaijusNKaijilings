using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Utils;
using static UnityEngine.InputSystem.InputAction;

public class OverworldPlayerMovement : MonoBehaviour
{
    public delegate void OnMovementProcessedArguments (Vector3 distance);
    public event OnMovementProcessedArguments OnMovementProcessed;

    [field: SerializeField]
    private CharacterController CharacterController { get; set; }
    [field: SerializeField]
    private Transform MovementVector { get; set; }
    [field: SerializeField]
    private float MovementSpeedFactor { get; set; }
    private Vector3 CachedMovementVector { get; set; } = Vector3.zero;
    private bool CanMove { get; set; } = true;


    public void HandleMovementActionPerformed (CallbackContext callbackContext)
    {
        Vector2 inputVector = callbackContext.ReadValue<Vector2>();
        CachedMovementVector = new Vector3(inputVector.x, 0, inputVector.y);
    }

    public void SetCharacterMovementActive (bool canMove)
    {
        CanMove = canMove;
    }

    protected virtual void Update ()
    {
        if (CanMove == true)
        {
            UpdateMovement();
        }
    }

    private void UpdateMovement ()
    {
        Vector3 velocityOfTransform = MovementVector.TransformDirection(CachedMovementVector);
        Vector3 distance = velocityOfTransform * MovementSpeedFactor * Time.deltaTime;

        CharacterController.Move(distance);

        if (!CharacterController.isGrounded)
        {
            Vector3 playerVelocity = Vector3.zero;
            playerVelocity.y -= 9.5f * Time.deltaTime;
            CharacterController.Move(playerVelocity);
        }

        OnMovementProcessed?.Invoke(distance);
    }
}
