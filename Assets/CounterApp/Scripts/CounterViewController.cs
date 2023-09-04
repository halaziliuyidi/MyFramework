using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FrameworkDesign.CounterApp
{
    public class CounterViewController : MonoBehaviour,IController
    {
        private ICounterModel mCounterModel;

        private void Start()
        {
            mCounterModel = this.GetModel<ICounterModel>();

            mCounterModel.Count.OnValueChanged += OnCountChanged;

            OnCountChanged(mCounterModel.Count.Value);

            transform.Find("AddBtn").GetComponent<Button>().onClick.AddListener(() =>
            {
                //½»»¥Âß¼­
                this.SendCommand<AddCountCommand>();
            });

            transform.Find("SubBtn").GetComponent<Button>().onClick.AddListener(() =>
            {
                //½»»¥Âß¼­
                this.SendCommand<SubCountCommand>();
            });
        }

        void OnCountChanged(int newCount)
        {
            transform.Find("CountText").GetComponent<TextMeshProUGUI>().text = newCount.ToString();
        }

        void OnDestroy()
        {
            mCounterModel.Count.OnValueChanged -= OnCountChanged;
            mCounterModel = null;
        }

         IArchitecture IBelongToArchitecture.GetArchitecture()
        {
            return CounterApp.Interface;
        }
    }
}
