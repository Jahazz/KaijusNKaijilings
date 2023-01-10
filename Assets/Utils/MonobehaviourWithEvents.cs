using UnityEngine;

namespace Utils
{
    public class MonobehaviourWithEvents : MonoBehaviour
    {
        protected virtual void Start ()
        {
            AttachToEvents();
        }

        protected virtual void OnDestroy ()
        {
            DetachFromEvents();
        }

        protected virtual void AttachToEvents ()
        {

        }

        protected virtual void DetachFromEvents ()
        {

        }
    }
}

