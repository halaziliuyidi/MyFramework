using UnityEngine;

namespace KLFramework.UIFramework
{
    public abstract class BasePanel : MonoBehaviour
    {
        /// <summary>
        /// ���汻��ʾ����
        /// </summary>
        public virtual void OnEnter()
        {

        }

        /// <summary>
        /// ������ͣ
        /// </summary>

        public virtual void OnPause()
        {

        }

        /// <summary>
        /// �������
        /// </summary>
        public virtual void OnResume()
        {

        }

        /// <summary>
        /// ���治��ʾ
        /// </summary>
        public virtual void OnExit()
        {

        }
    }
}
