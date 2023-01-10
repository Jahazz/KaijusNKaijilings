using System;
using System.Collections.Generic;
using UnityEngine;

namespace MVC.RuntimeTabbedView
{
    public class TabbedViewView<EnumType, CreatedTabType, CreatedButtonType> : BaseView
        where EnumType : struct, IConvertible
        where CreatedTabType : TabbedViewTab<CreatedButtonType, EnumType>
        where CreatedButtonType : TabbedViewButton<EnumType>
    {
        public delegate void ButtonTabPairArguments (TabbedViewButtonTabPair<CreatedButtonType, CreatedTabType, EnumType> arguments);
        public event ButtonTabPairArguments OnButtonAndTabPairCreated;

        [field: Space]
        [field: Header("TabbedView")]
        [field: SerializeField]
        private CreatedTabType TabPrefab { get; set; }
        [field: SerializeField]
        private CreatedButtonType ButtonPrefab { get; set; }
        [field: SerializeField]
        private Transform ButtonContainer { get; set; }
        [field: SerializeField]
        private Transform TabContainer { get; set; }

        public Dictionary<EnumType, TabbedViewButtonTabPair<CreatedButtonType, CreatedTabType, EnumType>> TabsAndButtonsCollection { get; private set; } = new Dictionary<EnumType, TabbedViewButtonTabPair<CreatedButtonType, CreatedTabType, EnumType>>();

        public TabbedViewButtonTabPair<CreatedButtonType, CreatedTabType, EnumType> CreateTabAndButton (EnumType tabEnum)
        {
            CreatedTabType createdTab = Instantiate(TabPrefab, TabContainer);
            CreatedButtonType createdButton = Instantiate(ButtonPrefab, ButtonContainer);

            createdButton.Initialize(() => ShowTabOfEnum(tabEnum));

            TabbedViewButtonTabPair<CreatedButtonType, CreatedTabType, EnumType> output = new TabbedViewButtonTabPair<CreatedButtonType, CreatedTabType, EnumType>(createdButton, createdTab, tabEnum);
            TabsAndButtonsCollection[tabEnum] = output;

            OnButtonAndTabPairCreated?.Invoke(output);

            return output;
        }

        public void ShowTabOfEnum (EnumType tabEnum)
        {
            ResetAllTabsAndButtons();

            TabbedViewButtonTabPair<CreatedButtonType, CreatedTabType, EnumType> pair = TabsAndButtonsCollection[tabEnum];

            pair.Button.BoundButton.interactable = false;
            pair.Tab.gameObject.SetActive(true);
        }

        public void ShowFirstTab ()
        {
            ShowTabOfEnum((EnumType)Enum.ToObject(typeof(EnumType), 0));
        }

        private void ResetAllTabsAndButtons ()
        {
            foreach (KeyValuePair<EnumType, TabbedViewButtonTabPair<CreatedButtonType, CreatedTabType, EnumType>> tabButtonPair in TabsAndButtonsCollection)
            {
                tabButtonPair.Value.Button.BoundButton.interactable = true;
                tabButtonPair.Value.Tab.gameObject.SetActive(false);
            }
        }
    }
}
