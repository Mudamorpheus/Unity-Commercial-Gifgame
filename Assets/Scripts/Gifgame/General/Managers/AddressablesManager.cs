using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AddressableAssets;
using UnityEngine.AddressableAssets.ResourceLocators;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;

namespace LTG.GifGame
{
    [RequireComponent(typeof(AudioSource))]
    public class AddressablesManager : Technical.SingletonMonoBehaviour<AddressablesManager>
    {
        //==================================
        //=========ADDRESSABLES
        //==================================

        //Locations
        public static List<IResourceLocation> AllLocations()
        {
            var allLocations = new List<IResourceLocation>();

            foreach (var resourceLocator in Addressables.ResourceLocators)
            {
                if (resourceLocator is ResourceLocationMap map)
                {
                    foreach (var locations in map.Locations.Values)
                    {
                        allLocations.AddRange(locations);
                    }
                }
            }

            return allLocations;
        }

        //Show
        public static void ShowLocations()
        {
            foreach (var locator in Addressables.ResourceLocators)
            {
                if (locator is ResourceLocationMap map)
                {
                    foreach (var locations in map.Locations.Values)
                    {
                        foreach (var location in locations)
                        {
                            Debug.Log(location.InternalId);
                        }
                    }
                }
            }
        }

        //Clear
        public static void ClearLocations()
        {
            foreach (var locator in Addressables.ResourceLocators)
            {
                if (locator is ResourceLocationMap map)
                {
                    foreach (var locations in map.Locations.Values)
                    {
                        foreach(var location in locations)
                        {
                            Addressables.Release(location);
                        }
                    }
                }
            }
        }

        //==================================
        //=========UNITY
        //==================================

        //Scene start
        private void Start()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
