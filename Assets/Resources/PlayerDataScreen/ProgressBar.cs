using System.Collections;
using System.Collections.Generic;
using UI.Base;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI
{
    public class ProgressBar : CustomVisualElement<ProgressBar>
    {
        public float MinValue { get; set; }
        [field: SerializeField]
        public float MaxValue { get; set; }
        [field: SerializeField]
        public float CurrentValue { get; set; }

        public new class UxmlFactory : UxmlFactory<ProgressBar, UxmlTraits> { }

        private float CalculateProgress ()
        {
            return Mathf.InverseLerp(MinValue, MaxValue, CurrentValue);
        }

        private VisualElement pbParent;
        private VisualElement pbBackground;
        private VisualElement pbForeground;

        public new class UxmlTraits : CustomUxmlTraits<ProgressBar>
        {
            UxmlFloatAttributeDescription m_minValue = new UxmlFloatAttributeDescription { name = "MinValue", defaultValue = 0.0f };
            UxmlFloatAttributeDescription m_maxValue = new UxmlFloatAttributeDescription { name = "MaxValue", defaultValue = 1.0f };
            UxmlFloatAttributeDescription m_currentValue = new UxmlFloatAttributeDescription { name = "CurrentValue", defaultValue = 0.7f };
            
            public override void Init (VisualElement ve, IUxmlAttributes bag, CreationContext cc)
            {
                base.Init(ve, bag, cc);
                Debug.Log("sddasdsaasd");

                InitializeElements(ve, "BaseUI/ProgressBar", out ProgressBar outerObject, out VisualElement progressBar);

                outerObject.pbParent = progressBar.Q<VisualElement>("ProgressBar");
                outerObject.pbBackground = progressBar.Q<VisualElement>("Background");
                outerObject.pbForeground = progressBar.Q<VisualElement>("Foreground");
                progressBar.RegisterCallback<GeometryChangedEvent>(GeometryChangedCallback);
                outerObject.Add(progressBar);


                //createdProgressBar.pbParent.style.width = new StyleLength(new Length(100, LengthUnit.Percent));
                //createdProgressBar.pbParent.style.height = new StyleLength(new Length(100, LengthUnit.Percent));

                //createdProgressBar.style.width = new StyleLength(new Length(100, LengthUnit.Percent));
                //createdProgressBar.style.height = new StyleLength(new Length(100, LengthUnit.Percent));

                void GeometryChangedCallback (GeometryChangedEvent evt)
                {
                    outerObject.pbParent.style.width = outerObject.resolvedStyle.width;
                    outerObject.pbParent.style.height = outerObject.resolvedStyle.height;


                    outerObject.style.width = outerObject.resolvedStyle.width;
                    outerObject.style.height = outerObject.resolvedStyle.height;
                    HandleUpdateValue();
                }

                void HandleUpdateValue ()
                {
                    float value = m_currentValue.GetValueFromBag(bag, cc) / (m_maxValue.GetValueFromBag(bag, cc) - m_minValue.GetValueFromBag(bag, cc));
                    outerObject.pbForeground.style.width = new StyleLength(new Length(value*100, LengthUnit.Percent));
                }
            }
        }
    }


}

