using System;
using System.Collections.Generic;
using UnityEngine.Events;

namespace MVC.List
{
    [Serializable]

    public class UnityListOrderChangedParams<ElementData> : UnityEvent<List<ElementData>>
    {

    }
}