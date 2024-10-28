using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace LTG.Technical
{
    public static class Audios
    {
        //==================================
        //=========SHELL
        //==================================

        //Play
        public static void Play(GameObject gobject, AudioClip clip, float volume)
        {
            var source = gobject.GetComponent<AudioSource>();
            if (source != null)
            {
                source.clip = clip;
                source.volume = volume;
                source.Play();
            }
        }
        public static void Play(AudioSource source, AudioClip clip, float volume)
        {
            source.clip = clip;
            source.volume = volume;
            source.Play();
        }
    }
}
