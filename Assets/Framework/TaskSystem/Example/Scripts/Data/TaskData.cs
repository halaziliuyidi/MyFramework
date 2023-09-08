using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KLFramework.TaskSyatem
{
    public class TaskDataItem
    {
        //链Id
        public int task_chain_id;
        //任务子Id
        public int task_sub_id;
        //进度
        public int progress;
        //奖励是否被领取,1为已经领取，0为未领取
        public short award_is_get;
    }

    public class TaskData
    {
        public List<TaskDataItem> taskDatas
        {
            get
            {
                return mTaskDatas;
            }
        }

        private List<TaskDataItem> mTaskDatas;
        public TaskData()
        {
            mTaskDatas = new List<TaskDataItem>();
        }

        public void GetTaskDataFromDB(Action callBack)
        {
            var jsonStr = PlayerPrefs.GetString("TASK_DATA", "[{'task_chain_id':1,'task_sub_id':1,'progress':0,'award_is_get':0}]");
            var taskList = JsonMapper.ToObject<List<TaskDataItem>>(jsonStr);

            int taskCount = taskList.Count;
            for (int i = 0; i < taskCount; i++)
            {
                AddOrUpdateData(taskList[i]);
            }
            callBack();
        }

        public void AddOrUpdateData(TaskDataItem itemData)
        {
            bool isUpdate = false;

            int taskCount = mTaskDatas.Count;
            for (int i = 0; i < taskCount; i++)
            {
                var item = mTaskDatas[i];
                if (itemData.task_chain_id == item.task_chain_id
                    && itemData.task_sub_id == item.task_sub_id)
                {
                    mTaskDatas[i] = itemData;
                    isUpdate = true;
                    break;
                }
            }
            if (!isUpdate)
            {
                mTaskDatas.Add(itemData);
            }

            mTaskDatas.Sort((a, b) =>
            {
                return a.task_chain_id.CompareTo(b.task_chain_id);
            });

            SaveDataToDB();
        }

        public TaskDataItem GetData(int chainId, int subId)
        {
            for (int i = 0; i < mTaskDatas.Count; i++)
            {
                var item = mTaskDatas[i];
                if (chainId == item.task_chain_id && subId == item.task_sub_id)
                {
                    return item;
                }
            }
            return null;
        }

        public void RemoveData(int chainId, int subId)
        {
            for (int i = 0; i < mTaskDatas.Count; i++)
            {
                var item = mTaskDatas[i];
                if (chainId == item.task_chain_id && subId == item.task_sub_id)
                {
                    mTaskDatas.Remove(item);
                    SaveDataToDB();
                    return;
                }
            }
        }

        private void SaveDataToDB()
        {
            var jsonStr = JsonMapper.ToJson(mTaskDatas);
            PlayerPrefs.SetString("TASK_DATA", jsonStr);
        }

        public void ResetData(Action callBack)
        {
            PlayerPrefs.DeleteKey("TASK_DATA");
            mTaskDatas.Clear();
            GetTaskDataFromDB(callBack);
        }
    }
}
