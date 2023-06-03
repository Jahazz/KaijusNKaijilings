using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatElement : MonoBehaviour
{
    [field: SerializeField]
    private TMP_Text StatValueLabel { get; set; }
    [field: SerializeField]
    private TMP_Text StatModifierLabel { get; set; }
    [field: SerializeField]
    private TMP_Text FinalStatValue { get; set; }
    [field: SerializeField]
    private Color PositiveStatValueColor { get; set; }
    [field: SerializeField]
    private Color NegativeStatValueColor { get; set; }
    [field: SerializeField]
    private Color DefaultStatValueColor { get; set; }
    [field: SerializeField]
    private string StatModifierFormat { get; set; }
    [field: SerializeField]
    private Image StatImage { get; set; }
    [field: SerializeField]
    private string StatFormat { get; set; }

    public void SetStatValues (float baseStatValue, float finalStatValue, StatType statType)
    {
        StatValueLabel.text = string.Format(StatFormat, baseStatValue);
        FinalStatValue.text = string.Format(StatFormat, finalStatValue);
        StatImage.sprite = SingletonContainer.Instance.EntityManager.StatTypeSpriteDictionary[statType];

        float statModifierValue = finalStatValue - baseStatValue;

        if (statModifierValue > 0)
        {
            StatModifierLabel.color = PositiveStatValueColor;
        }
        else if (statModifierValue == 0)
        {
            StatModifierLabel.color = DefaultStatValueColor;
        }
        else
        {
            StatModifierLabel.color = NegativeStatValueColor;
        }

        StatModifierLabel.text = string.Format(StatModifierFormat, statModifierValue >= 0 ? "+" : "", statModifierValue);
    }
}
