
namespace KLFramework.Example
{
    public class PointGame : Architecture<PointGame>
    {
        protected override void Init()
        {
            RegisterModel<IGameModel>(new GameModel());

            RegisterSystem<IScoreSystem>(new ScoreSystem());

            RegisterUtility<IStorage>(new PlayerPrefsStorage());
        }
    }
}
