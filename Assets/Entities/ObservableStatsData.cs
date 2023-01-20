using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class ObservableStatsData : BaseStatsData<ObservableVariable<float>>
{
    public Resource<float> Health { get; set; } = new Resource<float>(1);
    public Resource<float> Mana { get; set; } = new Resource<float>(1);

    public ObservableStatsData ()
    {
        Might = new ObservableVariable<float>();
        Magic = new ObservableVariable<float>();
        Willpower = new ObservableVariable<float>();
        Agility = new ObservableVariable<float>();
        Initiative = new ObservableVariable<float>();

        Might.OnVariableChange += HandleOnMightValueCHange;
        Magic.OnVariableChange += HandleOnMagicValueChange;
    }

    public ObservableVariable<float> GetStatOfType (StatType statType)
    {
        ObservableVariable<float> output = default;

        switch (statType)
        {
            case StatType.CURRENT_HEALTH:
                output = Health.CurrentValue;
                break;
            case StatType.MAX_HEALTH:
                output = Health.MaxValue;
                break;
            case StatType.CURRENT_MANA:
                output = Health.CurrentValue;
                break;
            case StatType.MAX_MANA:
                output = Health.MaxValue;
                break;
            case StatType.MIGHT:
                output = Might;
                break;
            case StatType.MAGIC:
                output = Magic;
                break;
            case StatType.WILLPOWER:
                output = Willpower;
                break;
            case StatType.AGILITY:
                output = Agility;
                break;
            case StatType.INITIATIVE:
                output = Initiative;
                break;
            default:
                break;
        }

        return output;
    }

    public void ApplyModifierToModifiedStats (StatType modfiedStat, float value)
    {
        switch (modfiedStat)
        {
            case StatType.CURRENT_HEALTH:
                Health.CurrentValue.PresentValue = value;
                break;
            case StatType.MAX_HEALTH:
                Health.MaxValue.PresentValue = value;
                break;
            case StatType.CURRENT_MANA:
                Health.CurrentValue.PresentValue = value;
                break;
            case StatType.MAX_MANA:
                Health.MaxValue.PresentValue = value;
                break;
            case StatType.MIGHT:
                Might.PresentValue = value;
                break;
            case StatType.MAGIC:
                Magic.PresentValue = value;
                break;
            case StatType.WILLPOWER:
                Willpower.PresentValue = value;
                break;
            case StatType.AGILITY:
                Agility.PresentValue = value;
                break;
            case StatType.INITIATIVE:
                Initiative.PresentValue = value;
                break;
            default:
                break;
        }
    }

    private void HandleOnMagicValueChange (float newValue)
    {
        RecalculateResourceValue(Mana, Magic.PresentValue);
    }

    private void HandleOnMightValueCHange (float newValue)
    {
        RecalculateResourceValue(Health, Might.PresentValue);
    }

    private void RecalculateResourceValue (Resource<float> targetResource, float newMaxValue)
    {
        float resourcePercentageValue = targetResource.MaxValue.PresentValue / targetResource.CurrentValue.PresentValue;
        float newCurrentValue = resourcePercentageValue * newMaxValue;
        newCurrentValue = Mathf.Clamp(newCurrentValue, 1, newMaxValue);

        targetResource.MaxValue.PresentValue = newMaxValue;
        targetResource.CurrentValue.PresentValue = newCurrentValue;
    }
}
