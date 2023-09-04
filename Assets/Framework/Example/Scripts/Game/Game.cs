using System;
using UnityEngine;

namespace FrameworkDesign.Exmple
{
    public class Game : MonoBehaviour
    {
        // Start is called before the first frame update
        void Awake()
        {
            GameStartEvent.Register(OnGameStart);
        }

        private void OnGameStart()
        {
            transform.Find("Enemies").gameObject.SetActive(true);
        }

        private void OnDestroy()
        {
            GameStartEvent.Unregister(OnGameStart);
        }
    }
}
