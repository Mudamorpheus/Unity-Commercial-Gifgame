using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

namespace LTG.Technical
{
    public static class Coroutines
    {
        //==================================
        //=========COROUTINES
        //==================================

        //Delay
        public static IEnumerator Delay(UnityAction action, float delay)
        {
            yield return new WaitForSeconds(delay);
            action?.Invoke();
            yield return null;
        }
    }
}
