using Unity.VisualScripting;
using UnityEngine;

namespace KLFramework.Example
{
    public class InterfaceStructExample:MonoBehaviour
    {
        public interface ICustomScript
        {
            public void Start();

            public void Update();

            public void Destroy();
        }

        public abstract class CustomScript : ICustomScript
        {
            void ICustomScript.Start()
            {
                OnStart();
            }

            void ICustomScript.Update()
            {
                OnUpdate();
            }

            void ICustomScript.Destroy()
            {
               OnDestroy();
            }

            protected abstract void OnStart();

            protected abstract void OnUpdate();

            protected abstract void OnDestroy();

        }

        public class MyScript : CustomScript
        {
            protected override void OnStart()
            {
                Debug.Log("On Start");
            }

            protected override void OnUpdate()
            {
                Debug.Log("On Update");
            }

            protected override void OnDestroy()
            {
                Debug.Log("On Destroy");
            }
        }

        private void Start()
        {
            ICustomScript customScript= new MyScript();
            customScript.Start();
            customScript.Update();
            customScript.Destroy();
        }
    }
}
