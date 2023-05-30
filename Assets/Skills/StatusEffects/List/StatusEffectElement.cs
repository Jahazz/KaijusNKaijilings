using StatusEffects;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatusEffectElement<StatusType> : MonoBehaviour where StatusType: BaseScriptableStatusEffect
{
    [field: SerializeField]
    private Image StatusEffectImage { get; set; }
    [field: SerializeField]
    private TMP_Text StatusEffectNameLabel { get; set; }

    private BaseStatusEffect<StatusType> SourceStatusEffect;

    public void Initialize (BaseStatusEffect<StatusType> sourceStatusEffect)
    {
        SourceStatusEffect = sourceStatusEffect;
        StatusEffectImage.sprite = SourceStatusEffect.BaseData.Image;
        StatusEffectNameLabel.text = SourceStatusEffect.BaseData.Name;
    }
}
