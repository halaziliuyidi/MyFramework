using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;

namespace KLFramework.TaskSyatem
{
    public class TaskConfig : MonoBehaviour
    {
        private Dictionary<int, Dictionary<int, TaskObject>> mTaskConfig;

        public void LoadConfig()
        {
            mTaskConfig = new Dictionary<int, Dictionary<int, TaskObject>>();

            var configTxt = Resources.Load<TextAsset>("taskConfig").text;

            var jsonData = JsonMapper.ToObject<JsonData>(configTxt);

            int jsonCount = jsonData.Count;
            for (int i = 0; i < jsonCount; ++i)
            {
                var jsonObject = jsonData[i] as JsonData;

                TaskObject taskObject = JsonMapper.ToObject<TaskObject>(jsonObject.ToJson());

                if (!mTaskConfig.ContainsKey(taskObject.task_chain_id))
                {
                    mTaskConfig[taskObject.task_chain_id] = new Dictionary<int, TaskObject>();
                }
                mTaskConfig[taskObject.task_chain_id].Add(taskObject.task_sub_id, taskObject);
            }
        }

        public TaskObject GetTaskObject(int chainID,int taskSubId)
        {
            if (mTaskConfig.ContainsKey(chainID) && mTaskConfig[chainID].ContainsKey(taskSubId))
            {
                return mTaskConfig[chainID][taskSubId];
            }

            return null;
        }
    }
}
