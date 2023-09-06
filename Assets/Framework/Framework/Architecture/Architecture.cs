using KLFramework.Example;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KLFramework
{
    public interface IArchitecture
    {
        void RegisterSystem<T>(T instance) where T : ISystem;

        /// <summary>
        /// 注册Model
        /// </summary>
        void RegisterModel<T>(T instance) where T : IModel;

        /// <summary>
        /// 注册Utility
        /// </summary>
        void RegisterUtility<T>(T instance) where T : IUtility;

        /// <summary>
        /// 获取系统层
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T GetSystem<T>() where T : class, ISystem;

        /// <summary>
        /// 获取数据层
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T GetModel<T>() where T : class, IModel;

        /// <summary>
        /// 获取工具
        /// </summary>
        T GetUtility<T>() where T : class, IUtility;

        void SendCommand<T>() where T : ICommand, new();

        void SendCommand<T>(T command) where T : ICommand;

        void SendEvent<T>() where T : new();

        void SendEvent<T>(T e);

        IUnRegister RegisterEvent<T>(Action<T> onEvent);

        void UnRegisterEvent<T>(Action<T> onEvent);
    }

    public abstract class Architecture<T> : IArchitecture where T : Architecture<T>, new()
    {
        /// <summary>
        /// 是否初始化完成
        /// </summary>
        private bool mInited = false;

        private List<IModel> mModels = new List<IModel>();

        private List<ISystem> mSystems = new List<ISystem>();

        public static Action<T> OnRegisterPatch = architecture => { };

        private static T mArchitecture = null;

        public static IArchitecture Interface
        {
            get
            {
                if (mArchitecture == null)
                {
                    MakeSureArchitecture();
                }

                return mArchitecture;
            }
        }

        static void MakeSureArchitecture()
        {
            if (mArchitecture == null)
            {
                mArchitecture = new T();
                mArchitecture.Init();

                OnRegisterPatch?.Invoke(mArchitecture);

                //Model层初始化
                foreach (var architectureModel in mArchitecture.mModels)
                {
                    architectureModel.Init();
                }
                mArchitecture.mModels.Clear();

                //系统层初始化，系统层初始化应该排在Model层之后，系统层可能会依赖Model层的数据
                foreach (var architecturrSystem in mArchitecture.mSystems)
                {
                    architecturrSystem.Init();
                }
                mArchitecture.mSystems.Clear();

                mArchitecture.mInited = true;
            }
        }

        protected abstract void Init();



        private IOCContainer mContainer = new IOCContainer();


        //注册Model层API
        public void RegisterModel<T>(T model) where T : IModel
        {
            model.SetArchitecture(this);
            mContainer.Register<T>(model);
            if (!mInited)
            {
                mModels.Add(model);
            }
            else
            {
                model.Init();
            }
        }

        public void RegisterUtility<T>(T utility) where T : IUtility
        {
            mContainer.Register<T>(utility);
        }

        public T GetUtility<T>() where T : class, IUtility
        {
            return mContainer.Get<T>();
        }

        //注册系统层API
        public void RegisterSystem<T>(T system) where T : ISystem
        {
            system.SetArchitecture(this);
            mContainer.Register<T>(system);
            if (!mInited)
            {
                mSystems.Add(system);
            }
            else
            {
                system.Init();
            }
        }

        public T GetSystem<T>() where T : class, ISystem
        {
            return mContainer.Get<T>();
        }

        //系统层获取Model
        public T GetModel<T>() where T : class, IModel
        {
            return mContainer.Get<T>();
        }

        public void SendCommand<T>() where T : ICommand, new()
        {
            var command = new T();
            command.SetArchitecture(this);
            command.Execute();
            command.SetArchitecture(null);
        }

        public void SendCommand<T>(T command) where T : ICommand
        {
            command.SetArchitecture(this);
            command.Execute();
            command.SetArchitecture(null);
        }

        private ITypeEventSystem mTypeEventSystem=new TypeEventSystem();

        public void SendEvent<T>() where T : new()
        {
            mTypeEventSystem.Send<T>();
        }

        public void SendEvent<T>(T e)
        {
            mTypeEventSystem.Send<T>(e);
        }

        public IUnRegister RegisterEvent<T>(Action<T> onEvent)
        {
            return mTypeEventSystem.Register<T>(onEvent);
        }

        public void UnRegisterEvent<T>(Action<T> onEvent)
        {
            mTypeEventSystem.UnRegister<T>(onEvent);
        }
    }
}
