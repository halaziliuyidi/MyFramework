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
            // 加载任务配置
            TaskConfig.Instance.LoadConfig();
            // 获取任务数据
            TaskLogic.Instance.GetTaskData(() =>
            {
                // 显示任务界面
                TaskPanel.Show();
            });
        }
    }
}
