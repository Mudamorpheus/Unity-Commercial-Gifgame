using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using TMPro;

namespace LTG.GifGame
{
    public class ScoreInterface : Interface
    {
        //==================================
        //=========CLASS
        //==================================

        public static ScoreInterface TInstance { get { return (ScoreInterface)Instance; } }

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

            //Score
            uiScoreboard.Init();

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

        //Alert
        [SerializeField]
        private GameObject uiAlertIcon;
        [SerializeField]
        private int uiAlertLimit = 1;

        //Components
        [SerializeField]
        private ScoreboardComponent uiScoreboard;

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
        //=========BALANCE
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
                SetScene(MasterProfile.GameSettings.LoadingScene, true);
            }
        }

        //Shop button
        public void EventButtonShop()
        {
            if (!transitionBlock)
            {
                SetScene(MasterProfile.GameSettings.ShopScene, true);
            }
        }
    }
}
