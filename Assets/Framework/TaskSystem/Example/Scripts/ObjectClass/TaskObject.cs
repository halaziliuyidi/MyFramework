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
        //��id��ÿ������������Ӧ����id��ͬһ�����ϵ��������id��ͬ
        public int task_chain_id;

        //����id���������ϵ�����id����ͬ��������id�����ظ�����1��ʼ��������
        public int task_sub_id;

        //����ͼ��
        public string icon;

        //�����������������ʾ��������
        public string desc;

        //����Ŀ�꣬����һ���ַ�������ʾ�����Ŀ�����
        //����Ӱ�5�κͼӰ�10�ε�����Ŀ����һ���ģ�ֻ��������ͬ��
        //ͬ��д����5ƪ��д����100ƪ������Ŀ��Ҳ��һ����
        public string task_target;

        //Ŀ������������Ӱ�5�ε�Ŀ����������5��
        //д����100ƪ��Ŀ����������100
        public int target_amount;

        //����
        public string award;

        public string open_chain;
    }
}
