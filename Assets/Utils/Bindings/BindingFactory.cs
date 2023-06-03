using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using Utils;

namespace Bindings
{
    public static class BindingFactory
    {
        public static Binding GenerateCustomProgressBarBinding (CustomProgressBar progressBar, ObservableVariable<float> minValue, ObservableVariable<float> maxValue, ObservableVariable<float> currentValue)
        {
            progressBar.SetValue(minValue.PresentValue, maxValue.PresentValue, currentValue.PresentValue);

            minValue.OnVariableChange += HandleValueChange;
            maxValue.OnVariableChange += HandleValueChange;
            currentValue.OnVariableChange += HandleValueChange;

            void HandleValueChange (float newValue)
            {
                progressBar.SetValue(minValue.PresentValue, maxValue.PresentValue, currentValue.PresentValue);
            }

            return new Binding(() => {
                minValue.OnVariableChange -= HandleValueChange;
                maxValue.OnVariableChange -= HandleValueChange;
                currentValue.OnVariableChange -= HandleValueChange;
            });
        }

        public static Binding GenerateStatElementBinding (StatElement statElement, ObservableVariable<float> currentStatValue, ObservableVariable<float> modifiedValue, StatType statType)
        {
            statElement.SetStatValues(currentStatValue.PresentValue, modifiedValue.PresentValue, statType);

            currentStatValue.OnVariableChange += HandleValueChange;
            modifiedValue.OnVariableChange += HandleValueChange;

            void HandleValueChange (float newValue)
            {
                statElement.SetStatValues(currentStatValue.PresentValue, modifiedValue.PresentValue, statType);
            }

            return new Binding(() =>
            {
                currentStatValue.OnVariableChange -= HandleValueChange;
                modifiedValue.OnVariableChange -= HandleValueChange;
            });
        }

        public static Binding GenerateInputFieldBinding(TMP_InputField textField,string format, params ObservableVariable<string>[] textCollection)
        {
            HandleValueChange();

            foreach (ObservableVariable<string> observableString in textCollection)
            {
                observableString.OnVariableChange += HandleValueChange;
            }

            void HandleValueChange (string newValue = "")
            {
                textField.text = string.Format(format, textCollection.Select(observableElement => observableElement.PresentValue).ToArray());
            }

            return new Binding(() =>
            {
                foreach (ObservableVariable<string> observableString in textCollection)
                {
                    observableString.OnVariableChange += HandleValueChange;
                }
            });
        }
    }
}
