using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using TMPro;

namespace LTG.GifGame
{
    public class LoadingInterface : Interface
    {
        //==================================
        //=========CLASS
        //==================================

        public static LoadingInterface TInstance { get { return (LoadingInterface)Instance; } }

        //==================================
        //=========Unity
        //==================================

        //Scene awake
        override protected void Awake()
        {
            base.Awake();

            //Static
            FreezeTransition();
        }
    }
}
