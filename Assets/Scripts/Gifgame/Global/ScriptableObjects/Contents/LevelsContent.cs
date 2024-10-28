using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.AddressableAssets.ResourceLocators;
using UnityEngine.ResourceManagement.ResourceLocations;

namespace LTG.GifGame
{
    [System.Serializable]
    public class LevelData
    {
        [SerializeField]
        private string key = "";
        public string Key { set { key = value; } get { return key; } }

        [SerializeField]
        private string animation = "";
        public string Animation { set { animation = value; } get { return animation; } }

        [SerializeField]
        private string hitbox = "";
        public string Hitbox { set { hitbox = value; } get { return hitbox; } }

        [SerializeField]
        private string catalog = "";
        public string Catalog { set { catalog = value; } get { return catalog; } }
    }

    [CreateAssetMenu(menuName = "Singletons/Contents/Levels")]
    public class LevelsContent : Technical.SingletonScriptableObject<LevelsContent>
    {
        //==================================
        //=========CONTENT
        //==================================

        //Init levels content
        public static void Init()
        {
            Levels = new List<LevelData>();
        }

        //Init levels content
        public static void Flush()
        {
            Levels = null;
        }

        //Init levels content
        public static void Reset()
        {
            Levels.Clear();
        }

        //==================================
        //=========LEVELS
        //==================================

        //List
        [SerializeField]
        private List<LevelData> levels;
        static public List<LevelData> Levels { set { Instance.levels = value; } get { return Instance.levels; } }

        //Find
        public static LevelData Find(string key)
        {
            return Levels.Find(lvl => lvl.Key == key);
        }

        //Create
        public static void Create(string key, string animation, string hitbox)
        {
            LevelData level = new LevelData
            {
                Key = key,
                Animation = animation,
                Hitbox = hitbox
            };

            Levels.Add(level);
        }

        //Add
        public static void Add(LevelData data)
        {
            Levels.Add(data);
        }

        //Random
        public static LevelData GetRandomLevel()
        {
            if (Levels == null || Levels.Count == 0)
            {
                return null;
            }
            else
            {
                LevelData level;
                var count = PlayerProfile.PlayerProgress.AfterMax-1;
                var exclude = PlayerProfile.PlayerProgress.GetLastLevels(count);

                do
                {
                    int index = Random.Range(0, Levels.Count);
                    level = Levels[index];
                } while (exclude.Contains(level));                                          

                return level;
            }            
        }
        public static LevelData GetRandomLoadedLevel()
        {
            var levels = new List<LevelData>();

            //Levels
            foreach (var config in PlayerProfile.PlayerConfig.PlayerConfigs)
            {
                levels.AddRange(config.Levels);
            }

            //Check
            if (levels == null || levels.Count == 0)
            {
                return null;
            }
            else
            {
                LevelData level;
                var count = PlayerProfile.PlayerProgress.AfterMax-1;
                var exclude = PlayerProfile.PlayerProgress.GetLastLevels(count);

                do
                {
                    int index = Random.Range(0, Levels.Count);
                    level = levels[index];
                } while (exclude.Contains(level));

                return level;
            }
        }
    }
}