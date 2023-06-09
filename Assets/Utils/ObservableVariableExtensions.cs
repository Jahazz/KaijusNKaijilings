using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Utils
{
    public static class ObservableVariableExtensions
    {


        public static void BindObservableTextToInputField (this ObservableVariable<string> customNameVariable, TMP_InputField targetTextBox)
        {
            customNameVariable.OnVariableChange += HandleVariableChange;

            void HandleVariableChange (string newValue, string _)
            {
                targetTextBox.text = newValue;
            }
        }
    }
}

