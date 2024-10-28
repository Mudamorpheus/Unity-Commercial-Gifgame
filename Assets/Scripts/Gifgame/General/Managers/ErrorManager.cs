using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

using TMPro;

namespace LTG.GifGame
{
    public class ErrorManager : Technical.SingletonMonoBehaviour<ErrorManager>
    {
        //==================================
        //=========UI
        //==================================

        [SerializeField]
        private GameObject uiCanvas;
        public static GameObject Canvas { get { return Instance.uiCanvas; } }

        //Reload scene
        public static void ReloadScene()
        {
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }

        //Close scene
        public static void ExitApplication()
        {
            Application.Quit();
        }

        //==================================
        //=========ERROR
        //==================================

        //Error
        public static void Throw(string message)
        {
            GameObject box = Instantiate(PrefabAssets.UiErrorBox, Instance.uiCanvas.transform);

            //Message
            var objectText = box.transform.Find("Message");
            if (objectText)
            {
                var text = objectText.GetComponent<TMP_Text>();
                text.text = message;
            }

            //Retry button
            var objectRetry = box.transform.Find("ButtonRetry");
            if (objectRetry)
            {
                var buttonRetry = objectRetry.GetComponent<Button>();
                if (buttonRetry)
                {
                    buttonRetry.onClick.AddListener(ReloadScene);
                }
            }

            //Exit button
            var objectExit = box.transform.Find("ButtonExit");
            if (objectRetry)
            {
                var buttonExit = objectExit.GetComponent<Button>();
                if (buttonExit)
                {
                    buttonExit.onClick.AddListener(ExitApplication);
                }
            }
        }
    }
}
