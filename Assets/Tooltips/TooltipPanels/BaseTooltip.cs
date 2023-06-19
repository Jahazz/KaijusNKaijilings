using UnityEngine;
using TMPro;
using System.Collections.Generic;
using Utils;

namespace Tooltips.UI
{
    public abstract class BaseTooltip : MonoBehaviour
    {
        public delegate void OnTooltipDestroyedParams (BaseTooltip destroyedTooltip);
        public event OnTooltipDestroyedParams OnTooltipDestroyed;
        [field: SerializeField]
        public RectTransform RectTransform { get; private set; }
        [field: SerializeField]
        protected TMP_Text TooltipTopLabel { get; private set; }
        [field: SerializeField]
        protected TMP_Text TooltipDesciptionLabel { get; private set; }
        public TooltipType TooltipType { get; private set; }
        public string TooltipSourceGUID { get; private set; }
        protected INameableGUIDableDescribableTooltipable SourceObject { get; private set; }

        public virtual void Initialize (TooltipType tooltipType, string GUID)
        {
            TooltipType = tooltipType;
            TooltipSourceGUID = GUID;
            SourceObject = GetDataForTooltipOfTypeAndGUID(TooltipType, GUID);
            FillBaseData(SourceObject);
        }

        protected void FillBaseData (INameableGUIDableDescribableTooltipable data)
        {
            TooltipTopLabel.text = data.Name;
            TooltipDesciptionLabel.text = data.Description;
        }

        public void Close ()
        {
            OnTooltipDestroyed?.Invoke(this);
            Destroy(gameObject);
        }

        private INameableGUIDableDescribableTooltipable GetDataForTooltipOfTypeAndGUID (TooltipType type, string GUID)
        {
            IEnumerable<INameableGUIDableDescribableTooltipable> listToLookFor = default;

            switch (type)
            {
                case TooltipType.ABILITY:
                    listToLookFor = SingletonContainer.Instance.SkillManagerInstance.SkillsPreloadedCollection;
                    break;
                case TooltipType.ENTITY_STATUS_EFFECT:
                    listToLookFor = SingletonContainer.Instance.EntityManager.AvailableEntityStatusEffects;
                    break;
                case TooltipType.BATTLEGROUND_STATUS_EFFECT:
                    listToLookFor = SingletonContainer.Instance.EntityManager.AvailableBattlegroundStatusEffects;
                    break;
                case TooltipType.KEYWORD:
                    listToLookFor = SingletonContainer.Instance.TooltipManager.AdditionalTooltips;
                    break;
                case TooltipType.STAT:
                    listToLookFor = SingletonContainer.Instance.EntityManager.StatTypeSpriteCollection;
                    break;
                case TooltipType.ENTITY_TYPE:
                    listToLookFor = SingletonContainer.Instance.EntityManager.AvailableTypes;
                    break;
                case TooltipType.ENTITY:
                    listToLookFor = SingletonContainer.Instance.EntityManager.AllEntitiesTypes;
                    break;
                case TooltipType.TRAIT:
                    listToLookFor = SingletonContainer.Instance.EntityManager.AvailableTraits;
                    break;
                default:
                    break;
            }

            return listToLookFor.GetElementByGUIDFromCollection(GUID);
        }
    }
}

