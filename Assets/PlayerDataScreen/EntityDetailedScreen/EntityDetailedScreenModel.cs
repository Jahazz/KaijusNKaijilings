using MVC;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityDetailedScreenModel : BaseEntityDetailedScreenModel<EntityDetailedScreenView>
{


    public void ChangeEntityCustomName (string value)
    {
        CurrentEntity.Name.PresentValue = value;
    }
}
