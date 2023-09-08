using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace KLFramework.TaskSyatem
{
    public class TaskItemUI : RecyclingListViewItem
    {
        public Image taskTypeImage;

        public Image taskIconImage;

        public TextMeshProUGUI descriptionText;

        public TextMeshProUGUI progressText;

        public Image progressImage;

        public TextMeshProUGUI awardNumText;

        public Button goAheadBtn;

        public Button getAwardBtn;

        public Action updateListCb;

        public void UpdateUI(TaskDataItem data)
        {
            var cfg = TaskConfig.Instance.GetTaskObject(data.task_chain_id, data.task_sub_id);
            if (cfg != null)
            {
                descriptionText.text = cfg.desc;

                var taskTypeSpriteName = ((cfg.task_chain_id == 1) ? "zhu" : "zhi");

                taskTypeImage.sprite = SpriteManager.instance.GetSprite(taskTypeSpriteName);

                progressText.text = $"{data.progress}/{cfg.target_amount}";

                progressImage.fillAmount= (float)data.progress / cfg.target_amount;

                goAheadBtn.onClick.RemoveAllListeners();

                goAheadBtn.onClick.AddListener(() =>
                {
                    TipsPanel.Show(data.task_chain_id, data.task_sub_id, () =>
                    {
                        UpdateUI(data);
                    });
                });

                getAwardBtn.onClick.RemoveAllListeners();

                getAwardBtn.onClick.AddListener(() =>
                {
                    TaskLogic.Instance.GetAward(data.task_chain_id,data.task_sub_id,(errorCode,award)=>
                    {
                        Debug.Log($"error code: {errorCode},award: {award}");
                        if (errorCode == 0)
                        {
                            AwardPanel.Show(award);
                            updateListCb();
                        }
                    });
                });

                goAheadBtn.gameObject.SetActive(data.progress < cfg.target_amount);
                getAwardBtn.gameObject.SetActive(data.progress >= cfg.target_amount && 0 == data.award_is_get);
            }
        }
    }
}
