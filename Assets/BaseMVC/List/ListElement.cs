using UnityEngine;

namespace MVC.List
{
    public abstract class ListElement<ElementData> : MonoBehaviour
    {
        public abstract void Initialize (ElementData elementData);
    }
}
