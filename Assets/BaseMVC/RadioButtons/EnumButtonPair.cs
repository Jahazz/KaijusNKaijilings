using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MVC.RadioButton
{
    [Serializable]
    public class EnumButtonPair<EnumType> where EnumType : struct, IConvertible
    {
        [field: SerializeField]
        public Button Button { get; private set; }
        [field: SerializeField]
        public EnumType Enum { get; private set; }
    }
}

