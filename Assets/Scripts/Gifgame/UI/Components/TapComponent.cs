using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace LTG.GifGame
{
    public class TapComponent : MonoBehaviour
    {
        //==================================
        //=========EVENT
        //==================================

        private void CreateTap(Vector3 position)
        {
            //Convert position
            var uiRect = Interface.Canvas.GetComponent<RectTransform>();
            position = Technical.Math.WorldToCanvasPosition(uiRect, Interface.Camera, position);

            //Create object
            GameObject tap = Instantiate(PrefabAssets.Tap, Interface.Canvas.transform);
            tap.transform.localPosition = new Vector3(position.x, position.y, 0);

            //Clear object
            Destroy(tap, 0.25f);
        }

        private void OnMouseDown()
        {
            //Tap
            Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            CreateTap(position);
        }
    }
}
