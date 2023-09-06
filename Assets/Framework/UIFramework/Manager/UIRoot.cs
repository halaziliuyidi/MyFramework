using UnityEngine;

namespace KLFramework.UIFramework
{
    public class UIRoot:MonoBehaviour
    {
        void Start ()
        {
            UIManager.Instance.PushPanel(UIPanelType.MainMenu);
        }
    }
}
