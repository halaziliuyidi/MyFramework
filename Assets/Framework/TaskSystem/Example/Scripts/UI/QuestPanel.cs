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

        //����ʹ�ã���ʽʹ��ʱӦ���ھ����¼����ʱʹ��
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
