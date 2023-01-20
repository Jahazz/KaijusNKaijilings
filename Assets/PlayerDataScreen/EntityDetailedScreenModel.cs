using MVC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityDetailedScreenModel : BaseModel<EntityDetailedScreenView>
{
    private Entity CurrentEntity { get; set; }

    public void ShowDataOfEntity (Entity targetEntity)
    {
        CurrentEntity = targetEntity;

        CurrentView.SetData(CurrentEntity);
    }

    public void ChangeEntityCustomName (string value)
    {
        CurrentEntity.Name.PresentValue = value;
    }
}
