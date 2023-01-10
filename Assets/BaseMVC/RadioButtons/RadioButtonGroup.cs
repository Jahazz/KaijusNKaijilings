using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace MVC.RadioButton
{
    public class RadioButtonGroup<EnumType> : MonobehaviourWithEvents where EnumType : struct, IConvertible
    {
        public delegate void OnButtonClickArguments (EnumType selectedEnum);
        public event OnButtonClickArguments OnRadioSelected;

        [field: SerializeField]
        public int DefaultSelectedButton { get; set; }
        [field: SerializeField]
        public List<EnumButtonPair<EnumType>> AvailableButtons { get; private set; }

        protected override void AttachToEvents ()
        {
            base.AttachToEvents();

            foreach (EnumButtonPair<EnumType> enumButtonPair in AvailableButtons)
            {
                enumButtonPair.Button.onClick.AddListener(() => { OnRadioSelected.Invoke(enumButtonPair.Enum); });
            }
        }

        protected override void DetachFromEvents ()
        {
            base.DetachFromEvents();

            foreach (EnumButtonPair<EnumType> enumButtonPair in AvailableButtons)
            {
                enumButtonPair.Button.onClick.RemoveAllListeners();
            }
        }
    }
}
