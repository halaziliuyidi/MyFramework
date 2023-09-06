using System;
using UnityEngine;

namespace FrameworkDesign.Exmple
{
    public class Game : MonoBehaviour,IController
    {
        // Start is called before the first frame update
        void Awake()
        {
            this.RegisterEvent<GameStartEvent>(OnGameStart);
        }

        private void OnGameStart(GameStartEvent e)
        {
            transform.Find("Enemies").gameObject.SetActive(true);
        }

        private void OnDestroy()
        {
            this.UnRegisterEvent<GameStartEvent>(OnGameStart);
        }

        public IArchitecture GetArchitecture()
        {
            return PointGame.Interface;
        }
    }
}
