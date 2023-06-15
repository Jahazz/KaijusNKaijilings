using CombatLogging.Entries;
using MVC.List;
using TMPro;
using UnityEngine;

namespace CombatLogging.UI
{
    public class CombatLogListElement : ListElement<BaseCombatLogEntry>
    {
        [field: SerializeField]
        private TMP_Text ContentLabel { get; set; }
        public override void Initialize (BaseCombatLogEntry elementData)
        {
            ContentLabel.text = SingletonContainer.Instance.TooltipManager.AddKeywordTooltipsToText(elementData.EntryToString());
        }
    }
}
