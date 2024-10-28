using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using TMPro;

namespace LTG.GifGame
{
    //Shop type
    public enum ShopType : ushort
    {
        Transitions = 0,
        Playheads = 1,
        Lifebars = 2
    }

    //Shop component
    public class ShopComponent : MonoBehaviour
    {
        //==================================
        //=========SHOP
        //==================================

        //Shop params
        [SerializeField]
        private ShopType shopType = ShopType.Transitions;
        [SerializeField]
        private float shopDistance = 250.0f;

        //Shop items
        private List<GameObject> shopCells = new List<GameObject>();

        //Shop init
        private void ShopInit()
        {
            //Type
            int index = 0;
            switch (shopType)
            {
                case ShopType.Transitions:
                {
                    foreach (var item in ShopContent.Transitions)
                    {
                        ShopItem(item, ++index);
                    }
                    break;
                }
                case ShopType.Playheads:
                {
                    foreach (var item in ShopContent.Playheads)
                    {
                        ShopItem(item, ++index);
                    }
                    break;
                }
                case ShopType.Lifebars:
                {
                    foreach (var item in ShopContent.Lifebars)
                    {
                        ShopItem(item, ++index);
                    }
                    break;
                }
                default:
                    break;
            }
        }

        //Shop item
        private void ShopItem(Item item, int index)
        {
            //Creation
            GameObject cell = Instantiate(PrefabAssets.UiCell, gameObject.transform);
            shopCells.Add(cell);
            item.Cell = cell;

            //Position
            RectTransform rect = gameObject.GetComponent<RectTransform>();
            cell.transform.localPosition = new Vector3(index * shopDistance - rect.sizeDelta.x / 2.0f, 0, gameObject.transform.position.z);

            //Icon
            GameObject background = cell.transform.Find("Background").gameObject;
            GameObject icon = Instantiate(item.Icon, background.transform);

            //Button
            Button button = icon.GetComponent<Button>();
            button.onClick.AddListener(delegate { OnClickItem(item); });

            //Select
            TMP_Text text = cell.GetComponentInChildren<TMP_Text>();
            if (!IsPurchased(item))
            {
                //Price
                text.text = item.Price.ToString();
            }
            else
            {
                //Price
                text.text = "--";

                //Select
                switch (shopType)
                {
                    case ShopType.Transitions:
                        {
                            if (item.Key == PlayerProfile.PlayerSettings.PlayerTransition)
                            {
                                SelectItem(item);
                            }
                            break;
                        }
                    case ShopType.Playheads:
                        {
                            if (item.Key == PlayerProfile.PlayerSettings.PlayerPlayhead)
                            {
                                SelectItem(item);
                            }
                            break;
                        }
                    case ShopType.Lifebars:
                        {
                            if (item.Key == PlayerProfile.PlayerSettings.PlayerLifebar)
                            {
                                SelectItem(item);
                            }
                            break;
                        }
                    default:
                        break;
                }
            }
        }

        //==================================
        //=========ITEMS
        //==================================

        //Select item
        public void SelectItem(Item item)
        {
            //Color
            foreach (var cell in shopCells)
            {
                Image border = cell.transform.Find("Border").gameObject.GetComponent<Image>();

                if (cell == item.Cell)
                {
                    border.color = Color.red;
                }
                else
                {
                    border.color = Color.white;
                }
            }

            //Type
            switch (shopType)
            {
                case ShopType.Transitions:
                {
                    PlayerProfile.PlayerSettings.PlayerTransition = item.Key;
                    break;
                }
                case ShopType.Playheads:
                {
                    PlayerProfile.PlayerSettings.PlayerPlayhead = item.Key;
                    break;
                }
                case ShopType.Lifebars:
                {
                    PlayerProfile.PlayerSettings.PlayerLifebar = item.Key;
                    break;
                }
                default:
                    break;
            }

            //Load
            PlayerProfile.SetupActiveItems();
        }

        //Buy item
        public void BuyItem(Item item)
        {
            //Money
            PlayerProfile.PlayerSettings.PlayerBalance -= item.Price;

            //Unlock
            UnlockItem(item);

            //Interface            
            ShopInterface.TInstance.SetBalance(PlayerProfile.PlayerSettings.PlayerBalance);
            item.Cell.GetComponentInChildren<TMP_Text>().text = "--";

            //Select
            SelectItem(item);
        }

        //Unlock item
        public void UnlockItem(Item item)
        {
            //Type
            switch (shopType)
            {
                case ShopType.Transitions:
                {
                    PlayerProfile.PlayerSettings.PlayerTransitions.Add(item.Key);
                    break;
                }
                case ShopType.Playheads:
                {
                    PlayerProfile.PlayerSettings.PlayerPlayheads.Add(item.Key);
                    break;
                }
                case ShopType.Lifebars:
                {
                    PlayerProfile.PlayerSettings.PlayerLifebars.Add(item.Key);
                    break;
                }
                default:
                    break;
            }
        }

        //Purchased
        private bool IsPurchased(Item item)
        {
            switch (shopType)
            {
                case ShopType.Transitions:
                {
                    if(PlayerProfile.PlayerSettings.PlayerTransitions.Contains(item.Key))
                    {
                        return true;
                    }
                    break;
                }
                case ShopType.Playheads:
                {
                    if (PlayerProfile.PlayerSettings.PlayerPlayheads.Contains(item.Key))
                    {
                        return true;
                    }
                    break;
                }
                case ShopType.Lifebars:
                {
                    if (PlayerProfile.PlayerSettings.PlayerLifebars.Contains(item.Key))
                    {
                        return true;
                    }
                    break;
                }
                default:
                    break;
            }

            return false;
        }

        //==================================
        //=========EVENTS
        //==================================

        private void OnClickItem(Item item)
        {
            if (IsPurchased(item))
            {
                Technical.Audios.Play(Interface.AudioSource, AudioAssets.ShopSelect, 1.00f);
                SelectItem(item);
            }
            else if (PlayerProfile.PlayerSettings.PlayerBalance >= item.Price)
            {
                Technical.Audios.Play(Interface.AudioSource, AudioAssets.ShopBuy, 1.00f);
                BuyItem(item);
            }
            else
            {
                Technical.Audios.Play(Interface.AudioSource, AudioAssets.ShopNoMoney, 1.00f);
            }
        }

        //==================================
        //=========Unity
        //==================================

        //Scene start
        private void Start()
        {
            ShopInit();
        }
    }
}
