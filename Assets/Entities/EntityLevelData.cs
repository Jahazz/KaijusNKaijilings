using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class EntityLevelData
{
    public delegate void LevelArguments ();
    public event LevelArguments OnLevelUp;

    [field: SerializeField]
    public ObservableVariable<int> CurrentLevel { get; private set; } = new ObservableVariable<int>(1);
    [field: SerializeField]
    public ObservableVariable<float> CurrentExperience { get; private set; } = new ObservableVariable<float>(0);
    [field: SerializeField]
    public ObservableVariable<float> ExperienceNeededForCurrentLevel { get; private set; } = new ObservableVariable<float>(0);
    [field: SerializeField]
    public ObservableVariable<float> ExperienceNeededForNextLevel { get; private set; } = new ObservableVariable<float>(0);

    public EntityLevelData ()
    {
        UpdateLevelData();
        OnLevelUp += UpdateLevelData;
    }

    public void UpdateLevelData ()
    {
        LevelRequirementsScriptable levelRequirements = SingletonContainer.Instance.EntityManager.LevelRequirements;
        ExperienceNeededForCurrentLevel.PresentValue = levelRequirements.GetExpNeededForLevel(CurrentLevel.PresentValue);
        ExperienceNeededForNextLevel.PresentValue = levelRequirements.GetExpNeededForLevel(CurrentLevel.PresentValue+1);

    }

    public void AddExperience (int experience)
    {
        CurrentExperience.PresentValue += experience;
        int targetLevel = SingletonContainer.Instance.EntityManager.LevelRequirements.GetLevelAtExp((int)CurrentExperience.PresentValue);

        while (CurrentLevel.PresentValue < targetLevel)
        {
            CurrentLevel.PresentValue++;
            OnLevelUp?.Invoke();
        }
    }
}
