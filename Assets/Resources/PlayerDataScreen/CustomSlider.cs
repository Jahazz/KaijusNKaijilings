using System.Collections;
using System.Collections.Generic;
using UI.Base;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI
{
    public class CustomSlider : CustomVisualElement<CustomSlider>
    {

        public new class UxmlTraits : CustomUxmlTraits<CustomSlider>
        {

            public override void Init (VisualElement ve, IUxmlAttributes bag, CreationContext cc)
            {
                base.Init(ve, bag, cc);

                InitializeElements(ve, "PlayerDataScreen/CustomSlider", out CustomSlider createdCustomSlider, out VisualElement visualElement);
            }
        }
    }
}
