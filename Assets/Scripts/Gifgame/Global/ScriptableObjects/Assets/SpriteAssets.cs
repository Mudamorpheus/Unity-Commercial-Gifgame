using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace LTG.GifGame
{
    [CreateAssetMenu(menuName = "Singletons/Assets/Sprites")]
    public class SpriteAssets : Technical.SingletonScriptableObject<SpriteAssets>
    {
        //==================================
        //=========SPRITES
        //==================================

        //Button icons
        [SerializeField]
        private Sprite buttonMuteOnUnpressed;
        public static Sprite ButtonMuteOnUnpressed { get { return Instance.buttonMuteOnPressed; } }

        [SerializeField]
        private Sprite buttonMuteOnPressed;
        public static Sprite ButtonMuteOnPressed { get { return Instance.buttonMuteOnPressed; } }

        [SerializeField]
        private Sprite buttonMuteOffUnpressed;
        public static Sprite ButtonMuteOffUnpressed { get { return Instance.buttonMuteOffUnpressed; } }

        [SerializeField]
        private Sprite buttonMuteOffPressed;
        public static Sprite ButtonMuteOffPressed { get { return Instance.buttonMuteOffPressed; } }
    }
}