using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Base
{
    public class CustomVisualElement<OuterType> : VisualElement where OuterType: VisualElement, new()
    {
    }

}
