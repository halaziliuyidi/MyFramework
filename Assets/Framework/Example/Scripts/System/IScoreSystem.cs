using UnityEngine;
namespace FrameworkDesign.Example
{
    public interface IScoreSystem : ISystem
    {
    }


    public class ScoreSystem : AbstractSystem, IScoreSystem
    {
        protected override void OnInit()
        {
            var gameModel = this.GetModel<IGameModel>();

            this.RegisterEvent<GamePassEvent>(e =>
            {
                Debug.Log($"new score：{gameModel.Score.Value},best score：{gameModel.BestScore.Value}");

                if (gameModel.Score.Value > gameModel.BestScore.Value)
                {
                    gameModel.BestScore.Value = gameModel.Score.Value;
                    Debug.Log("new higher score");
                }
            });

            this.RegisterEvent<OnEnemyKillEvent>(e =>
            {
                gameModel.Score.Value += 10;
                Debug.Log($"Add score,now score {gameModel.Score.Value}");
            });

            this.RegisterEvent<OnMissEvent>(e =>
            {
                gameModel.Score.Value -= 5;
                Debug.Log($"Reduce Score,now score {gameModel.Score.Value}");
            });
        }
    }
}
