using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CustomProgressBar : MonoBehaviour
{
    [field: SerializeField]
    private Slider ConnectedSlider { get; set; }
    [field: SerializeField]
    private TMP_Text ConnectedText { get; set; }
    [field: SerializeField]
    private string TextFormat { get; set; }

    public void SetValue (float minValue, float maxValue, float currentValue)
    {
        ConnectedSlider.minValue = minValue;
        ConnectedSlider.maxValue = maxValue;
        ConnectedSlider.value = currentValue;

        ConnectedText.text = string.Format(TextFormat, currentValue, maxValue);
    }
}
