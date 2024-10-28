using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace LTG.GifGame
{
    public class LoadingManager : Technical.SingletonMonoBehaviour<LoadingManager>
    {
        //==================================
        //=========UNITY
        //==================================

        //Scene awake
        private void Awake()
        {
            //Current
            loadedLevel = PlayerProfile.PlayerProgress.GetCurrentLevel();

            //Level
            if(loadedLevel == null)
            {
                ErrorManager.Throw("No level data.");
                return;                
            }

            //Progress
            progressBar.Visual = false;

            //Download
            DownloadLevel();
        }

        //==================================
        //=========UI
        //==================================

        //Progress bar
        [SerializeField]
        private ProgressComponent progressBar;

        //==================================
        //=========LOADING
        //==================================

        //Level
        private static LevelData loadedLevel;
        public static LevelData LoadedLevel { get { return loadedLevel; } }

        //Animation
        private static GameObject loadedAnimation;
        public static GameObject LoadedAnimation { get { return loadedAnimation; } }

        //Hitbox
        private static GameObject loadedHitbox;
        public static GameObject LoadedHitbox { get { return loadedHitbox; } }

        //==================================
        //=========LEVEL
        //==================================

        //Download
        private void DownloadLevel()
        {
            //Progress
            if (progressBar)
            {
                progressBar.ClearTasks();                
                progressBar.Run("Loading level...");
            }

            //Tasks
            int indexAnimation = progressBar.AddTask();
            int indexHitbox = progressBar.AddTask();            

            //Assets            
            StartCoroutine(QueueAnimation(loadedLevel.Animation, indexAnimation));
            StartCoroutine(QueueHitbox(loadedLevel.Hitbox, indexHitbox));
        }

        private void FinishLevel()
        {
            //Progress
            if (progressBar)
            {
                progressBar.Stop("Done!");
            }

            //Config
            PlayerProfile.PlayerConfig.AddLevel(loadedLevel);

            //Load scene
            SceneManager.LoadScene(MasterProfile.GameSettings.LevelScene);
        }

        //==================================
        //=========ANIMATION
        //==================================

        private IEnumerator QueueAnimation(string asset, int index)
        {
            //Download
            AsyncOperationHandle<GameObject> handle = Addressables.LoadAssetAsync<GameObject>(asset);

            //Progress
            while (!handle.IsDone)
            {
                DownloadStatus status = handle.GetDownloadStatus();
                progressBar.ProgressTask(index, status.Percent * 100.00f);
                yield return null;
            }

            //Complete
            FinishAnimation(handle, index);
        }

        private void FinishAnimation(AsyncOperationHandle<GameObject> handle, int index)
        {
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                //Task
                progressBar.CompleteTask(index);

                //Object
                loadedAnimation = handle.Result;

                //Assets
                if (progressBar.Completed)
                {
                    FinishLevel();
                }
            }
            else if (handle.Status == AsyncOperationStatus.Failed)
            {
                ErrorManager.Throw("Asset error.");
            }
        }

        //==================================
        //=========HITBOX
        //==================================

        private IEnumerator QueueHitbox(string asset, int index)
        {
            //Download
            AsyncOperationHandle<GameObject> handle = Addressables.LoadAssetAsync<GameObject>(asset);

            //Progress
            while (!handle.IsDone)
            {
                DownloadStatus status = handle.GetDownloadStatus();
                progressBar.ProgressTask(index, status.Percent * 100.00f);
                yield return null;
            }

            //Complete
            FinishHitbox(handle, index);
        }

        private void FinishHitbox(AsyncOperationHandle<GameObject> handle, int index)
        {
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                //Task
                progressBar.CompleteTask(index);

                //Object
                loadedHitbox = handle.Result;

                //Assets
                if (progressBar.Completed)
                {                    
                    FinishLevel();
                }
            }
            else if (handle.Status == AsyncOperationStatus.Failed)
            {
                ErrorManager.Throw("Asset error.");
            }
        }
    }
}
