using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeSlider : MonoBehaviour
{
    [field: SerializeField]
    private RectTransform HandledRectTransform { get; set; }

    public void SetValue(float min, float max, float firstValue, float secondValue)
    {
        float maxWithoutOffset = max - min;
        float firstValueWithoutOffset = firstValue - min;
        float secondValueWithoutOffset = secondValue - min;

        StartCoroutine(UpdateSizeAfterFrame(GetPercentageValue(firstValueWithoutOffset, maxWithoutOffset), GetPercentageValue(secondValueWithoutOffset, maxWithoutOffset)));

    }

    private IEnumerator UpdateSizeAfterFrame (float start, float end)
    {
        yield return null;


        HandledRectTransform.anchorMin = new Vector2(start, HandledRectTransform.anchorMin.y);
        HandledRectTransform.anchorMax = new Vector2(end, HandledRectTransform.anchorMax.y);
        //= ;//GetPercentageValue(firstValueWIthoutOffset, maxWithoutOffset)
        HandledRectTransform.sizeDelta = Vector2.zero;

    }

    private float GetPercentageValue (float value, float maxValue)
    {
        return value / maxValue;
    }

    public void Update ()
    {

    }
}
