using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace LTG.Technical
{
    public class DontDestroyOnLoad : MonoBehaviour
    {
        void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}

