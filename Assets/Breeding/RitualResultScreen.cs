using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RitualResultScreen : MonoBehaviour
{
    [field: SerializeField]
    private Image EntityImage { get; set; }
    [field: SerializeField]
    private TMP_Text EntityCustomNameLabel { get; set; }
    [field: SerializeField]
    private TMP_Text EntityDefaultNameLabel { get; set; }
    [field: SerializeField]
    private List<SliderStatPair> SliderStatPairCollection { get; set; }
    [field : SerializeField]
    private TraitListController TraitList { get; set; }

    public void Show (Entity createdEntity)
    {
        EntityImage.sprite = createdEntity.BaseEntityType.Image;
        EntityCustomNameLabel.text = createdEntity.Name.PresentValue;
        EntityDefaultNameLabel.text = createdEntity.BaseEntityType.Name;

        foreach (SliderStatPair pair in SliderStatPairCollection)
        {
            Vector2 baseStatRange = createdEntity.BaseEntityType.BaseMatRange.GetStatOfType(pair.StatType);
            pair.ProgressBar.SetValue(baseStatRange.x, baseStatRange.y, createdEntity.MatStats.GetStatOfType(pair.StatType));
        }

        gameObject.SetActive(true);

        TraitList.InitializeTraits(createdEntity.TraitsCollection);
    }

    public void Hide ()
    {
        gameObject.SetActive(false);
    }
}
