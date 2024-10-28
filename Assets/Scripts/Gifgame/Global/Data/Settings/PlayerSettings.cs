using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Newtonsoft.Json;

namespace LTG.GifGame
{
    [System.Serializable]
    public class PlayerSettings
    {
        //==================================
        //=========SETTINGS
        //==================================

        public void Reset()
        {
            PlayerBalance = 0;
            PlayerScore = 0;

            //===

            PlayerTransitions.Clear();
            PlayerTransitions.Add(ShopContent.Transitions[0].Key);
            //
            PlayerTransition = "Default";

            //===

            PlayerPlayheads.Clear();
            PlayerPlayheads.Add(ShopContent.Playheads[0].Key);
            //
            PlayerPlayhead = "Default";

            //===

            PlayerLifebars.Clear();
            PlayerLifebars.Add(ShopContent.Lifebars[0].Key);
            //
            PlayerLifebar = "Default";
        }

        //==================================
        //=========PLAYER
        //==================================

        //Coins
        [SerializeField]
        private int playerBalance = 0;
        public int PlayerBalance { set { playerBalance = value; } get { return playerBalance; } }

        //Score
        [SerializeField]
        private int playerScore = 0;
        public int PlayerScore { set { playerScore = value; } get { return playerScore; } }

        //==================================
        //=========OPTIONS
        //==================================

        //Sound
        [SerializeField]
        private bool optionAudio = true;
        public bool OptionAudio { set { optionAudio = value; } get { return optionAudio; } }

        //Tutorial
        [SerializeField]
        private bool optionTutorial = true;
        public bool OptionTutorial { set { optionTutorial = value; } get { return optionTutorial; } }

        //==================================
        //=========SHOP
        //==================================

        //Transitions
        [SerializeField]
        private string playerTransition = "Default";
        public string PlayerTransition { set { playerTransition = value; } get { return playerTransition; } }
        [SerializeField]
        private List<string> playerTransitions = new List<string>();
        public List<string> PlayerTransitions { set { playerTransitions = value; } get { return playerTransitions; } }

        //Playheads
        [SerializeField]
        private string playerPlayhead = "Default";
        public string PlayerPlayhead { set { playerPlayhead = value; } get { return playerPlayhead; } }
        [SerializeField]
        private List<string> playerPlayheads = new List<string>();
        public List<string> PlayerPlayheads { set { playerPlayheads = value; } get { return playerPlayheads; } }

        //Lifebars
        [SerializeField]
        private string playerLifebar = "Default";
        public string PlayerLifebar { set { playerLifebar = value; } get { return playerLifebar; } }
        [SerializeField]
        private List<string> playerLifebars = new List<string>();
        public List<string> PlayerLifebars { set { playerLifebars = value; } get { return playerLifebars; } }
    }
}
