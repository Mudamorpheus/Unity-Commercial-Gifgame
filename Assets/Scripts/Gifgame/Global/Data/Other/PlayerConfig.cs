using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Newtonsoft.Json;

using DateTime = System.DateTime;
using TimeSpan = System.TimeSpan;

namespace LTG.GifGame
{
    [System.Serializable]
    public class ConfigData
    {
        private string catalog;
        public string Catalog { set { catalog = value; } get { return catalog; } }

        private string version;
        public string Version { set { version = value; } get { return version; }  }

        private DateTime update;
        public DateTime Update { set { update = value; } get { return update; } }

        private List<LevelData> levels = new List<LevelData>();
        public List<LevelData> Levels { set { levels = value; } get { return levels; } }
    }

    [System.Serializable]
    public class PlayerConfig
    {
        //==================================
        //=========CONFIG
        //==================================

        //Config
        private List<ConfigData> playerConfigs = new List<ConfigData>();
        public List<ConfigData> PlayerConfigs { set { playerConfigs = value; } get { return playerConfigs; } }

        public void Reset()
        {
            playerConfigs.Clear();
        }

        //==================================
        //=========CATALOG
        //==================================

        //Catalog expiration in days
        [SerializeField]
        private int catalogExpirationDays = 1;

        //Catalog validation
        public bool IsCatalogVerified(string catalog)
        {
            //Find
            var config = PlayerConfigs.Find(cf => cf.Catalog == catalog);

            //Config
            if (config != null)
            {
                //Not found
                if (config == null)
                {
                    return false;
                }

                //Date
                DateTime now = DateTime.Now;
                DateTime update = config.Update;
                TimeSpan difference = now.Subtract(update);

                //Difference
                if (difference.TotalDays >= catalogExpirationDays)
                {
                    if (config.Version == MasterProfile.GameSettings.GameVersion)
                    {
                        //Catalog is fine
                        Debug.Log("Player config: Catalog is verified, version match.");
                        return true;
                    }
                    else
                    {
                        //Catalog has version mismatch  
                        Debug.Log("Player config: Catalog is not verified, version mismatch.");
                        return false;
                    }
                }
                else
                {
                    //Catalog is fine
                    Debug.Log("Player config: Catalog is verified, period not expired.");
                    return true;
                }
            }

            //Catalog does not exist
            Debug.Log("Player config: Catalog is not verified, catalog does not exist.");
            return false;
        }

        //Add catalog
        public void AddCatalog(string catalog, string version, List<LevelData> levels)
        {
            //New
            var config = new ConfigData
            {
                Catalog = catalog,
                Version = version,
                Update = DateTime.Now
            };

            //Levels
            for (int i = 0; i < levels.Count; i++)
            {
                //Level
                LevelData level = levels[i];

                //Add config level
                config.Levels.Add(level);
            }

            //Json
            string serialize = JsonConvert.SerializeObject(config);

            //List
            PlayerConfigs.Add(config);

            //Debug
            Debug.Log("Player config: Initializing new catalog config - " + serialize);
        }

        //Add level
        public void AddLevel(LevelData level)
        {
            //Find
            var catalog = level.Catalog;
            var config = PlayerConfigs.Find(cf => cf.Catalog == catalog);
            var check = config.Levels.Contains(level);

            //Check
            if(config != null && !check)
            {
                config.Levels.Add(level);
            }
        }
        public void AddLevel(string key)
        {
            //Find
            var level = LevelsContent.Find(key);
            if(level != null)
            {
                var catalog = level.Catalog;
                var config = PlayerConfigs.Find(cf => cf.Catalog == catalog);

                //Check
                if (config != null)
                {
                    config.Levels.Add(level);
                }
            }            
        }
        
        //==================================
        //=========SAVELOAD
        //==================================

        //Format
        private static string KeyConfigFormat(string name, string catalog)
        {
            return "GifGame" + "-" + name + "-" + catalog + "-" + "Config";
        }

        //Save catalog config
        public void SaveConfigJson(string catalog)
        {
            //Find
            var config = PlayerConfigs.Find(cf => cf.Catalog == catalog);

            //Process
            if (config != null)
            {
                //Key
                string name = PlayerProfile.PlayerName;
                string key = KeyConfigFormat(name, catalog);

                //Profile
                string json = JsonConvert.SerializeObject(config);
                PlayerPrefs.SetString(key, json);

                //Save
                PlayerPrefs.Save();

                //Debug
                Debug.Log("Player config: Catalog config is saved - " + json);
            }
        }
        public void SaveConfigsJson()
        {
            foreach (var config in PlayerConfigs)
            {
                //Process
                if (config != null)
                {
                    //Key
                    string name = PlayerProfile.PlayerName;
                    string catalog = config.Catalog;
                    string key = KeyConfigFormat(name, catalog);

                    //Profile
                    string json = JsonConvert.SerializeObject(config);
                    PlayerPrefs.SetString(key, json);

                    //Save
                    PlayerPrefs.Save();

                    //Debug
                    Debug.Log("Player config: Catalog config is saved - " + json);
                }
            }
        }

        //Load catalog config
        public bool LoadConfigJson(string catalog)
        {
            //Key
            string name = PlayerProfile.PlayerName;
            string key = KeyConfigFormat(name, catalog);

            //Check data
            ConfigData config = null;
            if (PlayerPrefs.HasKey(key))
            {
                //Json                
                string json = PlayerPrefs.GetString(key);
                config = JsonConvert.DeserializeObject<ConfigData>(json);
            }
            
            //Exist
            if (config == null)
            {
                //Debug
                Debug.Log("Player config: No catalog config.");
                return false;
            }
            else
            {
                //List
                PlayerConfigs.Add(config);

                //Json
                string serialize = JsonConvert.SerializeObject(config);
                Debug.Log("Player config: Catalog config is loaded - " + serialize);
                return true;
            }
        }
    }
}
