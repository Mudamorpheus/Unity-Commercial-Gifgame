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
    public class InitManager : Technical.SingletonMonoBehaviour<InitManager>
    {
        //==================================
        //=========UNITY
        //==================================

        //Scene awake
        private void Awake()
        {
            //Levels            
            LevelsContent.Init();

            //Player (TMP)
            PlayerProfile.LoadProfileJson();

            //Addressables
            AddressablesInit();
        }

        //==================================
        //=========LOAD
        //==================================

        //Startpoint
        private void AddressablesInit()
        {
            DownloadCatalogs();
        }

        //Endpoint
        private void AddressablesFinish()
        {
            //Progress
            if (progressBar)
            {
                progressBar.Stop("Done!");
            }

            //Configs            
            PlayerProfile.PlayerConfig.SaveConfigsJson();

            //Load scene
            SceneManager.LoadScene(MasterProfile.GameSettings.MenuScene);
        }

        //==================================
        //=========UI
        //==================================

        //Progress bar
        [SerializeField]        
        private ProgressComponent progressBar;

        //==================================
        //=========CATALOGS
        //==================================

        [SerializeField]
        private List<string> catalogPaths;

        private void DownloadCatalogs()
        {           
            //Progress
            if (progressBar)
            {
                progressBar.ClearTasks();
                progressBar.Run("Loading catalogs...");
            }

            //Catalogs
            foreach (string catalog in catalogPaths)
            {
                DownloadCatalog(catalog);
            }
        }

        private void DownloadCatalog(string catalog)
        {
            //Load config                        
            PlayerProfile.PlayerConfig.LoadConfigJson(catalog);
            bool result = PlayerProfile.PlayerConfig.IsCatalogVerified(catalog);
            bool network = Application.internetReachability != NetworkReachability.NotReachable;

            //Not verified
            if (!result)
            {
                //Cache
                Caching.ClearCache();

                //Network
                if(!network)
                {
                    ErrorManager.Throw("No connection.");
                    return;
                }
            }

            //Progress
            int index = progressBar.AddTask();

            //Load catalog
            StartCoroutine(QueueCatalog(catalog, index));            
        }

        private IEnumerator QueueCatalog(string catalog, int index)
        {
            //Download
            AsyncOperationHandle<IResourceLocator> handle = Addressables.LoadContentCatalogAsync(catalog);

            //Progress
            while (!handle.IsDone)
            {
                DownloadStatus status = handle.GetDownloadStatus();
                progressBar.ProgressTask(index, status.Percent * 100.00f);
                yield return null;
            }

            //Complete
            FinishCatalog(handle, catalog, index);       
        }

        private void FinishCatalog(AsyncOperationHandle<IResourceLocator> handle, string catalog, int index)
        {
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                //Task
                progressBar.CompleteTask(index);                

                //Pull                
                PullCatalog(catalog, handle.Result);

                //Assets
                if (progressBar.Completed)
                {
                    DownloadAssets();
                }
            }
            else if (handle.Status == AsyncOperationStatus.Failed)
            {
                ErrorManager.Throw("Catalog error.");
            }
        }

        private void PullCatalog(string catalog, IResourceLocator locator)
        {
            //Paths
            string folder = @"Assets/Assets/Graphic/Addressables/Levels/";
            string extension = "_video.prefab";

            //Pull
            var keys = locator.Keys;
            string version = PlayerProfile.PlayerVersion;

            //Levels
            List<LevelData> levels = new List<LevelData>();

            //Assets
            foreach (var key in keys)
            {
                //Key
                string path = key.ToString();

                //Level
                if (Technical.Text.EdgesWith(path, folder, extension))
                {
                    //Name
                    string name = path;
                    name = Technical.Text.TrimStart(name, folder);
                    name = Technical.Text.TrimEnd(name, extension);

                    //Animation
                    IResourceLocation animationLocation;
                    string animationPath = folder + name + "_video.prefab";
                    if (locator.Locate(animationPath, typeof(GameObject), out var animationLocations))
                    {
                        animationLocation = animationLocations[0];
                    }
                    else
                    {
                        ErrorManager.Throw("Location error.\n" + animationPath);
                        return;
                    }

                    //Hitbox  
                    IResourceLocation hitboxLocation;
                    string hitboxPath = folder + name + "_hitbox.prefab";
                    if (locator.Locate(hitboxPath, typeof(GameObject), out var hitboxLocations))
                    {
                        hitboxLocation = hitboxLocations[0];
                    }
                    else
                    {
                        ErrorManager.Throw("Location error.\n" + hitboxPath);
                        return;
                    }

                    //Add level
                    LevelData data = new LevelData
                    {
                        Key = path,
                        Animation = animationLocation.PrimaryKey,
                        Hitbox = hitboxLocation.PrimaryKey,
                        Catalog = catalog
                    };

                    //Level lists
                    LevelsContent.Add(data);
                }
            }

            //Config            
            PlayerProfile.PlayerConfig.AddCatalog(catalog, version, levels);
        }

        //==================================
        //=========ASSETS
        //==================================        

        private void DownloadAssets()
        {
            //Progress
            if (progressBar)
            {
                progressBar.ClearTasks();
                progressBar.Run("Loading assets...");
            }

            //Levels
            if(PlayerProfile.PlayerProgress.AfterLevels.Count == 0)
            {
                PlayerProfile.PlayerProgress.GenerateProgress();
            }            

            //Assets
            for(int i = 0; i < PlayerProfile.PlayerProgress.AfterLevels.Count; i++)
            {
                //Check
                bool check = false;
                if(i == 0)
                {
                    check = true;
                }

                //Progress
                var progress = PlayerProfile.PlayerProgress.AfterLevels[i];

                //Coroutine
                int index = progressBar.AddTask();
                StartCoroutine(QueueAsset(progress.Key, index, check));
            }
        }

        private IEnumerator QueueAsset(string asset, int index, bool check)
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
            FinishAsset(handle, asset, index, check);     
        }

        private void FinishAsset(AsyncOperationHandle<GameObject> handle, string level, int index, bool check)
        {
            if (handle.Status == AsyncOperationStatus.Succeeded || !check)
            {
                //Task
                progressBar.CompleteTask(index);

                //Config
                PlayerProfile.PlayerConfig.AddLevel(level);

                //Assets
                if (progressBar.Completed)
                {
                    AddressablesFinish();
                }
            }
            else if (handle.Status == AsyncOperationStatus.Failed)
            {
                ErrorManager.Throw("Asset error.");
            }
        }
    }
}
