using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace LTG.Remote
{
    public class RemoteLevelData : MonoBehaviour
    {
        //=========================
        //=========LEVEL
        //=========================

        //Data
        [SerializeField]
        private float levelDuration = 0.0f;
        public float Duration { get { return levelDuration; } set { levelDuration = value; } }
    }
}
