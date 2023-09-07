using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace KLFramework.TaskSyatem
{
    public class QuestPanel : MonoBehaviour
    {
        public TextMeshProUGUI descriptionText;

        public Image rewardIcon;

        public Text rewardNumText;

        public Button CloseBtn;

        //测试使用，正式使用时应该在具体事件完成时使用
        public Button CompleteBtn;

        // Start is called before the first frame update
        void Start()
        {
            CloseBtn.onClick.AddListener(() =>
            {
                OnCloseBtnClick();
            });

            CompleteBtn.onClick.AddListener(() =>
            {
                OnCompleteBtnClick();
            });
        }

        void OnCloseBtnClick()
        {
            this.gameObject.SetActive(false);
        }

        void OnCompleteBtnClick()
        {

        }
    }
}
