using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class CharacterMovementInterpreter : MonobehaviourWithEvents
{
    [field: SerializeField]
    private float AnimationSpeed { get; set; }
    [field: SerializeField]
    private Transform Model { get; set; }
    [field: SerializeField]
    private Animator ConnectedAnimator { get; set; }
    [field: SerializeField]
    private OverworldPlayerMovement PlayerMovementScript { get; set; }

    private const string ANIMATOR_SPEED_PARAMETER_NAME = "Speed";

    protected override void AttachToEvents ()
    {
        base.AttachToEvents();
        PlayerMovementScript.OnMovementProcessed += PlayerMovementScript_OnMovementProcessed;
    }

    protected override void DetachFromEvents ()
    {
        base.DetachFromEvents();
        PlayerMovementScript.OnMovementProcessed -= PlayerMovementScript_OnMovementProcessed;
    }

    private void PlayerMovementScript_OnMovementProcessed (Vector3 distance)
    {
        UpdateRotation(distance);
        UpdateSpeed(distance);
    }

    public void FreezeCharacterMovement ()
    {
        //ConnectedRigidbody.velocity = Vector3.zero;
        //ConnectedRigidbody.constraints = RigidbodyConstraints.FreezeAll;
    }

    public void UnfreezeCharacterMovement ()
    {
        //ConnectedRigidbody.constraints = RigidbodyConstraints.FreezeRotation;
    }

    public void UpdateRotation (Vector3 movementVector)
    {
        if (movementVector != Vector3.zero)
        {
            Model.rotation = Quaternion.LookRotation(new Vector3(movementVector.x, 0, movementVector.z));
        }
    }

    private void UpdateSpeed (Vector3 movementVector)
    {
        float normalizedVelocity = movementVector.magnitude * AnimationSpeed;
        ConnectedAnimator.SetFloat(ANIMATOR_SPEED_PARAMETER_NAME, normalizedVelocity);
    }
}
