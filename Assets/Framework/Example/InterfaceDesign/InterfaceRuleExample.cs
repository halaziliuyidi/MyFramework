using UnityEngine;

namespace FrameworkDesign.Exmple
{
    public class CanDoEverything
    {
        public void DowSomething1()
        {
            Debug.Log("Do Something1");
        }

        public void DowSomething2()
        {
            Debug.Log("Do Something2");
        }

        public void DowSomething3()
        {
            Debug.Log("Do Something3");
        }
    }

    public interface IHasEverything
    {
        CanDoEverything CanDoEverything { get;}
    }

    public interface ICanDoSomething1 : IHasEverything
    {

    }

    public static class ICandoSomething1Extension
    {
        public static void DoSomething1(this ICanDoSomething1 self)
        {
            self.CanDoEverything.DowSomething1();
        }
    }

    public interface ICanDoSomething2 : IHasEverything
    {

    }

    public static class ICandoSomething2Extension
    {
        public static void DoSomething2(this ICanDoSomething2 self)
        {
            self.CanDoEverything.DowSomething2();
        }
    }

    public interface ICanDoSomething3 : IHasEverything
    {

    }

    public static class ICandoSomething3Extension
    {
        public static void DoSomething3(this ICanDoSomething3 self)
        {
            self.CanDoEverything.DowSomething3();
        }
    }

    public class InterfaceRuleExample : MonoBehaviour
    {

        public class OnlyCanDo1 : ICanDoSomething1
        {
            CanDoEverything IHasEverything.CanDoEverything { get; } = new CanDoEverything();
        }

        public class OnlyCanDo23 : ICanDoSomething2, ICanDoSomething3
        {
            CanDoEverything IHasEverything.CanDoEverything { get; } = new CanDoEverything();
        }

        private void Start()
        {
            var onlyCanDo1 = new OnlyCanDo1();

            onlyCanDo1.DoSomething1();

            var onlyCanDo23=new OnlyCanDo23();
            onlyCanDo23.DoSomething2();
            onlyCanDo23.DoSomething3();
        }
    }
}
