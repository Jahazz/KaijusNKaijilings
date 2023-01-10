using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace MVC.RuntimeTabbedView
{
    public class TabbedViewButton<EnumType> : MonobehaviourWithEvents
        where EnumType : struct, IConvertible
    {
        [field: SerializeField]
        public Button BoundButton { get; private set; }
        [field: SerializeField]
        public TMP_Text ButtonText { get; private set; }

        public EnumType BoundType { get; internal set; }
        private Action ClickCallback { get; set; }

        public virtual void Initialize (Action clickCallback)
        {
            ClickCallback = clickCallback;
        }
        public virtual void SetButtonText (string text)
        {
            ButtonText.text = text;
        }

        protected override void AttachToEvents ()
        {
            base.AttachToEvents();
            BoundButton.onClick.AddListener(() => ClickCallback?.Invoke());
        }

        protected override void DetachFromEvents ()
        {
            base.DetachFromEvents();
            BoundButton.onClick.RemoveListener(() => ClickCallback?.Invoke());
        }
    }
}
