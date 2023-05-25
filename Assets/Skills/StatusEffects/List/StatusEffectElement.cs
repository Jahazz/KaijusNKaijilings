using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatusEffectElement : MonoBehaviour
{
    [field: SerializeField]
    private Image StatusEffectImage { get; set; }
    [field: SerializeField]
    private TMP_Text StatusEffectNameLabel { get; set; }

    private BaseStatusEffect SourceStatusEffect;

    public void Initialize (BaseStatusEffect sourceStatusEffect)
    {
        SourceStatusEffect = sourceStatusEffect;
        StatusEffectImage.sprite = SourceStatusEffect.BaseData.Image;
        StatusEffectNameLabel.text = SourceStatusEffect.BaseData.Name;
    }
}
