using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace LTG.GifGame
{
    public class AnimationController : MonoBehaviour
    {
        //==================================
        //=========ANIMATION
        //==================================

        //Components
        private Animator controllerAnimator;

        //Params
        [SerializeField]
        private string controllerState;

        //==================================
        //=========UNITY
        //==================================

        private void Awake()
        {
            //Components
            controllerAnimator = gameObject.GetComponent<Animator>();

            //Action
            if(controllerAnimator)
            {
                controllerAnimator.Play(controllerState);
            }
        }
    }
}
