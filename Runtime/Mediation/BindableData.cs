using System;
using System.Collections.Generic;

namespace Blanketmen.Hypnos.Mediation
{
    public class BindableData<T> where T : unmanaged
    {
        public static implicit operator T(BindableData<T> data) => data != null ? data.Value : default;

        private Action<T> onValueChanged;
        private T value;

        public T Value
        {
            get => value;
            set
            {
                if (!EqualityComparer<T>.Default.Equals(this.value, value))
                {
                    this.value = value;
                    onValueChanged?.Invoke(value);
                }
            }
        }

        public BindableData(T val)
        {
            value = val;
        }

        public void Register(Action<T> onValueChangedCb)
        {
            onValueChanged += onValueChangedCb;
            onValueChangedCb?.Invoke(value);
        }

        public void Unregister(Action<T> onValueChangedCb)
        {
            onValueChanged -= onValueChangedCb;
        }
    }
}