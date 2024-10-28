using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

namespace LTG.GifGame
{
    public class HitboxController : MonoBehaviour
    {
        //==================================
        //=========EVENT
        //==================================

        //Event
        [SerializeField]
        private UnityEvent onMouseDownEvent = new UnityEvent();
        public UnityEvent OnMouseDownEvent { get { return onMouseDownEvent; } }

        //Action
        private void OnMouseDown()
        {
            onMouseDownEvent?.Invoke();
        }

        //Destroy
        private void OnDestroy()
        {
            onMouseDownEvent.RemoveAllListeners();
        }
    }
}
