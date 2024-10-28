using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace LTG.Remote
{
    public class RemoteHitboxData : MonoBehaviour
    {
        //=========================
        //=========LEVEL
        //=========================

        //Data
        [SerializeField]
        private float hitboxTime = 0.0f;
        public float Time { get { return hitboxTime; } set { hitboxTime = value; } }
        [SerializeField]
        private float hitboxDuration = 0.0f;
        public float Duration { get { return hitboxDuration; } set { hitboxDuration = value; } }
    }
}

