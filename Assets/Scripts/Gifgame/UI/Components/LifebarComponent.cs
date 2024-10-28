using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

namespace LTG.GifGame
{
    public class LifebarComponent : MonoBehaviour
    {
        //==================================
        //=========UNITY
        //==================================

        private void Awake()
        {
            //Create lifebars
            for(int i = 0; i < lifebarMax; i++)
            {
                CreateLifebar();
            }
        }

        //==================================
        //=========LIFEBARS
        //==================================

        //Params
        [SerializeField]
        private int lifebarMax = 3;        
        private int lifebarHealth = 3;
        public int Lives { get { return lifebarHealth; } }

        //List
        private List<GameObject> lifebarList = new List<GameObject>();

        private void CreateLifebar()
        {
            //Params
            var prefab = PrefabAssets.UiLifebar;

            //Playhead
            var lifebar = Instantiate(prefab, gameObject.transform);

            //Components
            var image = lifebar.GetComponent<Image>();
            image.sprite = PlayerProfile.PlayerLifebar.LifebarOn;

            //List
            lifebarList.Add(lifebar);
        }

        //==================================
        //=========HEALTH
        //==================================

        public void Damage(int damage)
        {
            //Health
            lifebarHealth -= damage;

            //Textures
            for(int i = 0; i < lifebarMax; i++)
            {
                var lifebar = lifebarList[i];
                var image = lifebar.GetComponent<Image>();
                image.sprite = i <= lifebarHealth-1 ? PlayerProfile.PlayerLifebar.LifebarOn : PlayerProfile.PlayerLifebar.LifebarOff;
            }
        }

        public void Heal(int heal)
        {
            //Health
            lifebarHealth += heal;

            //Textures
            for (int i = 0; i < lifebarMax; i++)
            {
                var lifebar = lifebarList[i];
                var image = lifebar.GetComponent<Image>();
                image.sprite = i <= lifebarHealth - 1 ? PlayerProfile.PlayerLifebar.LifebarOn : PlayerProfile.PlayerLifebar.LifebarOff;
            }
        }
    }
}
