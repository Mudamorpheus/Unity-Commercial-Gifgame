using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

namespace LTG.GifGame 
{
    public class PlayheadComponent : MonoBehaviour
    {
        //==================================
        //=========UI
        //==================================

        //Components
        private Mask playheadMask;

        //==================================
        //=========UNITY
        //==================================

        private void Update()
        {
            if (playheadRun)
            {
                playheadDuration += Time.deltaTime;

                if (!playheadSpawned)
                {
                    //Spawn playhead
                    Spawn();

                    //Remove
                    if (playheadFirst)
                    {
                        StartCoroutine(Technical.Coroutines.Delay(delegate { Remove(); }, 2f));
                    }

                    //Logic
                    playheadSpawned = true;
                    playheadFirst = true;
                }

                if (playheadDuration >= playheadCycle)
                {
                    playheadDuration = 0f;
                    playheadSpawned = false;
                }

                foreach (var playhead in playheadList)
                {
                    float distance = playheadDistance * playheadDuration/playheadCycle - playheadDistance/2f;
                    playhead.transform.localPosition = new Vector3(distance, 0, 0);
                }
            }
        }

        //==================================
        //=========PLAYHEAD
        //==================================

        //Params        
        private float playheadDuration;
        public float Duration { get { return playheadDuration; } set { playheadDuration = value; } }
        private float playheadCycle;        
        private float playheadDistance;
        private float playheadSpeed;
        private bool playheadTip = false;
        public bool Tip { get { return playheadTip; } set { playheadTip = value; } }
        private bool playheadSpawned = false;
        private bool playheadFirst = false;

        //State
        private bool playheadPhase = true;
        public bool Phase { get { return playheadPhase; } set { playheadPhase = value; } }
        private bool playheadRun = false;
        public bool Run { get { return playheadRun; } set { playheadRun = value; } }

        //List
        private List<GameObject> playheadList = new List<GameObject>();

        public void Init(float duration)
        {
            //Components
            playheadMask = GetComponentInChildren<Mask>();

            //Params
            var rect = GetComponent<RectTransform>();
            playheadDistance = rect.sizeDelta.x * 1.50f;
            playheadDuration = 0;
            playheadCycle = duration/2f;            
            playheadSpeed = playheadDistance/playheadCycle;
        }

        public void Recolor(Color color)
        {
            foreach (var playhead in playheadList)
            {
                var image = playhead.GetComponent<Image>();
                image.color = color;
            }
        }

        public void Spawn()
        {
            //Params
            var prefab = PrefabAssets.UiPlayhead;
            float shift = playheadDistance/2f;

            //Playhead
            var playhead = Instantiate(prefab, playheadMask.gameObject.transform);
            playhead.transform.localPosition = new Vector2(-shift, 0);

            //Components
            var image = playhead.GetComponent<Image>();
            if (playheadTip)
            {
                image.sprite = PlayerProfile.PlayerPlayhead.PlayheadActive;
                image.color = Color.yellow;
            }
            else
            {
                image.sprite = PlayerProfile.PlayerPlayhead.PlayheadDefault;
                image.color = Color.white;
            }
            //List            
            playheadList.Add(playhead);

            //Switch
            if (playheadPhase)
            {
                playheadTip = !playheadTip;
            }
        }

        private void Remove()
        {
            if(playheadList.Count > 0)
            {
                var playhead = playheadList[0];
                playheadList.Remove(playhead);
                Destroy(playhead);
            }            
        }

        public void Victory()
        {
            foreach(var playhead in playheadList)
            {
                var image = playhead.GetComponent<Image>();
                image.sprite = PlayerProfile.PlayerPlayhead.PlayheadVictory;
                if(PlayerProfile.PlayerPlayhead.PlayheadRecolor)
                { 
                    image.color = Color.green;
                }
            }
        }

        public void Defeat()
        {
            foreach (var playhead in playheadList)
            {
                var image = playhead.GetComponent<Image>();
                image.sprite = PlayerProfile.PlayerPlayhead.PlayheadDefeat;
                if (PlayerProfile.PlayerPlayhead.PlayheadRecolor)
                {
                    image.color = Color.red;
                }
            }
        }
    }
}
