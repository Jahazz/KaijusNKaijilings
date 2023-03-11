using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovementInterpreter : MonoBehaviour
{
    [field: SerializeField]
    private Rigidbody ConnectedRigidbody { get; set; }
    [field: SerializeField]
    private Transform Model { get; set; }
    [field: SerializeField]
    private Animator ConnectedAnimator { get; set; }

    private const string ANIMATOR_SPEED_PARAMETER_NAME = "Speed";

    public void FreezeCharacterMovement ()
    {
        ConnectedRigidbody.velocity = Vector3.zero;
        ConnectedRigidbody.constraints = RigidbodyConstraints.FreezeAll;
    }

    public void UnfreezeCharacterMovement ()
    {
        ConnectedRigidbody.constraints = RigidbodyConstraints.FreezeRotation;
    }

    protected virtual void FixedUpdate ()
    {
        UpdateRotation();
        UpdateSpeed();
    }

    private void UpdateRotation ()
    {
        Vector3 currentVelocity = ConnectedRigidbody.velocity;

        if (currentVelocity != Vector3.zero)
        {
            Model.rotation = Quaternion.LookRotation(new Vector3(currentVelocity.x, 0, currentVelocity.z));
        }
    }

    private void UpdateSpeed ()
    {
        float normalizedVelocity = ConnectedRigidbody.velocity.magnitude / 6; //remove this magic as max velocity
        ConnectedAnimator.SetFloat(ANIMATOR_SPEED_PARAMETER_NAME, normalizedVelocity);
    }
}
