using System.Collections.Generic;

namespace KLFramework.UIFramework
{
    /// <summary>
    /// 字典扩展
    /// </summary>
    public static class DictionaryExtension
    {
        /// <summary>
        /// 根据key获取value.存在返回value，不存在返回null
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="Tvalue"></typeparam>
        /// <param name="dic"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static Tvalue TryGet<TKey, Tvalue>(this Dictionary<TKey, Tvalue> dic, TKey key)
        {
            Tvalue tvalue;
            dic.TryGetValue(key, out tvalue);
            return tvalue;
        }
    }
}
