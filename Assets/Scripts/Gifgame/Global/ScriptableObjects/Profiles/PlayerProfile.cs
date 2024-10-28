using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

using Newtonsoft.Json;

namespace LTG.GifGame
{
    [CreateAssetMenu(menuName = "Singletons/Profiles/Player")]
    public class PlayerProfile : Technical.SingletonScriptableObject<PlayerProfile>
    {
        //==================================
        //=========SETTINGS
        //==================================

        //Settings
        [SerializeField]
        private PlayerSettings playerSettings;
        public static PlayerSettings PlayerSettings { set { Instance.playerSettings = value; } get { return Instance.playerSettings; } }

        //==================================
        //=========CONFIG
        //==================================
        //Settings
        [SerializeField]
        private PlayerConfig playerConfig;
        public static PlayerConfig PlayerConfig { set { Instance.playerConfig = value; } get { return Instance.playerConfig; } }

        //==================================
        //=========PROGRESS
        //==================================
        //Settings
        [SerializeField]
        private PlayerProgress playerProgress;
        public static PlayerProgress PlayerProgress { set { Instance.playerProgress = value; } get { return Instance.playerProgress; } }

        //==================================
        //=========PROFILE
        //==================================

        //Name
        [SerializeField]
        private string playerName;
        public static string PlayerName { set { Instance.playerName = value; } get { return Instance.playerName; } }

        //Version
        [SerializeField]
        private string playerVersion;
        public static string PlayerVersion { set { Instance.playerVersion = value; } get { return Instance.playerVersion; } }

        //State
        public static bool Loaded { get { return PlayerName != ""; } }

        //Setup master data
        public static void Setup()
        {
            PlayerName = MasterProfile.GameSettings.GameDefault;
            PlayerVersion = MasterProfile.GameSettings.GameVersion;
        }

        //Init player profile
        public static void Init()
        {
            //Settings
            PlayerSettings = new PlayerSettings();

            //Progress
            PlayerProgress = new PlayerProgress();

            //Config
            PlayerConfig = new PlayerConfig();
        }

        //Flush player profile
        public static void Flush()
        {
            //Settings
            PlayerSettings = null;

            //Progress
            PlayerProgress = null;

            //Config
            PlayerConfig = null;

            //Shop
            PlayerTransition = null;
            PlayerPlayhead = null;
            PlayerLifebar = null;
        }

        //Reset player profile
        public static void Reset()
        {
            //Settings
            PlayerSettings.Reset();

            //Config
            PlayerConfig.Reset();

            //Progress
            PlayerProgress.Reset();
        }

        //==================================
        //=========SETGET
        //==================================

        //Player name
        public static void SetName(string name)
        {
            PlayerName = name;
        }

        //==================================
        //=========SAVELOAD
        //==================================

        //Format
        private static string KeySettingsFormat(string name)
        {
            return "GifGame" + "-" + name + "-" + "Profile";
        }
        private static string KeyProgressFormat(string name)
        {
            return "GifGame" + "-" + name + "-" + "Progress";
        }

        //Save player profile
        public static void SaveProfileJson()
        {
            //Keys
            string keySettings = KeySettingsFormat(PlayerName);
            string keyProgress = KeyProgressFormat(PlayerName);

            //Profile
            string jsonSettings = JsonConvert.SerializeObject(PlayerSettings);
            PlayerPrefs.SetString(keySettings, jsonSettings);
            string jsonProgress = JsonConvert.SerializeObject(PlayerProgress);
            PlayerPrefs.SetString(keyProgress, jsonProgress);

            //Debug
            Debug.Log("Player profile: Settings are saved - " + jsonSettings);
            Debug.Log("Player profile: Progress is saved - " + jsonProgress);

            //Save
            PlayerPrefs.Save();

            //Config
            PlayerConfig.SaveConfigsJson();
        }

        //Load player profile
        public static void LoadProfileJson()
        {
            //Initial data
            Setup();

            //Key
            string keySettings = KeySettingsFormat(PlayerName);
            string keyProgress = KeyProgressFormat(PlayerName);

            //Check data
            if (PlayerPrefs.HasKey(keySettings) && PlayerPrefs.HasKey(keyProgress))
            {
                //Default
                PlayerConfig = new PlayerConfig();

                //Json                
                string jsonSettings = PlayerPrefs.GetString(keySettings);
                PlayerSettings = JsonConvert.DeserializeObject<PlayerSettings>(jsonSettings);
                string jsonProgress = PlayerPrefs.GetString(keyProgress);
                PlayerProgress = JsonConvert.DeserializeObject<PlayerProgress>(jsonProgress);

                //Debug
                Debug.Log("Player profile: Settings are loaded - " + jsonSettings);
                Debug.Log("Player profile: Progress is loaded - " + jsonProgress);
            }
            else
            {
                string jsonSettings = JsonConvert.SerializeObject(PlayerSettings);
                PlayerPrefs.SetString(keySettings, jsonSettings);
                string jsonProgress = JsonConvert.SerializeObject(PlayerProgress);
                PlayerPrefs.SetString(keyProgress, jsonProgress);

                Init();
                Reset();

                //Debug
                Debug.Log("Player profile: Settings are inited - " + jsonSettings);
                Debug.Log("Player profile: Progress is inited - " + jsonProgress);
            }

            //Shop
            SetupActiveItems();
        }

        //==================================
        //=========SHOP
        //==================================

        //Active transition
        [SerializeField]
        private Transition playerTransition;
        public static Transition PlayerTransition { set { Instance.playerTransition = value; } get { return Instance.playerTransition; } }

        //Active playhead
        [SerializeField]
        private Playhead playerPlayhead;
        public static Playhead PlayerPlayhead { set { Instance.playerPlayhead = value; } get { return Instance.playerPlayhead; } }

        //Active lifebar
        [SerializeField]
        private Lifebar playerLifebar;
        public static Lifebar PlayerLifebar { set { Instance.playerLifebar = value; } get { return Instance.playerLifebar; } }

        //Load shop
        public static void SetupActiveItems()
        {
            PlayerTransition = ShopContent.Transitions.Where(i => i.Key == PlayerSettings.PlayerTransition).FirstOrDefault();
            PlayerPlayhead = ShopContent.Playheads.Where(i => i.Key == PlayerSettings.PlayerPlayhead).FirstOrDefault();
            PlayerLifebar = ShopContent.Lifebars.Where(i => i.Key == PlayerSettings.PlayerLifebar).FirstOrDefault();
        }
    }
}