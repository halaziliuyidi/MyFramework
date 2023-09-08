using UnityEngine;
using UnityEngine.UI;

namespace KLFramework.TaskSyatem
{
    public class TaskPanel:MonoBehaviour
    {
        private static GameObject s_taskPanelPrefab;
        public RecyclingListView scrollList;
        public Button oneKeyGetAwardBtn;
        public Button resetAllDataBtn;

        // 显示任务界面
        public static void Show()
        {
            if (null == s_taskPanelPrefab)
                s_taskPanelPrefab = Resources.Load<GameObject>("TaskPanel");
            var panelObj = Instantiate(s_taskPanelPrefab);
            panelObj.transform.SetParent(GlobalObj.s_canvasTrans, false);
        }

        private void Start()
        {
            // 列表item更新回调
            scrollList.ItemCallback = PopulateItem;

            // 创建列表
            CreateList();

            oneKeyGetAwardBtn.onClick.AddListener(() =>
            {
                TaskLogic.Instance.OneKeyGetAward((errorCode, awards) =>
                {
                    if (0 == errorCode)
                    {
                        AwardPanel.Show(awards);
                        RefreshAll();
                    }
                });
            });

            resetAllDataBtn.onClick.AddListener(() =>
            {
                TaskLogic.Instance.ResetAll(() =>
                {
                    RefreshAll();
                });
            });
        }



        /// <summary>
        /// item更新回调
        /// </summary>
        /// <param name="item">复用的item对象</param>
        /// <param name="rowIndex">行号</param>
        private void PopulateItem(RecyclingListViewItem item, int rowIndex)
        {
            var child = item as TaskItemUI;
            child.UpdateUI(TaskLogic.Instance.taskDatas[rowIndex]);
            child.updateListCb = () =>
            {
                RefreshAll();
            };
        }

        /// <summary>
        /// 刷新整个列表
        /// </summary>
        private void RefreshAll()
        {
            scrollList.RowCount = TaskLogic.Instance.taskDatas.Count;
            scrollList.Refresh();
        }

        private void CreateList()
        {
            // 设置数据，此时列表会执行更新
            scrollList.RowCount = TaskLogic.Instance.taskDatas.Count;
        }

        
    }
}
