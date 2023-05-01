using MVC;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BaseEntityDetailedScreenView : BaseView
{
    [field: SerializeField]
    private TMP_Text DefaultNameLabel { get; set; }
    [field: SerializeField]
    private TMP_Text DescriptionLabel { get; set; }
    [field: SerializeField]
    private Image EntityImage { get; set; }
    protected Entity CurrentEntityData { get; set; }

    public virtual void SetData (Entity entity)
    {
        CurrentEntityData = entity;
        SetPersistentData();
    }

    private void SetPersistentData ()
    {
        DefaultNameLabel.text = CurrentEntityData.BaseEntityType.Name;
        DescriptionLabel.text = CurrentEntityData.BaseEntityType.Description;
        EntityImage.sprite = CurrentEntityData.BaseEntityType.Image;
    }
}
