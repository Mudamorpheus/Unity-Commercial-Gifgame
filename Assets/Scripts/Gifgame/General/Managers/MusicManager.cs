using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace LTG.GifGame
{
    [RequireComponent(typeof(AudioSource))]
    public class MusicManager : MonoBehaviour
    {
        //==================================
        //=========MUSIC
        //==================================

        private static GameObject instance = null;
        public static GameObject Instance { get { return instance; } set { instance = value; } }

        [SerializeField]
        private bool transferMusic;
        public bool TransferMusic { get { return transferMusic; } set { transferMusic = value; } }

        [SerializeField]
        private bool breakMusic;
        public bool BreakMusic { get { return breakMusic; } set { breakMusic = value; } }

        //==================================
        //=========UNITY
        //==================================

        //Scene start
        private void Start()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
