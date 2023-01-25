using MVC.List;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TraitListElement : ListElement<TraitBaseScriptableObject>
{
    [field: SerializeField]
    private Image TraitIcon { get; set; }
    [field: SerializeField]
    private GameObject TooltipObject { get; set; }
    [field: SerializeField]
    private TMP_Text TraitTitleLabel { get; set; }
    [field: SerializeField]
    private TMP_Text TraitDescriptionLabel { get; set; }

    public override void Initialize (TraitBaseScriptableObject elementData)
    {
        
        TraitIcon.sprite = elementData.Image;
        TraitTitleLabel.text = elementData.Name;
        TraitDescriptionLabel.text = elementData.Description;

    }

    public void HandleOnHover ()
    {
        SetTooltipVisibility(true);
    }

    public virtual void HandleOnEndHover ()
    {
        SetTooltipVisibility(false);
    }

    private void SetTooltipVisibility (bool isVisible)
    {
        TooltipObject.SetActive(isVisible);
    }
}
