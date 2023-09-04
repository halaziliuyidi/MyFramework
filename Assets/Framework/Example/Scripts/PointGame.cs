
using FrameworkDesign.CounterApp;

namespace FrameworkDesign.Exmple
{
    public class PointGame : Architecture<PointGame>
    {
        protected override void Init()
        {
            Register<IGameModel>(new GameModel());
        }
    }
}
