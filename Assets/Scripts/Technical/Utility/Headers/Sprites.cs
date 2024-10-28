using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace LTG.Technical
{
    public static class Sprites
    {
        //==================================
        //=========SHELL
        //==================================

        //Play
        public static void Sprite(GameObject gobject, Sprite sprite)
        {
            var renderer = gobject.GetComponent<SpriteRenderer>();
            if (renderer != null)
            {
                renderer.sprite = sprite;
            }
        }

        //Speed
        public static void Material(GameObject gobject, Material material)
        {
            var renderer = gobject.GetComponent<SpriteRenderer>();
            if (renderer != null)
            {
                renderer.material = material;
            }
        }

        //Layer
        public static void SortingLayer(GameObject gobject, string layer)
        {
            var renderer = gobject.GetComponent<SpriteRenderer>();
            if (renderer != null)
            {
                renderer.sortingLayerName = layer;
            }
        }
    }
}
