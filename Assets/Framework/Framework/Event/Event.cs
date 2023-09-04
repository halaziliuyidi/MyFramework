using OpenCover.Framework.Model;
using System;

namespace FrameworkDesign
{
    public class Event<T> where T : Event<T>
    {
        private static Action mOnEvent;

        public static void Register(Action onEvent)
        {
            mOnEvent += onEvent;
        }

        public static void Unregister(Action onEvent)
        {
            if (mOnEvent != null)
            {
                mOnEvent -= onEvent;
            }
        }

        public static void Trigger()
        {
            mOnEvent?.Invoke();
        }
    }
}
