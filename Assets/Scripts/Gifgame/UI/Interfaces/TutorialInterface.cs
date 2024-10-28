using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using TMPro;

using LTG.Remote;
using LTG.Technical;

namespace LTG.GifGame
{
    public class TutorialInterface : Interface
    {
        //==================================
        //=========CLASS
        //==================================

        public static TutorialInterface TInstance { get { return (TutorialInterface)Instance; } }

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
            if (uiPlayhead.Run && !visualPause)
            {
                gameDuration += Time.deltaTime;

                if (gameDuration >= gameCycle)
                {
                    gameDuration = 0f;
                    gameHitbox = false;
                }

                if (gameDuration >= gameSpawn && !gameHitbox)
                {
                    if (gameActive) { Hitb(); }
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

        [SerializeField]
        private GameObject uiFade;
        [SerializeField]
        private GameObject uiTopLeft;
        [SerializeField]
        private GameObject uiTopRight;

        [SerializeField]
        private GameObject uiHint1;
        [SerializeField]
        private GameObject uiHint2;
        [SerializeField]
        private GameObject uiHint3;

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
        //=========TUTORIAL
        //==================================

        private void StartTutorial()
        {
            StartCoroutine(Coroutines.Delay(delegate { Phase1(); }, 1f));
        }

        //=======

        bool tutorialPhase1 = false;
        private void Phase1()
        {
            tutorialPhase1 = true;

            Pause();
            SortTopRight();
            FadeOn();

            StartCoroutine(Coroutines.Delay(delegate { StartHint1(); }, 1f));            
        }
        public void StartHint1()
        {
            Animations.Play(uiHint1, "On", 1f);
            StartCoroutine(Coroutines.Delay(delegate { uiLifebar.Damage(1); }, 1.0f));
            StartCoroutine(Coroutines.Delay(delegate { uiLifebar.Damage(1); }, 2.0f));
            StartCoroutine(Coroutines.Delay(delegate { uiLifebar.Damage(1); }, 3.0f));
            StartCoroutine(Coroutines.Delay(delegate { Animations.Play(uiHint1, "Break", 1f); }, 4f));            
        }
        public void EndHint1()
        {
            tutorialPhase1 = false;

            Animations.Play(uiHint1, "Off", 1f);
            uiLifebar.Heal(3);
            StartCoroutine(Coroutines.Delay(delegate { Destroy(uiHint1); }, 1f));
            StartCoroutine(Coroutines.Delay(delegate { Phase2(); }, 2f));
        }

        //=======

        bool tutorialPhase2 = false;
        private void Phase2()
        {
            tutorialPhase2 = true;

            Pause();
            SortTopLeft();

            StartCoroutine(Coroutines.Delay(delegate { StartHint2(); }, 1f));
        }
        public void StartHint2()
        {
            Animations.Play(uiHint2, "On", 1f);
            StartCoroutine(Coroutines.Delay(delegate { uiPlayhead.Run = true; }, 2f));
            StartCoroutine(Coroutines.Delay(delegate { Animations.Play(uiHint2, "Rotate", 1f); ; }, 2f));           
            StartCoroutine(Coroutines.Delay(delegate { if (tutorialPhase2) { Animations.Play(uiHint2, "Loop", 1f); } }, 5f));
        }
        public void EndHint2()
        {
            tutorialPhase2 = false;

            Animations.Play(uiHint2, "Off", 1f);                                 
            StartCoroutine(Coroutines.Delay(delegate { Destroy(uiHint2); }, 1f));
            StartCoroutine(Coroutines.Delay(delegate { FadeOff(); }, 2f));
            StartCoroutine(Coroutines.Delay(delegate { uiPlayhead.Phase = true; }, 3f));
            StartCoroutine(Coroutines.Delay(delegate { uiPlayhead.Tip = true; }, 3f));
            StartCoroutine(Coroutines.Delay(delegate { uiPlayhead.Duration = 1f; }, 3f));
            StartCoroutine(Coroutines.Delay(delegate { Resume(); }, 3f));
            StartCoroutine(Coroutines.Delay(delegate { Phase3(); }, 4f));
        }

        //=======

        bool tutorialPhase3 = false;
        private void Phase3()
        {
            tutorialPhase3 = true;

            Pause();
            uiPlayhead.Run = true;
            SortTopLeft();
            FadeOn();

            StartCoroutine(Coroutines.Delay(delegate { StartHint3(); }, 1f));
        }
        public void StartHint3()
        {            
            Animations.Play(uiHint3, "On", 1f);
            Animations.Play(uiHint3, "Rotate", 1f);
            StartCoroutine(Coroutines.Delay(delegate { if (tutorialPhase3) { Animations.Play(uiHint3, "Rotate2", 1f); } }, 6f));
            StartCoroutine(Coroutines.Delay(delegate { if (tutorialPhase3) { Animations.Play(uiHint3, "Loop", 1f); } }, 12f));
        }
        public void EndHint3()
        {
            tutorialPhase3 = false;

            Animations.Play(uiHint3, "Off", 1f);
            FadeOff();
            Gameplay();
            StartCoroutine(Coroutines.Delay(delegate { Resume(); }, 1f));
            StartCoroutine(Coroutines.Delay(delegate { uiPlayhead.Tip = true; ; }, 1f));
            StartCoroutine(Coroutines.Delay(delegate { uiPlayhead.Duration = 2f; }, 1f));
            StartCoroutine(Coroutines.Delay(delegate { uiPlayhead.Recolor(Color.white); }, 1f));
            StartCoroutine(Coroutines.Delay(delegate { Destroy(uiHint3); }, 1f));                        
        }

        //=======

        public void Gameplay()
        {
            gameActive = true;
            var controller = uiHitbox.AddComponent<HitboxController>();
            controller.OnMouseDownEvent.AddListener(delegate { Damage(1); });
        }

        //=======

        private void FadeOn()
        {
            Animations.Play(uiFade, "On 50%", 1f);
        }

        private void FadeOff()
        {
            Animations.Play(uiFade, "Off 50%", 1f);
        }

        private void SortTopLeft()
        {            
            uiTopRight.transform.SetSiblingIndex(0);
            uiFade.transform.SetSiblingIndex(1);
            uiTopLeft.transform.SetSiblingIndex(2);
        }

        private void SortTopRight()
        {
            uiTopLeft.transform.SetSiblingIndex(0);
            uiFade.transform.SetSiblingIndex(1);            
            uiTopRight.transform.SetSiblingIndex(2);
        }

        //==================================
        //=========LEVEL
        //==================================

        //Data
        private RemoteLevelData lvlData;
        public RemoteLevelData LvlData { get { return lvlData; } }
        private RemoteHitboxData htbxData;
        public RemoteHitboxData HitboxData { get { return htbxData; } }

        //Level
        private void Init()
        {
            //Data
            lvlData = PrefabAssets.LvlTutorialVideo.GetComponent<RemoteLevelData>();
            htbxData = PrefabAssets.LvlTutorialHitbox.GetComponent<RemoteHitboxData>(); ;

            //Params
            gameCycle = lvlData.Duration;
            gameSpawn = htbxData.Time;

            //Playhead
            uiPlayhead.Init(lvlData.Duration);
            uiPlayhead.Phase = false;

            //Animation
            Instantiate(PrefabAssets.LvlTutorialVideo, Background.transform);
            Run();
        }
        private void Run()
        {
            StartTutorial();
        }

        //==================================
        //=========GAMEPLAY
        //==================================

        private bool gameActive = false;
        private bool gameHitbox = false;
        private bool gameFailed = false;
        private bool gameHint = false;
        private float gameDuration = 0f;
        private float gameCycle = 1f;
        private float gameSpawn = 1f;        

        public void Damage(int damage)
        {
            if (gameActive && !IsTransition())
            {
                uiLifebar.Damage(damage);

                if (uiLifebar.Lives <= 0)
                {
                    Defeat();
                }
            }
        }

        //Hitbox
        private void Hitb()
        {
            //Hitbox
            var hitbox = Instantiate(PrefabAssets.LvlTutorialHitbox, Spawner.transform);

            //Tutorial
            if (gameFailed || gameHint)
            {
                var arrow = Instantiate(PrefabAssets.UiHitboxArrow, hitbox.transform);                
            }

            if (gameFailed) 
            { 
                Pause();             
            }
            else
            {
                Destroy(hitbox, htbxData.Duration);
            }

            //Hint
            gameHint = true;

            //Controller
            var controller = hitbox.AddComponent<HitboxController>();
            controller.OnMouseDownEvent.AddListener(delegate { Victory(); });
        }

        private void Victory()
        {
            if (gameActive && !IsTransition())
            {
                //Disable
                Pause();

                //State
                uiPlayhead.Victory();

                //Audio
                Play(AudioAssets.GameVictory, false);

                //Next
                End();
            }
        }

        private void Defeat()
        {
            if (gameActive && !IsTransition())
            {
                //Fail
                gameFailed = true;

                //Audio
                Play(AudioAssets.GameDefeat, false);
            }
        }

        //==================================
        //=========VISUAL
        //==================================

        private bool visualPause = false;

        private void Pause()
        {
            //States
            uiPlayhead.Run = false;
            visualPause = true;

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
            uiPlayhead.Run = true;
            visualPause = false;

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
            PlayerProfile.PlayerSettings.OptionTutorial = false;
            SetScene(MasterProfile.GameSettings.MenuScene, true);
        }
    }
}
