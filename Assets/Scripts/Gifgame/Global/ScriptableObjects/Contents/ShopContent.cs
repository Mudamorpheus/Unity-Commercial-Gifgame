using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace LTG.GifGame
{
    [System.Serializable]
    public class Item
    {
        public string Key = "";
        public int Price = 0;
        public GameObject Icon;

        [System.NonSerialized]
        public GameObject Cell;
    }

    [System.Serializable]
    public class Transition : Item
    {
        public GameObject Prefab;
        public float Duration;
    }

    [System.Serializable]
    public class Playhead : Item
    {
        public Sprite PlayheadDefault;
        public Sprite PlayheadActive;
        public Sprite PlayheadVictory;
        public Sprite PlayheadDefeat;
        public bool PlayheadRecolor;
    }

    [System.Serializable]
    public class Lifebar : Item
    {
        public Sprite LifebarOn;
        public Sprite LifebarOff;
    }

    [CreateAssetMenu(menuName = "Singletons/Contents/Shop")]
    public class ShopContent : Technical.SingletonScriptableObject<ShopContent>
    {
        //==================================
        //=========TRANSITIONS
        //==================================

        [SerializeField]
        private List<Transition> shopTransitions;
        static public List<Transition> Transitions { set { Instance.shopTransitions = value; } get { return Instance.shopTransitions; } }

        //==================================
        //=========PLAYHEADS
        //==================================

        [SerializeField]
        private List<Playhead> shopPlayheads;
        static public List<Playhead> Playheads { set { Instance.shopPlayheads = value; } get { return Instance.shopPlayheads; } }

        //==================================
        //=========LIFEBARS
        //==================================

        [SerializeField]
        private List<Lifebar> shopLifebars;
        static public List<Lifebar> Lifebars { set { Instance.shopLifebars = value; } get { return Instance.shopLifebars; } }
    }
}