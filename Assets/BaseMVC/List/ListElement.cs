using UnityEngine;
using UnityEngine.EventSystems;

namespace MVC.List
{
    public abstract class ListElement<ElementData> : MonoBehaviour, IBeginDragHandler, IDragHandler, IDropHandler
    {
        public delegate void OnElementDraggedArguments ();
        public event OnElementDraggedArguments OnElementDropped;

        public abstract void Initialize (ElementData elementData);

        private bool IsDraggingSetUp { get; set; }
        public RectTransform currentTransform;
        private GameObject Container;
        private Vector3 CurrentPosition;
        private int ChildCount;

        internal void SetupDragging ()
        {
            IsDraggingSetUp = true;
            currentTransform = GetComponent<RectTransform>();
        }

        public void OnBeginDrag (PointerEventData eventData)
        {
            if (IsDraggingSetUp == true)
            {
                HandleBeginDrag();
            }
        }

        public void OnDrop (PointerEventData eventData)
        {
            if (IsDraggingSetUp == true)
            {
                HandleDrop();
            }
        }

        public void OnDrag (PointerEventData eventData)
        {
            if (IsDraggingSetUp == true)
            {
                HandleDrag(eventData);
            }
        }

        private void HandleBeginDrag ()
        {
            CurrentPosition = currentTransform.position;
            Container = currentTransform.parent.gameObject;
            ChildCount = Container.transform.childCount;
        }

        private void HandleDrag (PointerEventData eventData) // source: https://github.com/dipen-apptrait/Vertical-drag-drop-listview-unity
        {
            currentTransform.position = new Vector3(currentTransform.position.x, eventData.position.y, currentTransform.position.z);

            for (int i = 0; i < ChildCount; i++)
            {
                if (i != currentTransform.GetSiblingIndex())
                {
                    Transform otherTransform = Container.transform.GetChild(i);
                    int distance = (int)Vector3.Distance(currentTransform.position, otherTransform.position);

                    if (distance <= 10)
                    {
                        Vector3 otherTransformOldPosition = otherTransform.position;
                        otherTransform.position = new Vector3(otherTransform.position.x, CurrentPosition.y,otherTransform.position.z);
                        currentTransform.position = new Vector3(currentTransform.position.x, otherTransformOldPosition.y, currentTransform.position.z);
                        currentTransform.SetSiblingIndex(otherTransform.GetSiblingIndex());
                        CurrentPosition = currentTransform.position;
                    }
                }
            }
        }

        private void HandleDrop ()
        {
            currentTransform.position = CurrentPosition;
            OnElementDropped.Invoke();
        }
    }
}
