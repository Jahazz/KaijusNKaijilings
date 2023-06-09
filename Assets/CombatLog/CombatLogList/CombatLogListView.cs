using CombatLogging.Entries;
using MVC.List;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CombatLogging.UI
{
    public class CombatLogListView : ListView<CombatLogListElement, BaseCombatLogEntry>
    {
        [field: SerializeField]
        private ScrollRect ContainerScrollView { get; set; }

        public override CombatLogListElement AddNewItem (BaseCombatLogEntry elementData)
        {
            CombatLogListElement output = base.AddNewItem(elementData);
            ContainerScrollView.normalizedPosition = Vector2.zero;

            return output;
        }
    }
}
