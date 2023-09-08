using System.Collections.Generic;
using UnityEngine;
using LitJson;

namespace KLFramework.TaskSyatem
{
    public class TaskConfig
    {
        private static TaskConfig instance;

        public static TaskConfig Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new TaskConfig();
                }
                return instance;
            }
        }

        // 任务配置，(链id : 子任务id : TaskObject)
        private Dictionary<int, Dictionary<int, TaskObject>> mTaskConfig;

        /// <summary>
        /// 读取配置
        /// </summary>
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

        /// <summary>
        /// 获取配置项
        /// </summary>
        /// <param name="chainId"></param>
        /// <param name="taskSubId"></param>
        /// <returns></returns>
        public TaskObject GetTaskObject(int chainId,int taskSubId)
        {
            if (mTaskConfig.ContainsKey(chainId) && mTaskConfig[chainId].ContainsKey(taskSubId))
            {
                return mTaskConfig[chainId][taskSubId];
            }
            return null;
        }
    }
}
