using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using TMPro;

using LTG.Remote;
using LTG.Technical;

namespace LTG.GifGame
{
    public class GameInterface : Interface
    {
        //==================================
        //=========CLASS
        //==================================

        public static GameInterface TInstance { get { return (GameInterface)Instance; } }

        //==================================
        //=========UNITY
        //==================================

        override protected void Awake()
        {
            base.Awake();
         
            //Animation
            Init();

            //Pause
            Resume();
        }

        private void Update()
        {
            if (gameActive)
            {
                gameDuration += Time.deltaTime;

                if (gameDuration >= gameCycle)
                {
                    gameDuration = 0f;
                    gameHitbox = false;
                }

                if (gameDuration >= gameSpawn && !gameHitbox)
                {
                    Spawn();
                    gameHitbox = true;
                }
            }
        }

        //==================================
        //=========UI
        //==================================

        [SerializeField]
        private GameObject uiHitbox;
        public static GameObject Hitbox { get { return TInstance.uiHitbox; } }
        [SerializeField]
        private GameObject uiSpawner;
        public static GameObject Spawner { get { return TInstance.uiSpawner; } }

        [SerializeField]
        private PlayheadComponent uiPlayhead;
        [SerializeField]
        private LifebarComponent uiLifebar;

        private void Play(AudioClip clip, bool loop)
        {
            AudioSource.clip = clip;
            AudioSource.loop = loop;
            AudioSource.Play();
        }

        private void MuteLevel(bool state)
        {
            AudioSource[] sources = Background.GetComponentsInChildren<AudioSource>();
            foreach (var source in sources)
            {
                if (source != AudioSource)
                {
                    if (state)
                    {
                        source.Stop();
                    }
                    else
                    {
                        source.Play();
                    }
                }
            }
        }

        //==================================
        //=========LEVEL
        //==================================

        //Hints
        [SerializeField]
        private bool lvlHints = false;

        //Data
        private RemoteLevelData lvlData;
        public RemoteLevelData LvlData { get { return lvlData; } }
        private RemoteHitboxData htbxData;
        public RemoteHitboxData HitboxData { get { return htbxData; } }

        //Level
        private void Init()
        {
            //Data
            lvlData = LoadingManager.LoadedAnimation.GetComponent<RemoteLevelData>();
            htbxData = LoadingManager.LoadedHitbox.GetComponent<RemoteHitboxData>(); ; 

            //Params
            gameCycle = lvlData.Duration;
            gameSpawn = htbxData.Time;

            //Playhead
            uiPlayhead.Init(lvlData.Duration);

            //Animation
            Instantiate(LoadingManager.LoadedAnimation, Background.transform);

            //Shaders
            RestoreMaterials();

            //Controller
            var controller = uiHitbox.AddComponent<HitboxController>();
            controller.OnMouseDownEvent.AddListener(delegate { Damage(1); });
        }

        //Hitbox
        private void Spawn()
        {
            //Hitbox
            var hitbox = Instantiate(LoadingManager.LoadedHitbox, Spawner.transform);
            Destroy(hitbox, htbxData.Duration);

            //Hints
            if(lvlHints)
            {
                var zone = Instantiate(PrefabAssets.UiHitboxZone, hitbox.transform);
            }

            //Controller
            var controller = hitbox.AddComponent<HitboxController>();
            controller.OnMouseDownEvent.AddListener(delegate { Victory(); });
        }

        //==================================
        //=========GAMEPLAY
        //==================================

        private bool gameActive = false;
        private bool gameHitbox = false;
        private float gameDuration = 0f;
        private float gameCycle = 1f;
        private float gameSpawn = 1f;

        public void Damage(int damage)
        {
            if(gameActive && !IsTransition())
            {
                uiLifebar.Damage(damage);

                if (uiLifebar.Lives <= 0)
                {
                    Defeat();
                }
            }            
        }

        private void Victory()
        {
            if (gameActive && !IsTransition())
            {
                //Disable
                Pause();

                //State
                uiPlayhead.Victory();

                //Coins
                Coins();
                PlayerProfile.PlayerSettings.PlayerBalance++;
                PlayerProfile.PlayerSettings.PlayerScore++;

                //Audio
                Play(AudioAssets.GameVictory, false);

                //Next
                StartCoroutine(Coroutines.Delay(delegate { End(); }, 1.8f));

                //Progress
                PlayerProfile.PlayerProgress.Victory();
            }
        }

        private void Defeat()
        {
            //Disable
            Pause();

            //State
            uiPlayhead.Defeat();

            //Audio
            Play(AudioAssets.GameDefeat, false);

            //Next
            End();

            //Progress
            PlayerProfile.PlayerProgress.Defeat();
        }
       
        private void Pause()
        {
            //States
            gameActive = false;
            uiPlayhead.Run = false;

            //Mute
            MuteLevel(true);

            //Animations
            foreach (Transform child in Background.transform.GetComponentsInChildren<Transform>())
            {
                Animations.Speed(child.gameObject, 0f);
            }
            foreach (Transform child in Hitbox.transform.GetComponentsInChildren<Transform>())
            {
                Animations.Speed(child.gameObject, 0f);
            }
        }
        private void Resume()
        {
            //States
            gameActive = true;
            uiPlayhead.Run = true;

            //Mute
            MuteLevel(false);

            //Animations
            foreach (Transform child in Background.transform.GetComponentsInChildren<Transform>())
            {
                Animations.Speed(child.gameObject, 1f);
            }
            foreach (Transform child in Hitbox.transform.GetComponentsInChildren<Transform>())
            {
                Animations.Speed(child.gameObject, 1f);
            }
        }

        private void End()
        {
            SetScene(MasterProfile.GameSettings.ScoreScene, true);
        }

        //==================================
        //=========EFFECTS
        //==================================

        [SerializeField]
        private Material defaultMaterial;

        private void RestoreMaterials()
        {
            foreach (Transform child in Background.transform.GetComponentsInChildren<Transform>())
            {
                Sprites.Material(child.gameObject, defaultMaterial);
            }
        }

        private void Coins()
        {
            //Effect
            GameObject effect = Instantiate(PrefabAssets.Collect, Canvas.transform);

            //Balance
            int balance = PlayerProfile.PlayerSettings.PlayerBalance;
            TMP_Text tmp = effect.transform.Find("Balance").GetComponent<TMP_Text>();
            tmp.text = balance.ToString();

            //Delay
            StartCoroutine(Technical.Coroutines.Delay(delegate { Play(AudioAssets.EffectCollect, false); ; }, 1.8f));
            StartCoroutine(Technical.Coroutines.Delay(delegate { tmp.text = (balance+1).ToString(); }, 1.8f));
        }
    }
}
