using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace LTG.Technical
{
    public class Math
    {
        //World to canvas
        public static Vector3 WorldToCanvasPosition(RectTransform canvas, Camera camera, Vector3 position)
        {
            Vector3 temp = camera.WorldToViewportPoint(position);

            temp.x *= canvas.sizeDelta.x;
            temp.y *= canvas.sizeDelta.y;

            temp.x -= canvas.sizeDelta.x * canvas.pivot.x;
            temp.y -= canvas.sizeDelta.y * canvas.pivot.y;

            return temp;
        }
    }
}
