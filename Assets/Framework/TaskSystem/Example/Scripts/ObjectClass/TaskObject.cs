using System;
using UnityEngine;

namespace KLFramework.TaskSyatem
{
    public enum RewardType
    {
        Gold,
        Diamond,
        Power
    }

    [Serializable]
    public struct Reward
    {
       public RewardType type;

       public int num;
    }

    [CreateAssetMenu]
    public class TaskObject : ScriptableObject
    {
        //链id，每个任务都有它对应的链id，同一条链上的任务的链id相同
        public int task_chain_id;

        //任务id，它是链上的任务id，不同链的任务id可以重复，从1开始往下自增
        public int task_sub_id;

        //任务图标
        public string icon;

        //任务描述，这个会显示到界面中
        public string desc;

        //任务目标，定义一个字符串来表示任务的目标类别，
        //比如加班5次和加班10次的任务目标是一样的，只是数量不同，
        //同理，写博客5篇和写博客100篇的任务目标也是一样的
        public string task_target;

        //目标数量，比如加班5次的目标数量就是5，
        //写博客100篇的目标数量就是100
        public int target_amount;

        //奖励
        public string award;

        public string open_chain;
    }
}
