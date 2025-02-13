using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Blanketmen.Hypnos.Mediation
{
    /// <summary>
    /// A class that holds a value and notifies when the value changes.
    /// </summary>
    /// <remarks>
    /// NOTE: Ensure consistency between operator == and IEquatable&lt;T&gt;.Equals(T) to avoid unexpected behavior in equality checks.
    /// </remarks>
    public class BindableData<T> : IDisposable where T : unmanaged
    {
        private static readonly EqualityComparer<T> comparer = EqualityComparer<T>.Default;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator T(BindableData<T> data) => data != null ? data.Value : default;

        private Action<T> onValueChanged;
        private T value;

        public T Value
        {
            get => value;
            set
            {
                if (!comparer.Equals(this.value, value))
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

        public void Dispose()
        {
            onValueChanged = null;
            GC.SuppressFinalize(this);
        }

        public void Register(Action<T> onValueChangedCb)
        {
            if (onValueChanged != null)
            {
                onValueChanged += onValueChangedCb;
                onValueChangedCb?.Invoke(value);
            }
        }

        public void Unregister(Action<T> onValueChangedCb)
        {
            if (onValueChanged != null)
            {
                onValueChanged -= onValueChangedCb;
            }
        }
    }
}