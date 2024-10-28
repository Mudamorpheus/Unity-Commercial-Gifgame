using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using TMPro;

namespace LTG.GifGame
{
    public class ShopInterface : Interface
    {
        //==================================
        //=========CLASS
        //==================================

        public static ShopInterface TInstance { get { return (ShopInterface)Instance; } }

        //==================================
        //=========SETUP
        //==================================

        //Player
        override protected void SetupPlayer()
        {
            base.SetupPlayer();

            //Settings
            var settings = PlayerProfile.PlayerSettings;

            //Balance
            SetBalance(settings.PlayerBalance);
        }

        //==================================
        //=========UI
        //==================================

        //Balance
        [SerializeField]
        private TMP_Text uiBalanceText;

        //Balance
        public void SetBalance(int balance)
        {
            if (uiBalanceText)
            {
                uiBalanceText.text = balance.ToString();
            }
        }

        //==================================
        //=========EVENTS
        //==================================

        //Play button
        public void EventButtonMenu()
        {
            if (!transitionBlock)
            {
                SetScene(MasterProfile.GameSettings.MenuScene, true);
            }
        }
    }
}
