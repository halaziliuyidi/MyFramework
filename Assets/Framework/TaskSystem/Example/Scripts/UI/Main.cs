using UnityEngine;

namespace KLFramework.TaskSyatem
{
    public class GlobalObj
    {
        public static Transform s_canvasTrans;
    }
    public class Main : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            GlobalObj.s_canvasTrans = GameObject.Find("Canvas").transform;
            // ������������
            TaskConfig.Instance.LoadConfig();
            // ��ȡ��������
            TaskLogic.Instance.GetTaskData(() =>
            {
                // ��ʾ�������
                TaskPanel.Show();
            });
        }
    }
}
