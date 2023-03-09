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

    private Vector3 LastVelocity;

    private const string ANIMATOR_SPEED_PARAMETER_NAME = "Speed";

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
            Model.rotation = Quaternion.LookRotation(ConnectedRigidbody.velocity);
            LastVelocity = currentVelocity;
        }
    }

    private void UpdateSpeed ()
    {
        float normalizedVelocity = ConnectedRigidbody.velocity.magnitude/26;
        ConnectedAnimator.SetFloat(ANIMATOR_SPEED_PARAMETER_NAME, normalizedVelocity);
    }
}
