using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MVC.RuntimeTabbedView
{
    public class TabbedViewTab<TabbedViewButtonType, EnumType> : MonoBehaviour where TabbedViewButtonType : TabbedViewButton<EnumType>
        where EnumType : struct, IConvertible
    {
        public EnumType BoundType { get; internal set; }

    }
}