using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AddressableAssets;
using UnityEngine.AddressableAssets.ResourceLocators;
using UnityEngine.ResourceManagement.ResourceLocations;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace LTG.GifGame
{
    public class BackgroundManager : Technical.SingletonMonoBehaviour<BackgroundManager>
    {
        //==================================
        //=========UNITY
        //==================================

        //Scene awake
        private void Awake()
        {
            //Metascene
            DontDestroyOnLoad(gameObject);
        }

        //==================================
        //=========LEVEL
        //==================================

        //Download
        public static void DownloadAsset(string asset, string level)
        {           
            if(Instance) { Instance.StartCoroutine(QueueAsset(asset, level)); }            
        }
        private static IEnumerator QueueAsset(string asset, string level)
        {
            var handle = Addressables.LoadAssetAsync<GameObject>(asset);

            while (!handle.IsDone)
            {
                yield return null;
            }

            //Complete
            FinishAsset(handle, level);
        }
        private static void FinishAsset(AsyncOperationHandle<GameObject> handle, string level)
        {
            //Config            
            PlayerProfile.PlayerConfig.AddLevel(level);

            //Debug
            Debug.Log("Background loader: " + handle.Result.name + " is loaded.");
        }
    }
}
