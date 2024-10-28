using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

namespace LTG.GifGame
{
    [RequireComponent(typeof(GridLayoutGroup))]
    public class LvlListComponent : MonoBehaviour
    {
        //==================================
        //=========UI
        //==================================

        //Sprites
        [SerializeField]
        private Sprite defaultSprite;
        [SerializeField]
        private Sprite currentSprite;
        [SerializeField]
        private Sprite victorySprite;
        [SerializeField]
        private Sprite defeatSprite;

        //Grid
        private GridLayoutGroup gridGroup;

        //Focus
        private GameObject focusObject;

        private void CreateIcons()
        {
            //Icons
            foreach (var progress in PlayerProfile.PlayerProgress.PrevLevels)
            {
                CreateIcon(progress);
            }
            foreach (var progress in PlayerProfile.PlayerProgress.AfterLevels)
            {
                CreateIcon(progress);
            }

            //Shift
            int index = focusObject.transform.GetSiblingIndex();
            float shift = (gridGroup.cellSize.x + gridGroup.spacing.x) * index;

            //Position
            Vector2 position = new Vector2(gameObject.transform.localPosition.x - shift, gameObject.transform.localPosition.y);
            gameObject.transform.localPosition = position;
        }

        private void CreateIcon(ProgressData progress)
        {
            //Object
            var icon = Instantiate(PrefabAssets.UiLevel, gameObject.transform);
            var image = icon.GetComponent<Image>();

            //Exit
            if(image == null)
            {
                return;
            }

            //Focus
            if(progress.State == ProgressData.StateData.Current)
            {
                focusObject = icon;
            }

            //Sprite
            switch (progress.State)
            {
                //Default
                case ProgressData.StateData.Default:
                {
                    image.sprite = defaultSprite;
                    break;
                }
                //Current
                case ProgressData.StateData.Current:
                {
                    image.sprite = currentSprite;
                    break;
                }
                //Victory
                case ProgressData.StateData.Victory:
                {
                    image.sprite = victorySprite;
                    break;
                }
                //Defeat
                case ProgressData.StateData.Defeat:
                {
                    image.sprite = defeatSprite;
                    break;
                }
                
                //Default
                default:
                {
                    image.sprite = defaultSprite;
                    break;
                }
            }
        }

        //==================================
        //=========UNITY
        //==================================

        private void Awake()
        {
            //Grid
            gridGroup = GetComponent<GridLayoutGroup>();

            //Icons
            CreateIcons();
        }
    }
}
