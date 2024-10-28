using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using TMPro;

namespace LTG.GifGame
{
    public class MenuInterface : Interface
    {
        //==================================
        //=========CLASS
        //==================================

        public static MenuInterface TInstance { get { return (MenuInterface)Instance; } }

        //==================================
        //=========SETUP
        //==================================

        //Player
        override protected void SetupPlayer()
        {
            base.SetupPlayer();

            //Settings
            var settings = PlayerProfile.PlayerSettings;

            //Text
            SetTitle(PlayerProfile.PlayerName);

            //Balance
            SetBalance(settings.PlayerBalance);

            //Alert
            if (settings.PlayerBalance < uiAlertLimit)
            {
                if (uiAlertIcon)
                {
                    Destroy(uiAlertIcon);
                }
            }
        }

        //==================================
        //=========UI
        //==================================

        //Buttons
        [SerializeField]
        private Button uiMuteButton;

        //Texts
        [SerializeField]
        private TMP_Text uiTitleText;
        [SerializeField]
        private TMP_Text uiBalanceText;

        //Alert
        [SerializeField]
        private GameObject uiAlertIcon;
        [SerializeField]
        private int uiAlertLimit = 1;

        //Title
        public void SetTitle(string player)
        {
            if(uiTitleText)
            {
                uiTitleText.text = "Hello, " + player;
            }            
        }

        //Balance
        public void SetBalance(int balance)
        {
            if (uiBalanceText)
            {
                uiBalanceText.text = balance.ToString();
            }
        }

        //Sound
        override public void SetActiveAudio(bool state)
        {
            base.SetActiveAudio(state);

            //Icons
            SpriteState sprites = uiMuteButton.spriteState;
            Image image = uiMuteButton.image;
            if (state)
            {
                image.sprite = SpriteAssets.ButtonMuteOnUnpressed;
                sprites.pressedSprite = SpriteAssets.ButtonMuteOnPressed;
            }
            else
            {
                image.sprite = SpriteAssets.ButtonMuteOffUnpressed;
                sprites.pressedSprite = SpriteAssets.ButtonMuteOffPressed;
            }
        }

        //==================================
        //=========EVENTS
        //==================================

        //Mute button
        public void EventButtonAudio()
        {
            if (!transitionBlock)
            {
                //Settings
                var settings = PlayerProfile.PlayerSettings;

                //Options
                settings.OptionAudio = !settings.OptionAudio;

                //Audio
                SetActiveAudio(settings.OptionAudio);
            }
        }

        //Play button
        public void EventButtonPlay()
        {
            if (!transitionBlock)
            {
                if(PlayerProfile.PlayerSettings.OptionTutorial) 
                     { SetScene(MasterProfile.GameSettings.TutorialScene, true); }
                else { SetScene(MasterProfile.GameSettings.LoadingScene, true); }                
            }
        }

        //Tutorial button
        public void EventButtonTutorial()
        {
            if (!transitionBlock) { SetScene(MasterProfile.GameSettings.TutorialScene, true); }
        }

        //Shop button
        public void EventButtonShop()
        {
            if (!transitionBlock) { SetScene(MasterProfile.GameSettings.ShopScene, true); }
        }
    }
}
