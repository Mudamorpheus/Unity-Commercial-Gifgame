using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using TMPro;

namespace LTG.GifGame
{
    [RequireComponent(typeof(TMP_Text))]
    public class FpsCounterComponent : MonoBehaviour
    {
        //==================================
        //=========FPS
        //==================================

        //Params
        private int fpsMax = 60;
        private float fpsTime = 0;
        private int fpsCount = 0;
        private int fpsRate = 0;
        private TMP_Text fpsText;

        //Update
        private void UpdateFps()
        {
            //Calculate
            if (fpsTime != 0)
            {
                //Params
                fpsRate = (int)(fpsCount/fpsTime)+1;
                fpsTime = 0;
                fpsCount = 0;

                //Text
                if (fpsText)
                {
                    fpsText.text = fpsRate.ToString() + " FPS";
                }                
            }
        }

        //==================================
        //=========Unity
        //==================================

        //Scene start
        private void Awake()
        {
            //FPS
            fpsMax = Interface.Instance.MaxFPS;
            InvokeRepeating(nameof(UpdateFps), 0.00f, 0.25f);

            //Text
            fpsText = GetComponent<TMP_Text>();
            fpsText.text = "0 FPS";
        }

        //Scene update
        private void Update()
        {
            //FPS
            fpsTime += Time.deltaTime;
            fpsCount++;
        }
    }
}
