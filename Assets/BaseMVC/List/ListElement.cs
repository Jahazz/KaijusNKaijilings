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
        public RectTransform CurrentTransform { get; private set; }
        private GameObject Container { get; set; }
        private Vector3 CurrentPosition { get; set; }
        private int ChildCount { get; set; }

        internal void SetupDragging ()
        {
            IsDraggingSetUp = true;
            CurrentTransform = GetComponent<RectTransform>();
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
            CurrentPosition = CurrentTransform.position;
            Container = CurrentTransform.parent.gameObject;
            ChildCount = Container.transform.childCount;
        }

        private void HandleDrag (PointerEventData eventData) // source: https://github.com/dipen-apptrait/Vertical-drag-drop-listview-unity
        {
            eventData.pressEventCamera.ScreenToWorldPoint(eventData.position);
            Debug.Log(eventData.position);
            Debug.Log(eventData.currentInputModule.name);
            float sign = eventData.delta.y > 0 ? 1.0f : -1.0f;
            CurrentTransform.position = new Vector3(CurrentTransform.position.x,eventData.pressEventCamera.ScreenToWorldPoint(eventData.position).y, CurrentTransform.position.z);

            for (int i = 0; i < ChildCount; i++)
            {
                if (i != CurrentTransform.GetSiblingIndex())
                {
                    Transform otherTransform = Container.transform.GetChild(i);
                    int distance = (int)Vector3.Distance(CurrentTransform.position, otherTransform.position);

                    if (distance <= 10)
                    {
                        Vector3 otherTransformOldPosition = otherTransform.position;
                        otherTransform.position = new Vector3(otherTransform.position.x, CurrentPosition.y, otherTransform.position.z);
                        CurrentTransform.position = new Vector3(CurrentTransform.position.x, otherTransformOldPosition.y, CurrentTransform.position.z);
                        CurrentTransform.SetSiblingIndex(otherTransform.GetSiblingIndex());
                        CurrentPosition = CurrentTransform.position;
                    }
                }
            }
        }

        private void HandleDrop ()
        {
            CurrentTransform.position = CurrentPosition;
            OnElementDropped.Invoke();
        }
    }
}
