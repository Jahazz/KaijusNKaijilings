using StatusEffects;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatusEffectElement<StatusType> : MonoBehaviour
{
    [field: SerializeField]
    private Image StatusEffectImage { get; set; }
    [field: SerializeField]
    private TMP_Text StatusEffectNameLabel { get; set; }

    private StatusType SourceStatusEffect;

    public virtual void Initialize (StatusType sourceStatusEffect)
    {
        SourceStatusEffect = sourceStatusEffect;
    }

    public void SetImageAndLabel (Sprite image, string label)
    {
        StatusEffectImage.sprite = image;
        StatusEffectNameLabel.text = label;
    }
}
