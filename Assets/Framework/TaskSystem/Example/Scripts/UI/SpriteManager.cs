using System.Collections.Generic;
using UnityEngine;

namespace KLFramework.TaskSyatem
{
    internal class SpriteManager
    {
        /// <summary>
        /// 根据名字加载精灵资源
        /// </summary>
        public Sprite GetSprite(string name)
        {
            if (m_sprites.ContainsKey(name))
                return m_sprites[name];
            var sprite = Resources.Load<Sprite>("Sprites/" + name);
            m_sprites.Add(name, sprite);
            return sprite;
        }


        private Dictionary<string, Sprite> m_sprites = new Dictionary<string, Sprite>();
        private static SpriteManager s_instance;
        public static SpriteManager instance
        {
            get
            {
                if (null == s_instance)
                    s_instance = new SpriteManager();
                return s_instance;
            }
        }
    }
}
