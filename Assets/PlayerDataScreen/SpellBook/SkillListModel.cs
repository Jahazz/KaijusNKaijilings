using MVC.SelectableList;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillListModel : SelectableListModel<SkillListElement, SkillScriptableObject, SkillListView>
{
    [field: SerializeField]
    private int MaxNumberOfSkills { get; set; }
    private Entity SourceEntity { get; set; }

    protected override void AttachToEvents ()
    {
        base.AttachToEvents();
        CurrentView.OnElementSelection += HandleOnElementSelection;
    }

    protected override void DetachFromEvents ()
    {
        base.DetachFromEvents();
        CurrentView.OnElementSelection -= HandleOnElementSelection;
    }

    public void Initialize (Entity sourceEntity)
    {
        SourceEntity = sourceEntity;
        CurrentView.ClearList();

        foreach (LevelSkillPair skillWithRequirement in SourceEntity.BaseEntityType.SkillsWithRequirements)
        {
            if (skillWithRequirement.RequiredLevel <= SourceEntity.LevelData.CurrentLevel.PresentValue)
            {
                SkillListElement createdElement = CurrentView.AddNewItem(skillWithRequirement.AssignedSkill);

                if (SourceEntity.SelectedSkillsCollection.Contains(skillWithRequirement.AssignedSkill))
                {
                    createdElement.Select();
                }
                else
                {
                    createdElement.Deselect();
                }
            }
        }
    }

    private void HandleOnElementSelection (SkillScriptableObject selectedElementData, bool isSelected)
    {
        if (isSelected == true)
        {
            if (SourceEntity.SelectedSkillsCollection.Contains(selectedElementData) == false)
            {
                SourceEntity.SelectedSkillsCollection.Add(selectedElementData);
            }
        }
        else
        {
            SourceEntity.SelectedSkillsCollection.Remove(selectedElementData);
        }

        if (SourceEntity.SelectedSkillsCollection.Count >= MaxNumberOfSkills)
        {
            SetUntoggledToggleActivity(false);
        }
        else
        {
            SetUntoggledToggleActivity(true);
        }
    }

    private void SetUntoggledToggleActivity (bool isActive)
    {
        foreach (var item in CurrentView.ContainingElementsCollection)
        {
            if (SourceEntity.SelectedSkillsCollection.Contains(item.Key) == false)
            {
                item.Value.SetActiveOfToggle(isActive);
            }
        }
    }
}
