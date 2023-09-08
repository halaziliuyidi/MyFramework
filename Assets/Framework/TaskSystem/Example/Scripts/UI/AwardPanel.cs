using LitJson;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace KLFramework.TaskSyatem
{
    public class AwardPanel:MonoBehaviour
    {
        public TextMeshProUGUI goldText;
        public Button bgBtn;
        private GameObject m_selfGo;

        private static GameObject s_awardPanelPrefab;

        /// <summary>
        /// 显示奖励界面
        /// </summary>
        public static void Show(string award)
        {
            if (null == s_awardPanelPrefab)
                s_awardPanelPrefab = Resources.Load<GameObject>("AwardPanel");
            var panelObj = Instantiate(s_awardPanelPrefab);
            panelObj.transform.SetParent(GlobalObj.s_canvasTrans, false);
            var panelBhv = panelObj.GetComponent<AwardPanel>();
            panelBhv.Init(award);
        }

        public void Init(string award)
        {
            var jd = JsonMapper.ToObject(award);
            goldText.text = jd["gold"].ToString();
            bgBtn.onClick.AddListener(() =>
            {
                SelfDestroy();
            });

            // 3秒后自动销毁
            Invoke("SelfDestroy", 1.5f);
        }

        private void Awake()
        {
            m_selfGo = gameObject;
        }

        private void SelfDestroy()
        {
            if (null != m_selfGo)
            {
                Destroy(m_selfGo);
                m_selfGo = null;
            }
        }
    }
}
