using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace LTG.GifGame
{
    [CreateAssetMenu(menuName = "Singletons/Assets/Audios")]
    public class AudioAssets : Technical.SingletonScriptableObject<AudioAssets>
    {
        //==================================
        //=========PREFABS
        //==================================

        //Shop
        [SerializeField]
        private AudioClip audioShopBuy;
        public static AudioClip ShopBuy { get { return Instance.audioShopBuy; } }
        [SerializeField]
        private AudioClip audioShopNoMoney;
        public static AudioClip ShopNoMoney { get { return Instance.audioShopNoMoney; } }
        [SerializeField]
        private AudioClip audioShopSelect;
        public static AudioClip ShopSelect { get { return Instance.audioShopSelect; } }

        //Game
        [SerializeField]
        private AudioClip audioGameVictory;
        public static AudioClip GameVictory { get { return Instance.audioGameVictory; } }
        [SerializeField]
        private AudioClip audioGameDefeat;
        public static AudioClip GameDefeat { get { return Instance.audioGameDefeat; } }

        //Effects
        [SerializeField]
        private AudioClip audioEffectCollect;
        public static AudioClip EffectCollect { get { return Instance.audioEffectCollect; } }
    }
}