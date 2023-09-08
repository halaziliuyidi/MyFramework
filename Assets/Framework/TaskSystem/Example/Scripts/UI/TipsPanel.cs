using System;
using UnityEngine;
using UnityEngine.UI;

namespace KLFramework.TaskSyatem
{
    public class TipsPanel:MonoBehaviour
    {
        public Button closeBtn;
        public Button addProgressBtn;
        public Button onekeyBtn;

        private int m_taskChainId;
        private int m_tasksubId;
        private Action updateTaskDataCb;

        private static GameObject s_tipsPanelPrefab;
        // 显示任务界面
        public static void Show(int chainId, int subId, Action cb)
        {
            if (null == s_tipsPanelPrefab)
                s_tipsPanelPrefab = Resources.Load<GameObject>("TipsPanel");
            var panelObj = Instantiate(s_tipsPanelPrefab);
            panelObj.transform.SetParent(GlobalObj.s_canvasTrans, false);
            var panelBhv = panelObj.GetComponent<TipsPanel>();
            panelBhv.Init(chainId, subId, cb);
        }

        public void Init(int chainId, int subId, Action cb)
        {
            m_taskChainId = chainId;
            m_tasksubId = subId;
            updateTaskDataCb = cb;
        }

        void Start()
        {
            // 关闭按钮
            closeBtn.onClick.AddListener(() =>
            {
                Destroy(gameObject);
            });

            // 进度+1
            addProgressBtn.onClick.AddListener(() =>
            {
                Destroy(gameObject);
                TaskLogic.Instance.AddProgress(m_taskChainId, m_tasksubId, 1, (errorCode, finishTask) =>
                {
                    updateTaskDataCb();
                });
            });

            // 一键完成
            onekeyBtn.onClick.AddListener(() =>
            {
                Destroy(gameObject);
                var cfg = TaskConfig.Instance.GetTaskObject(m_taskChainId, m_tasksubId);
                TaskLogic.Instance.AddProgress(m_taskChainId, m_tasksubId, cfg.target_amount, (errorCode, finishTask) =>
                {
                    updateTaskDataCb();
                });
            });
        }
    }
}
