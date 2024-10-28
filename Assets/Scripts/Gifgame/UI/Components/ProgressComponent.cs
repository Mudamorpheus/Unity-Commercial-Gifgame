using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using TMPro;

namespace LTG.GifGame
{
    public class ProgressComponent : MonoBehaviour
    {
        //==================================
        //=========UI
        //==================================

        //Elements
        [SerializeField]
        private Animator uiAnimator;
        [SerializeField]
        private TMP_Text uiPercent;
        [SerializeField]
        private TMP_Text uiText;

        //Logic
        private bool uiVisual = true;
        public bool Visual { set { uiVisual = value; } get { return uiVisual; } }

        //Progress
        private float uiProgress = 0.0f;
        public float Progress { set { uiProgress = value; uiPercent.text = value.ToString() + "%"; } get { return uiProgress; } }

        //Run
        public void Run(string message)
        {
            if(uiVisual)
            {
                uiAnimator.Play("On");
                uiPercent.text = "0%";
                uiText.text = message;
            }            
        }

        //Stop
        public void Stop(string message)
        {
            if (uiVisual)
            {
                uiAnimator.Play("Off");
                uiPercent.text = "100%";
                uiText.text = message;
            }
        }

        //Reset
        public void Calculate()
        {           
            //Calculate
            float sum = 0f;
            float max = taskProgress.Count * 100f;
            float result;

            foreach (var progress in taskProgress)
            {
                sum += progress;
            }

            result = sum / max * 100f;

            //Progress
            if (uiVisual)
            {
                uiPercent.text = ((int)result).ToString() + "%";
            }
        }

        //==================================
        //=========TASKS
        //==================================

        //Tasks
        private int taskCount = 0;
        private List<float> taskProgress = new List<float>();
        private List<bool> taskList = new List<bool>();

        //Flag
        private bool taskCompleted = false;
        public bool Completed { set { taskCompleted = value; } get { return taskCompleted; } }

        //Add
        public int AddTask()
        {
            //Counter
            taskCount++;
            taskProgress.Add(0f);
            taskList.Add(false);

            //Result
            return taskCount-1;
        }

        //Progress
        public void ProgressTask(int index, float progress)
        {
            //Progress
            taskProgress[index] = progress;

            //Recalculate
            Calculate();
        }

        //Complete
        public void CompleteTask(int index)
        {
            if (index < taskCount)
            {
                //Counter
                taskProgress[index] = 100f;
                taskList[index] = true;

                //Flag
                bool result = true;
                foreach (var task in taskList)
                {
                    if (!task)
                    {
                        result = false;
                        break;
                    }
                }
                taskCompleted = result;

                //Progress
                Calculate();
            }
        }

        //Clear
        public void ClearTasks()
        {
            taskCount = 0;
            taskProgress.Clear();
            taskList.Clear();
            taskCompleted = false;
        }
    }
}
