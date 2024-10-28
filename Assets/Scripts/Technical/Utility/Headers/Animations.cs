using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace LTG.Technical
{
    public static class Animations
    {
        //==================================
        //=========CONST
        //==================================

        //Animations
        public static readonly string AppearTimed = "AppearTimed";

        //==================================
        //=========SHELL
        //==================================

        //Play
        public static void Play(GameObject gobject, string animation, float speed)
        {
            var animator = gobject.GetComponent<Animator>();
            if(animator != null)
            {                
                animator.Play(animation);
                animator.speed = speed;
            }
        }

        //Speed
        public static void Speed(GameObject gobject, float speed)
        {
            var animator = gobject.GetComponent<Animator>();
            if (animator != null)
            {
                animator.speed = speed;
            }
        }
    }
}
