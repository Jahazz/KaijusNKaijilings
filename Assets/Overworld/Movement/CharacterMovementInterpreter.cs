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

    protected virtual void FixedUpdate ()
    {
        UpdateRotation();
        UpdateSpeed();
        UpdatePosition();
    }

    private void UpdateRotation ()
    {
        Vector3 currentVelocity = ConnectedRigidbody.velocity;

        if (currentVelocity != Vector3.zero)
        {
            Model.rotation = Quaternion.LookRotation(new Vector3(currentVelocity.x, 0, currentVelocity.z));
        }
    }

    private void UpdatePosition ()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position + Vector3.up, transform.TransformDirection(Vector3.down), out hit, 5.0f))
        {
            if(hit.transform.CompareTag("Walkable"))
            {
                //ConnectedRigidbody.transform.position = new Vector3(ConnectedRigidbody.transform.position.x, hit.point.y, ConnectedRigidbody.transform.position.z);
            }
        }
    }

    private void UpdateSpeed ()
    {
        float normalizedVelocity = ConnectedRigidbody.velocity.magnitude / 13; //remove this magic as max velocity
        ConnectedAnimator.SetFloat(ANIMATOR_SPEED_PARAMETER_NAME, normalizedVelocity);
    }
}
