using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KLFramework
{
    public class BindableProperty<T> where T: IEquatable<T>
    {
        private T mValue = default(T);

        public T Value
        {
            get
            {
                return mValue;
            }
            set 
            {
                if (!value.Equals(mValue))
                {
                    mValue = value;
                    mOnValueChanged?.Invoke(value);
                }
            }
        }

        private Action<T> mOnValueChanged = (v) => { };

        public IUnRegister RegisterOnValueChanged(Action<T> onValueChanged)
        {
            mOnValueChanged+= onValueChanged;

            return new BindablePropertyUnRegister<T>()
            {
                BindableProperty = this,
                OnValueChanged = onValueChanged
            };
        }

        public void UnRegisterOnValueChanged(Action<T> onValueChanged)
        {
            mOnValueChanged-= onValueChanged;
        }
    }

    public class BindablePropertyUnRegister<T> : IUnRegister where T : IEquatable<T>
    {
        public BindableProperty<T> BindableProperty { get; set; }

        public Action<T> OnValueChanged { get; set; }

        public void UnRegister()
        {
            BindableProperty.UnRegisterOnValueChanged(OnValueChanged);
            BindableProperty = null;
            OnValueChanged = null;
        }
    }
}
