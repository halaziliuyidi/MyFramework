using UnityEngine;

namespace FrameworkDesign.CounterApp
{
    public interface IAchievementSystem:ISystem
    {

    }

    public class AchievementSystem : AbstractSystem, IAchievementSystem
    {
        protected override void OnInit()
        {
            var counterModel = this.GetModel<ICounterModel>();

            var previousCount = counterModel.Count.Value;

            counterModel.Count.OnValueChanged += newCount =>
            {
                if (previousCount < 10 && newCount >= 10)
                {
                    Debug.Log("完成点击10次成就");
                }
                else if (previousCount < 20 && newCount >= 20)
                {
                    Debug.Log("完成点击20次成就");
                }

                previousCount = newCount;
            };
        }
    }
}
