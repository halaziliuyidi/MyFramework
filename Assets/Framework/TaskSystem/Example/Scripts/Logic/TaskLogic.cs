
using LitJson;
using System;
using System.Collections.Generic;

namespace KLFramework.TaskSyatem
{
    public class TaskLogic
    {
        private static TaskLogic instance;

        public static TaskLogic Instance
        {
            get 
            {
                if (instance == null)
                {
                    instance = new TaskLogic();
                }
                return instance;
            }
        }

        private TaskData mTaskData;

        public List<TaskDataItem> taskDatas
        {
            get
            {
                return mTaskData.taskDatas;
            }
        }

        public TaskLogic()
        {
            mTaskData = new TaskData();
        }

        public void GetTaskData(Action cb)
        {
            mTaskData.GetTaskDataFromDB(cb);
        }

        /// <summary>
        /// �����������
        /// </summary>
        /// <param name="chainId">��Id</param>
        /// <param name="subId">������Id</param>
        /// <param name="deltaProgress">����������</param>
        /// <param name="cb">�ص�</param>
        public void AddProgress(int chainId, int subId, int deltaProgress, Action<int, bool> cb)
        {
            var data = mTaskData.GetData(chainId, subId);
            if (data != null)
            {
                data.progress += deltaProgress;

                mTaskData.AddOrUpdateData(data);

                var cfg = TaskConfig.Instance.GetTaskObject(data.task_chain_id, data.task_sub_id);
                if (cfg != null)
                {
                    cb(0, data.progress >= cfg.target_amount);
                }
                else
                {
                    cb(-1, false);
                }
            }
            else
            {
                cb(0, false);
            }
        }

        /// <summary>
        /// һ����ȡ���н���
        /// </summary>
        /// <param name="cb">�ص�</param>
        public void OneKeyGetAward(Action<int, string> cb)
        {
            int totalGold = 0;

            var tmpTaskDatas = new List<TaskDataItem>(mTaskData.taskDatas);

            for (int i = 0; i < tmpTaskDatas.Count; i++)
            {
                var oneTask = tmpTaskDatas[i];
                var cfg = TaskConfig.Instance.GetTaskObject(oneTask.task_chain_id, oneTask.task_sub_id);
                if (oneTask.progress >= cfg.target_amount && oneTask.award_is_get == 0)
                {
                    oneTask.award_is_get = 1;

                    mTaskData.AddOrUpdateData(oneTask);

                    var awardJd = JsonMapper.ToObject(cfg.award);

                    totalGold += int.Parse(awardJd["gold"].ToString());

                    GoNext(oneTask.task_chain_id, oneTask.task_sub_id);
                }
            }
            if (totalGold > 0)
            {
                JsonData totalData = new JsonData();
                totalData["gold"] = totalGold;
                cb(0, JsonMapper.ToJson(totalData));
            }
            else
            {
                cb(-1, null);
            }
        }

        /// <summary>
        /// ��ȡ������
        /// </summary>
        /// <param name="chainId"></param>
        /// <param name="subId"></param>
        /// <param name="cb"></param>
        public void GetAward(int chainId, int subId, Action<int, string> cb)
        {
            var data = mTaskData.GetData(chainId, subId);

            if (data == null)
            {
                cb(-1, "{}");
                return;
            }
            //�жϵ�ǰ�����Ƿ��Ѿ���ȡ
            if (data.award_is_get == 0)
            {
                //δ��ȡ
                data.award_is_get = 1;
                mTaskData.AddOrUpdateData(data);
                GoNext(chainId, subId);
                var cfg = TaskConfig.Instance.GetTaskObject(data.task_chain_id, data.task_sub_id);
                cb(0, cfg.award);
            }
            else
            {
                //�Ѿ���ȡ
                cb(-2, "{}");
            }
        }

        /// <summary>
        /// ������һ�����񣬲�����������
        /// </summary>
        /// <param name="task_chain_id">��Id</param>
        /// <param name="task_sub_id">������Id</param>
        private void GoNext(int chainId, int subId)
        {
            var data = mTaskData.GetData(chainId, subId);
            var cfg = TaskConfig.Instance.GetTaskObject(data.task_chain_id, data.task_sub_id);
            var nextCfg = TaskConfig.Instance.GetTaskObject(data.task_chain_id, data.task_sub_id + 1);

            if (data.award_is_get == 1)
            {
                mTaskData.RemoveData(chainId, subId);

                if (nextCfg != null)
                {
                    TaskDataItem dataItem = new TaskDataItem();
                    dataItem.task_chain_id = nextCfg.task_chain_id;
                    dataItem.task_sub_id = nextCfg.task_sub_id;
                    dataItem.progress = 0;
                    dataItem.award_is_get = 0;
                    mTaskData.AddOrUpdateData(dataItem);
                }

                if (!string.IsNullOrEmpty(cfg.open_chain))
                {
                    var chains=cfg.open_chain.Split(',');
                    for (int i = 0; i < chains.Length; i++)
                    {
                        var task = chains[i].Split("|");
                        TaskDataItem subChainDataItem = new TaskDataItem();
                        subChainDataItem.task_chain_id = int.Parse(task[0]);
                        subChainDataItem.task_sub_id = int.Parse(task[1]);
                        subChainDataItem.progress = 0;
                        subChainDataItem.award_is_get = 0;
                        mTaskData.AddOrUpdateData(subChainDataItem);
                    }
                }
            }
        }

        public void ResetAll(Action cb)
        {
            mTaskData.ResetData(cb);
        }
    }
}
