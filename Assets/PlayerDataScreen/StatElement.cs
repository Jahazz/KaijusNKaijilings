using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

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



    public void SetStatValues (float baseStatValue, float finalStatValue)
    {
        StatValueLabel.text = baseStatValue.ToString();
        FinalStatValue.text = finalStatValue.ToString();

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
