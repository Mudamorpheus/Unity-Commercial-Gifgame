using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace LTG.GifGame
{
    [CreateAssetMenu(menuName = "Singletons/Assets/Prefabs")]
    public class PrefabAssets : Technical.SingletonScriptableObject<PrefabAssets>
    {
        //==================================
        //=========EFFECTS
        //==================================

        [SerializeField]
        private GameObject effectTap;
        public static GameObject Tap { get { return Instance.effectTap; } }

        [SerializeField]
        private GameObject effectCollect;
        public static GameObject Collect { get { return Instance.effectCollect; } }

        //==================================
        //=========UI
        //==================================

        [SerializeField]
        private GameObject uiCell;
        public static GameObject UiCell { get { return Instance.uiCell; } }

        [SerializeField]
        private GameObject uiLevel;
        public static GameObject UiLevel { get { return Instance.uiLevel; } }

        [SerializeField]
        private GameObject uiPlayhead;
        public static GameObject UiPlayhead { get { return Instance.uiPlayhead; } }

        [SerializeField]
        private GameObject uiLifebar;
        public static GameObject UiLifebar { get { return Instance.uiLifebar; } }

        [SerializeField]
        private GameObject uiScore;
        public static GameObject UiScore { get { return Instance.uiScore; } }

        [SerializeField]
        private GameObject uiHitboxZone;
        public static GameObject UiHitboxZone { get { return Instance.uiHitboxZone; } }

        [SerializeField]
        private GameObject uiHitboxArrow;
        public static GameObject UiHitboxArrow { get { return Instance.uiHitboxArrow; } }

        [SerializeField]
        private GameObject uiErrorBox;
        public static GameObject UiErrorBox { get { return Instance.uiErrorBox; } }

        //==================================
        //=========LEVELS
        //==================================

        [SerializeField]
        private GameObject lvlTutorialHitbox;
        public static GameObject LvlTutorialHitbox { get { return Instance.lvlTutorialHitbox; } }

        [SerializeField]
        private GameObject lvlTutorialVideo;
        public static GameObject LvlTutorialVideo { get { return Instance.lvlTutorialVideo; } }

    }
}