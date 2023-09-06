using KLFramework.CounterApp;
using UnityEngine;


namespace KLFramework.Example
{
    public class Enemy : MonoBehaviour,IController
    {
        IArchitecture IBelongToArchitecture.GetArchitecture()
        {
            return PointGame.Interface;
        }

        private void OnMouseDown()
        {
            Destroy(gameObject);
            this.SendCommand<KillEnemyCommand>();
        }
    }
}
