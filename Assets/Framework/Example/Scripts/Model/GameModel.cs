namespace KLFramework.Example
{
    public interface IGameModel : IModel
    {
        public BindableProperty<int> KillCount { get; }

        public BindableProperty<int> BestScore { get; }

        public BindableProperty<int> Score { get; }

        public BindableProperty<int> Gold { get; }
    }

    public enum GameState
    {

    }

    public class GameModel : AbstractModel, IGameModel
    {
        public BindableProperty<int> KillCount { get; } = new BindableProperty<int>()
        {
            Value = 0,
        };

        public BindableProperty<int> BestScore { get; } = new BindableProperty<int>()
        {
            Value = 0,
        };

        public BindableProperty<int> Score { get; } = new BindableProperty<int>()
        {
            Value = 0,
        };

        public BindableProperty<int> Gold { get; } = new BindableProperty<int>()
        {
            Value = 0,
        };

        protected override void OnInit()
        {
            var storage = this.GetUtility<IStorage>();

            BestScore.Value = storage.LoadInt(nameof(BestScore), 0);

            BestScore.RegisterOnValueChanged(v =>
            { 
                storage.SaveInt(nameof(BestScore), v); 
            });
        }
    }
}
