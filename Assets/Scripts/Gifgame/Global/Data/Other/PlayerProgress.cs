using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Newtonsoft.Json;

namespace LTG.GifGame
{
    [System.Serializable]
    public class ProgressData
    {        
        [System.Serializable]
        public enum StateData
        {
            Default,
            Current,
            Victory,
            Defeat,            
        }

        [SerializeField]
        private string key;
        public string Key { set { key = value; } get { return key; } }

        [SerializeField]
        private StateData state = StateData.Default;
        public StateData State { set { state = value; } get { return state; } }

        public ProgressData(string key, StateData state)
        {
            this.key = key;
            this.state = state;
        }
    }

    [System.Serializable]
    public class PlayerProgress
    {
        //==================================
        //=========PROGRESS
        //==================================

        //Counts
        [SerializeField]
        private const int prevMax = 10;
        public int PrevMax { get { return prevMax; } }
        private const int afterMax = 5;
        public int AfterMax { get { return afterMax; } }

        //Config
        [SerializeField]
        private List<ProgressData> prevLevels = new List<ProgressData>();
        public List<ProgressData> PrevLevels { set { prevLevels = value; } get { return prevLevels; } }
        [SerializeField]
        private List<ProgressData> afterLevels = new List<ProgressData>();
        public List<ProgressData> AfterLevels { set { afterLevels = value; } get { return afterLevels; } }

        public void Reset()
        {
            prevLevels.Clear();
            afterLevels.Clear();
        }

        public void AddPrev(ProgressData progress)
        {
            //Add
            prevLevels.Add(progress);

            //Limit
            if (prevLevels.Count > prevMax)
            {
                prevLevels.RemoveAt(0);
            }
        }
        public void AddAfter(ProgressData progress)
        {
            //Add
            afterLevels.Add(progress);

            //Limit
            if (afterLevels.Count > afterMax)
            {
                afterLevels.RemoveAt(0);
            }
        }

        public void RemovePrev(ProgressData progress)
        {
            //Remove
            prevLevels.Remove(progress);
        }
        public void RemoveAfter(ProgressData progress)
        {
            //Remove
            afterLevels.Remove(progress);
        }

        public void GenerateProgress()
        {
            for (int i = 0; i < afterMax; i++)
            {
                //Random
                var level = LoadRandomLevel();
                var state = ProgressData.StateData.Default;

                //Current
                if (afterLevels.Count == 0)
                {
                    state = ProgressData.StateData.Current;
                }

                //Add
                var progress = new ProgressData(level.Key, state);
                AddAfter(progress);
            }
        }

        public void FillProgress(bool load)
        {
            //Count of levels
            int fillLevels = afterMax - afterLevels.Count;

            //Add new
            for (int i = 0; i < fillLevels; i++)
            {
                //Random
                var level = LoadRandomLevel();
                var state = ProgressData.StateData.Default;

                //Load
                if(load)
                {
                    BackgroundManager.DownloadAsset(level.Animation, level.Key);
                    BackgroundManager.DownloadAsset(level.Hitbox, level.Key);
                }                

                //Add
                var progress = new ProgressData(level.Key, state);
                AddAfter(progress);
            }
        }

        //Random
        public LevelData LoadRandomLevel()
        {
            if (Application.internetReachability == NetworkReachability.NotReachable)
            {
                return LevelsContent.GetRandomLoadedLevel();
            }
            else
            {
                return LevelsContent.GetRandomLevel();
            }
        }

        //Current
        public LevelData GetCurrentLevel()
        {
            if(AfterLevels.Count > 0)
            {
                var progress = AfterLevels[0];
                var level = LevelsContent.Levels.Find(lvl => lvl.Key == progress.Key);
                return level;
            }
            else
            {
                return null;
            }            
        }
        public LevelData GetLastLevel()
        {
            if (AfterLevels.Count > 0)
            {
                var progress = AfterLevels[AfterLevels.Count-1];
                var level = LevelsContent.Levels.Find(lvl => lvl.Key == progress.Key);
                return level;
            }
            else
            {
                return null;
            }
        }
        public List<LevelData> GetLastLevels(int count)
        {
            var exclude = new List<LevelData>();

            for(int i = AfterLevels.Count-1; i >= 0; i--)
            {
                if(count <= 0)
                {
                    break;
                }

                var progress = AfterLevels[i];
                var level = LevelsContent.Levels.Find(lvl => lvl.Key == progress.Key);

                exclude.Add(level);

                count--;
            }

            return exclude;
        }
        public ProgressData GetCurrentProgress()
        {
            return AfterLevels.Find(prg => prg.State == ProgressData.StateData.Current);
        }

        //==================================
        //=========GAMEPLAY
        //==================================

        public void Victory()
        {
            Next(ProgressData.StateData.Victory);
        }

        public void Defeat()
        {
            Next(ProgressData.StateData.Defeat);
        }

        public void Next(ProgressData.StateData state)
        {
            //Old
            var current = GetCurrentProgress();
            current.State = state;

            //Move
            AddPrev(current);
            RemoveAfter(current);

            //Fill
            FillProgress(true);

            //New
            var next = AfterLevels[0];
            next.State = ProgressData.StateData.Current;
        }
    }
}
