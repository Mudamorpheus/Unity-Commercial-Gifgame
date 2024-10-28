using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace LTG.GifGame
{
    [CreateAssetMenu(menuName = "Singletons/Profiles/Master")]
    public class MasterProfile : Technical.SingletonScriptableObject<MasterProfile>
    {
        //==================================
        //=========SETTINGS
        //==================================

        [SerializeField]
        private GameSettings gameSettings = new GameSettings();
        public static GameSettings GameSettings { get { return Instance.gameSettings; } }

    }
}