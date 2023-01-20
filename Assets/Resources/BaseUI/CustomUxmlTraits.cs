using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Base
{
    public class CustomUxmlTraits<ElementType> : VisualElement.UxmlTraits where ElementType : VisualElement
    {
        public override IEnumerable<UxmlChildElementDescription> uxmlChildElementsDescription {
            get { yield break; }
        }

        public void InitializeElements (VisualElement inputVisualElement, string visualElementPath, out ElementType outerObject, out VisualElement createdVisualElement)
        {
            outerObject = inputVisualElement as ElementType;
            outerObject.Clear();

            VisualTreeAsset visualTreeAsset = Resources.Load<VisualTreeAsset>(visualElementPath);
            createdVisualElement = visualTreeAsset.Instantiate();
            outerObject.Add(createdVisualElement);
        }
    }
}

