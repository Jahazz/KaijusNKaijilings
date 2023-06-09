using System;

namespace Utils
{
    public class ObservableVariable<Type>
    {
        public delegate void VariableChangedArguments(Type newValue, Type oldValue);
        public event VariableChangedArguments OnVariableChange;

        private Type presentValue;

        public Type PresentValue {
            get { return presentValue; }
            set {
                bool hasValueChanged = HasValueChanged(value, presentValue);
                Type oldValue = presentValue;
                presentValue = value;

                if (hasValueChanged == true)
                {
                    OnVariableChange?.Invoke(presentValue, oldValue);
                }
            }
        }

        public ObservableVariable (Type initialValue)
        {
            PresentValue = initialValue;
        }

        public ObservableVariable ()
        {
            PresentValue = default;
        }

        private bool HasValueChanged (Type oldValue, Type newValue)
        {

            return (oldValue == null && newValue != null) || (oldValue != null && newValue == null) || (oldValue != null && oldValue.Equals(newValue) == false) || oldValue == null;
        }
    }
}

