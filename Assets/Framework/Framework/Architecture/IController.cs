
namespace KLFramework
{
    public interface IController : IBelongToArchitecture,ICanSendCommand,ICanGetSystem,ICanGetModel,ICanRegisterEvent
    {
    }
}
