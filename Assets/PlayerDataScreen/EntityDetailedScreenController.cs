using MVC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityDetailedScreenController : BaseController<EntityDetailedScreenModel, EntityDetailedScreenView>
{
    [field: SerializeField]
    private EntityListModel EntityListModel { get; set; }
    [field: SerializeField]
    private SkillListController SkillListController { get; set; }

    public void ChangeEntityCustomName (string newName)
    {
        CurrentModel.ChangeEntityCustomName(newName);
    }

    public void ShowSpellbook ()
    {
        SkillListController.Show(CurrentModel.CurrentEntity);
    }

    protected override void AttachToEvents ()
    {
        base.AttachToEvents();
        EntityListModel.OnElementSelection += HandleOnElementSelection;
    }

    protected override void DetachFromEvents ()
    {
        base.DetachFromEvents();
        EntityListModel.OnElementSelection -= HandleOnElementSelection;
    }

    private void HandleOnElementSelection (Entity selectedElementData, bool isSelected)
    {
        if (isSelected == true)
        {
            CurrentModel.ShowDataOfEntity(selectedElementData);
        }
    }
}
