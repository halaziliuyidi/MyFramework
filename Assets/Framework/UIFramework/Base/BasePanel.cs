using UnityEngine;

namespace KLFramework.UIFramework
{
    public abstract class BasePanel : MonoBehaviour
    {
        /// <summary>
        /// 界面被显示出来
        /// </summary>
        public virtual void OnEnter()
        {

        }

        /// <summary>
        /// 界面暂停
        /// </summary>

        public virtual void OnPause()
        {

        }

        /// <summary>
        /// 界面继续
        /// </summary>
        public virtual void OnResume()
        {

        }

        /// <summary>
        /// 界面不显示
        /// </summary>
        public virtual void OnExit()
        {

        }
    }
}
