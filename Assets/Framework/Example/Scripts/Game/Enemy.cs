using FrameworkDesign.CounterApp;
using UnityEngine;


namespace FrameworkDesign.Exmple
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
