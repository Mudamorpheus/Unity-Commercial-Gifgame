using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace LTG.GifGame
{
    [System.Serializable]
    public class GameSettings
    {
        //==================================
        //=========GAME
        //==================================

        //Name
        [SerializeField]
        private string gameDefault = "Player#001";
        public string GameDefault { get { return gameDefault; } }

        //Version
        [SerializeField]
        private string gameVersion = "1.0.0";
        public string GameVersion { get { return gameVersion; } }

        //==================================
        //=========SCENES
        //==================================

        [SerializeField]
        private string initScene = "InitScene";
        public string InitScene { get { return initScene; } }
        [SerializeField]
        private string menuScene = "MenuScene";
        public string MenuScene { get { return menuScene; } }
        [SerializeField]
        private string shopScene = "ShopScene";
        public string ShopScene { get { return shopScene; } }
        [SerializeField]
        private string loadingScene = "LoaderScene";
        public string LoadingScene { get { return loadingScene; } }
        [SerializeField]
        private string levelScene = "LevelScene";
        public string LevelScene { get { return levelScene; } }
        [SerializeField]
        private string scoreScene = "ScoreScene";
        public string ScoreScene { get { return scoreScene; } }
        [SerializeField]
        private string tutorialScene = "TutorialScene";
        public string TutorialScene { get { return tutorialScene; } }
    }
}
