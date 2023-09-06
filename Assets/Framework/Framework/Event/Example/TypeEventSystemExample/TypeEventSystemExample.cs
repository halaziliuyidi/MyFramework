using System;
using UnityEngine;

namespace FrameworkDesign.Example
{
    public class TypeEventSystemExample : MonoBehaviour
    {
        public struct EventA
        {

        }

        public struct EventB
        {
            public int ParamB;
        }

        public interface IEventGroup
        {

        }

        public struct EventC : IEventGroup
        {

        }

        public struct EventD : IEventGroup
        {

        }

        private TypeEventSystem mTypeEventSystem = new TypeEventSystem();

        private void Start()
        {
            mTypeEventSystem.Register<EventA>(OnEvent);
            mTypeEventSystem.Register<EventB>(b =>
            {
                Debug.Log($"OnEventB:{b.ParamB}");
            }).UnRegisterWhenGameObjectDestroyed(gameObject);


            mTypeEventSystem.Register<IEventGroup>(e =>
            {
                Debug.Log($"event type:{e.GetType()}");
            }).UnRegisterWhenGameObjectDestroyed(gameObject);
        }

        private void OnEvent(EventA obj)
        {
            Debug.Log("OnEventA");
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                mTypeEventSystem.Send<EventA>();
            }
            if (Input.GetMouseButtonDown(1))
            {
                mTypeEventSystem.Send(new EventB()
                {
                    ParamB = 2
                }); ;
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                mTypeEventSystem.Send<IEventGroup>(new EventC());
                mTypeEventSystem.Send<IEventGroup>(new EventD());
            }
        }

        private void OnDestroy()
        {
            mTypeEventSystem.UnRegister<EventA>(OnEvent);
            mTypeEventSystem = null;
        }
    }
}
