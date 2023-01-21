using MVC.SelectableList;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillListController : SelectableListController<SkillListElement, SkillScriptableObject, SkillListView, SkillListModel>
{
    public void Show (Entity sourceEntity)
    {
        gameObject.SetActive(true);
        CurrentModel.Initialize(sourceEntity);
    }

    public void Hide ()
    {
        gameObject.SetActive(false);
    }
}
