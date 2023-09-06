using System.Collections.Generic;
using UnityEngine;

namespace KLFramework.UIFramework
{
    public class UIManager
    {
        private static UIManager instance;

        public static UIManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new UIManager();
                }
                return instance;
            }
        }

        private Transform canvasTransform;

        private Transform CanvasTransform
        {
            get
            {
                if (canvasTransform == null)
                {
                    canvasTransform = GameObject.Find("Canvas").transform;
                }
                return canvasTransform;
            }
        }

        #region
        public Dictionary<UIPanelType, string> panelPathDict;//UI各个界面Prefab的路径

        public Dictionary<UIPanelType, BasePanel> panelDict;//UI面板的实体组件

        private Stack<BasePanel> panelStack;
        #endregion

        public UIManager()
        {
            ParseUIPanelTypeJson();
        }

        public void PushPanel(UIPanelType panelType)
        {
            if (panelStack == null)
            {
                panelStack = new Stack<BasePanel>();
            }

            if (panelStack.Count > 0)
            {
                BasePanel topPanel = panelStack.Peek();
                topPanel.OnPause();
            }

            BasePanel panel = GetPanel(panelType);

            panel.OnEnter();

            panelStack.Push(panel);
        }

        public void PopPanel()
        {
            if (panelStack == null)
            {
                panelStack = new Stack<BasePanel>();
            }
            if (panelStack.Count <= 0)
            {
                return;
            }

            BasePanel topPanel = panelStack.Pop();

            topPanel.OnExit();

            if (panelStack.Count <= 0)
            {
                return;
            }

            BasePanel basePanel= panelStack.Peek();
            basePanel.OnExit();
        }

        private BasePanel GetPanel(UIPanelType panelType)
        {
            if (panelDict == null)
            {
                panelDict=new Dictionary<UIPanelType, BasePanel> ();
            }

            BasePanel panel = panelDict.TryGet(panelType);

            if (panel == null)
            {
                string path = panelPathDict.TryGet(panelType);
                var panelObject = Resources.Load(path);
                GameObject newPanel = UnityEngine.Object.Instantiate(panelObject) as GameObject;
                BasePanel basePanel = newPanel.GetComponent<BasePanel>();
                panelDict.Add(panelType, basePanel);
                panelObject = null;
                Resources.UnloadUnusedAssets();
                return basePanel;
            }
            else
            {
                return panel;
            }
        }

        class UIPanelTypeJson
        {
            public List<UIPanelInfo> infoList;
        }

        private void ParseUIPanelTypeJson()
        {
            panelPathDict = new Dictionary<UIPanelType, string>();
            TextAsset typeAsset = Resources.Load<TextAsset>("UIPanelType");

            UIPanelTypeJson jsonObject = JsonUtility.FromJson<UIPanelTypeJson>(typeAsset.text);

            foreach (UIPanelInfo info in jsonObject.infoList)
            {
                panelPathDict.Add(info.panelType,info.path);
            }
        }
    }
}
