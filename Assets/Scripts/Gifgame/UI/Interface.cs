using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

using TMPro;

namespace LTG.GifGame
{ 
    public class Interface : Technical.SingletonMonoBehaviour<Interface>
    {
        //==================================
        //=========Unity
        //==================================

        //Scene awake
        virtual protected void Awake()
        {
            //=====
            //APPLICATION
            //=====

            //FPS
            Application.targetFrameRate = fpsMax;

            //=====
            //SETUP
            //=====

            //Music
            SetupMusic();

            //Player
            SetupPlayer();

            //=====
            //CHECK
            //=====

            //Check
            if (!PlayerProfile.Loaded)
            {
                ExitApplication();
                return;
            }

            //=====
            //UI
            //=====

            //Text
            if (uiVersionText)
            {
                uiVersionText.text = "version " + PlayerProfile.PlayerVersion;
            }

            //Transition
            if (uiAutoTransition)
            {
                OpenTransition(null);
            }

            //=====
            //SAVE
            //=====

            //Autosave
            PlayerProfile.SaveProfileJson();
        }

        //Application quit
        void OnApplicationQuit()
        {
            //Save
            PlayerProfile.SaveProfileJson();

            //Destroy
            PlayerProfile.Flush();
            LevelsContent.Flush();
        }

        //==================================
        //=========SETUP
        //==================================

        //Player
        virtual protected void SetupPlayer()
        {
            //Settings
            var settings = PlayerProfile.PlayerSettings;

            //Audio
            SetActiveAudio(settings.OptionAudio);
        }

        //Music
        private void SetupMusic()
        {
            var uiInstance = MusicManager.Instance;
            var compManager = uiMusic.GetComponent<MusicManager>();          
            //uiMusic = manager.gameObject;

            //Check
            if (uiMusic != null)
            {
                if (compManager.BreakMusic)
                {
                    if (uiInstance) { Destroy(uiInstance); }
                    if (uiMusic) { Destroy(uiMusic); }
                }
                else
                {
                    if (compManager.TransferMusic)
                    {
                        if (uiInstance)
                        {
                            Destroy(uiMusic);
                            uiMusic = uiInstance;
                        }
                        else
                        {
                            MusicManager.Instance = uiMusic;
                        }
                    }
                }
            }
            else
            {
                if (uiInstance) { Destroy(uiInstance); }
            }
        }

        //==================================
        //=========UI
        //==================================

        //Basic
        [SerializeField]
        private Camera uiCamera;
        public static Camera Camera { get { return Instance.uiCamera; } }
        [SerializeField]
        private GameObject uiCanvas;
        public static GameObject Canvas { get { return Instance.uiCanvas; } }
        [SerializeField]
        private GameObject uiBackground;
        public static GameObject Background { get { return Instance.uiBackground; } }
        [SerializeField]
        private AudioSource uiAudioSource;
        public static AudioSource AudioSource { get { return Instance.uiAudioSource; } }

        //Texts
        [SerializeField]
        private TMP_Text uiVersionText;
        public static TMP_Text Version { get { return Instance.uiVersionText; } }

        //Music
        [SerializeField]
        private GameObject uiMusic;

        //Transition
        [SerializeField]
        private bool uiAutoTransition = true;

        //Sound
        virtual public void SetActiveAudio(bool state)
        {
            AudioListener.volume = (state ? 1 : 0);
        }

        //Load scene
        public void SetScene(string scene, bool animated)
        {
            if(animated)
            {
                CloseTransition(delegate { SceneManager.LoadScene(scene); });
            }
            else
            {
                SceneManager.LoadScene(scene);
            }                     
        }

        //Reload scene
        public void ReloadScene()
        {
            Scene scene = SceneManager.GetActiveScene(); 
            SceneManager.LoadScene(scene.name);
        }

        //Close scene
        public void ExitApplication()
        {
            Application.Quit();
        }

        //==================================
        //=========EFFECTS
        //==================================

        //Transition
        protected GameObject transitionOpen = null;
        protected GameObject transitionClose = null;
        protected bool transitionBlock = false;

        //Check
        public bool IsTransition()
        {
            return transitionOpen || transitionClose;
        }

        //Open transition
        public void OpenTransition(UnityAction action)
        {
            if(!transitionOpen)
            {
                //Create
                transitionOpen = Instantiate(PlayerProfile.PlayerTransition.Prefab, uiBackground.transform);

                //Animation
                Animator transitionAnimator = transitionOpen.GetComponent<Animator>();
                transitionAnimator.Play("Open");
                transitionAnimator.speed = 1;

                //Coroutine
                transitionBlock = false;
                StartCoroutine(Technical.Coroutines.Delay(delegate { DestroyOpenTransition(action); }, PlayerProfile.PlayerTransition.Duration));
            }
        }
        
        //Close transition
        public void CloseTransition(UnityAction action)
        {
            if (!transitionClose)
            {
                //Create
                transitionClose = Instantiate(PlayerProfile.PlayerTransition.Prefab, uiBackground.transform);

                //Animation
                Animator transitionAnimator = transitionClose.GetComponent<Animator>();
                transitionAnimator.Play("Close");
                transitionAnimator.speed = 1;

                //Coroutine
                transitionBlock = true;
                StartCoroutine(Technical.Coroutines.Delay(delegate { EndTransition(action); }, PlayerProfile.PlayerTransition.Duration));
            }
        }

        //Freeze transition
        public void FreezeTransition()
        {
            if (!transitionClose)
            {
                //Create
                transitionClose = Instantiate(PlayerProfile.PlayerTransition.Prefab, uiBackground.transform);

                //Animation
                var transitionAnimator = transitionClose.GetComponent<Animator>();
                transitionAnimator.Play("Close", 0, 1.00f);
                transitionAnimator.speed = 0;
            }
        }

        //End transition
        public void EndTransition(UnityAction action)
        {
            if (IsTransition())
            {
                action?.Invoke();
            }
        }

        //Destroy transition
        public void DestroyOpenTransition(UnityAction action)
        {
            if (IsTransition())
            {                
                action?.Invoke();
                Destroy(transitionOpen);
            }
        }
        public void DestroyCloseTransition(UnityAction action)
        {
            if (IsTransition())
            {
                action?.Invoke();
                Destroy(transitionClose);
            }
        }

        //==================================
        //=========FPS
        //==================================

        [SerializeField]
        private int fpsMax = 60;
        public int MaxFPS { get { return fpsMax; } }
    }
}
