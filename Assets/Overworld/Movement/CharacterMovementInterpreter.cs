using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovementInterpreter : MonoBehaviour
{
    [field: SerializeField]
    private float AnimationSpeed { get; set; }
    [field: SerializeField]
    private Transform Model { get; set; }
    [field: SerializeField]
    private Animator ConnectedAnimator { get; set; }

    private const string ANIMATOR_SPEED_PARAMETER_NAME = "Speed";

    public void FreezeCharacterMovement ()
    {
        //ConnectedRigidbody.velocity = Vector3.zero;
        //ConnectedRigidbody.constraints = RigidbodyConstraints.FreezeAll;
    }

    public void UnfreezeCharacterMovement ()
    {
        //ConnectedRigidbody.constraints = RigidbodyConstraints.FreezeRotation;
    }

    Vector3 lastVelocity;

    public void UpdateRotation (Vector3 movementVector)
    {
        Debug.DrawRay(transform.position, movementVector);
        //Vector3 currentVelocity = ConnectedRigidbody.velocity;

        if (movementVector != Vector3.zero)
        {
            Model.rotation = Quaternion.LookRotation(new Vector3(movementVector.x, 0, movementVector.z));
        }

        UpdateSpeed(movementVector);
    }

    private void UpdateSpeed (Vector3 movementVector)
    {
        float normalizedVelocity = movementVector.magnitude * AnimationSpeed;
        ConnectedAnimator.SetFloat(ANIMATOR_SPEED_PARAMETER_NAME, normalizedVelocity);
    }
}
