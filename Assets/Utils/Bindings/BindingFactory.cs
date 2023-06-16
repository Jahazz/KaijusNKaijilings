using System;
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

            void HandleValueChange (float newValue, float _)
            {
                progressBar.SetValue(minValue.PresentValue, maxValue.PresentValue, currentValue.PresentValue);
            }

            return new Binding(() =>
            {
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

            void HandleValueChange (float newValue, float _)
            {
                statElement.SetStatValues(currentStatValue.PresentValue, modifiedValue.PresentValue, statType);
            }

            return new Binding(() =>
            {
                currentStatValue.OnVariableChange -= HandleValueChange;
                modifiedValue.OnVariableChange -= HandleValueChange;
            });
        }

        public static Binding GenerateInputFieldBinding<T> (TMP_InputField textField, string format, bool generateTooltip = false, params ObservableVariable<T>[] textCollection)
        {
            void HandleValueChange (T newValue = default, T _ = default)
            {
                textField.text = FormatGenerateTooltipIfNeeded(format, generateTooltip, textCollection);
            }

            return GenerateBindingForObservableVariable(HandleValueChange, textCollection);
        }

        public static Binding GenerateTextBinding<T> (TMP_Text textField, string format, bool generateTooltip = false, params ObservableVariable<T>[] textCollection)
        {
            void HandleValueChange (T newValue = default, T _ = default)
            {
                textField.text = FormatGenerateTooltipIfNeeded(format, generateTooltip, textCollection);
            }

            return GenerateBindingForObservableVariable(HandleValueChange, textCollection);
        }

        private static string FormatGenerateTooltipIfNeeded<T> (string format, bool isTooltipNeeded, params ObservableVariable<T>[] textCollection)
        {
            string formatedText = string.Format(format, textCollection.Select(observableElement => observableElement.PresentValue.ToString()).ToArray());

            if (isTooltipNeeded == true)
            {
                formatedText = SingletonContainer.Instance.TooltipManager.AddKeywordTooltipsToText(formatedText);
            }

            return formatedText;
        }

        private static Binding GenerateBindingForObservableVariable<T> (ObservableVariable<T>.VariableChangedArguments valueChangedFunction, params ObservableVariable<T>[] textCollection)
        {
            foreach (ObservableVariable<T> observableString in textCollection)
            {
                valueChangedFunction?.Invoke(observableString.PresentValue, observableString.PresentValue);
                observableString.OnVariableChange += valueChangedFunction;
            }

            return new Binding(() =>
            {
                foreach (ObservableVariable<T> observableString in textCollection)
                {
                    observableString.OnVariableChange -= valueChangedFunction;
                }
            });
        }
    }
}
