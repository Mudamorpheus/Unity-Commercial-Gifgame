using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEngine.UI;

using TMPro;

namespace LTG.GifGame
{
    public class ScoreboardComponent : MonoBehaviour
    {
        //==================================
        //=========SCORE
        //==================================

        [System.Serializable]
        public class ScoreItem
        {
            [SerializeField]
            public string Name;
            [SerializeField]
            public int Value;
        }

        [SerializeField]
        private List<ScoreItem> uiScores = new List<ScoreItem>();

        public void Init()
        {
            //Player
            ScoreItem player = new ScoreItem
            {
                Name = PlayerProfile.PlayerName,
                Value = PlayerProfile.PlayerSettings.PlayerScore
            };
            uiScores.Add(player);

            //Sort
            var sorted = uiScores.OrderByDescending(key => key.Value);

            //Values
            int count = sorted.Count();
            int rows = 0;

            //Find player
            int index = 0;
            for (int i = 0; i <= count; i++)
            {
                if (sorted.ElementAt(i).Name == PlayerProfile.PlayerName)
                {
                    index = i;
                    break;
                }
            }

            //Title
            GameObject item;

            item = Instantiate(PrefabAssets.UiScore, gameObject.transform);

            var textpos = item.transform.Find("TextPos").GetComponent<TMP_Text>();
            textpos.text = "¹";
            textpos.color = Color.gray;

            var textname = item.transform.Find("TextName").GetComponent<TMP_Text>();
            textname.text = ". . .";
            textname.color = Color.gray;

            var textscore = item.transform.Find("TextScore").GetComponent<TMP_Text>();
            textscore.text = "S";
            textscore.color = Color.gray;

            rows++;

            //Other
            int max = 1;
            for (int i = index - max; i <= index + max; i++)
            {
                if (i >= 0 && i < count)
                {
                    item = Instantiate(PrefabAssets.UiScore, gameObject.transform);
                    item.transform.Find("TextPos").GetComponent<TMP_Text>().text = (i + 1).ToString();

                    var textnameplr = item.transform.Find("TextName").GetComponent<TMP_Text>();
                    textnameplr.text = sorted.ElementAt(i).Name.ToString();
                    if (i == index)
                    {
                        textnameplr.color = Color.yellow;
                    }

                    var textscoreplr = item.transform.Find("TextScore").GetComponent<TMP_Text>();
                    int points = sorted.ElementAt(i).Value;
                    if (points < 0)
                    {
                        textscoreplr.text = "0";
                    }
                    else
                    {
                        textscoreplr.text = points.ToString();
                    }

                    rows++;
                }
            }

            //Empty
            item = Instantiate(PrefabAssets.UiScore, gameObject.transform);
            item.transform.Find("TextPos").GetComponent<TMP_Text>().text = "";
            item.transform.Find("TextName").GetComponent<TMP_Text>().text = ". . .";
            item.transform.Find("TextScore").GetComponent<TMP_Text>().text = "";
            rows++;
        }
    }
}
