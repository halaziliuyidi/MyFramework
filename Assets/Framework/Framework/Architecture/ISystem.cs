
namespace KLFramework
{
    public interface ISystem : IBelongToArchitecture,ICanSetArchitecture,ICanGetModel,ICanRegisterEvent,ICanSendEvent,ICanGetSystem
    {
        void Init();
    }

    public abstract class AbstractSystem : ISystem
    {
        private IArchitecture mArchitecture;

        void ISystem.Init()
        {
            OnInit();
        }

        IArchitecture IBelongToArchitecture.GetArchitecture()
        {
            return mArchitecture;
        }


        void ICanSetArchitecture.SetArchitecture(IArchitecture architecture)
        {
            mArchitecture = architecture;
        }

        protected abstract void OnInit();
    }
}
