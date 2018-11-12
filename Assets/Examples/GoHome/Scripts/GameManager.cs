using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Xml serialization
using System.Xml.Serialization;
//Saving to computer via input + output
using System.IO;


namespace GoHome
{
    [System.Serializable]
    public class GameData
    {
        public int score;
        public int level;
    }
    public class GameManager : MonoBehaviour
    {
        #region Singleton
        public static GameManager Instance;
        private void Awake()
        {
            Load();
            Instance = this;
        }

        private void OnDestroy()
        {
            Save();
            Instance = null;
        }

        public static GameData GetData()
        {
            return Instance.data;
        }
        #endregion

        public GameData data = new GameData();
        public string fileName = "GameSave";

        public int currentScore = 0;
        public int currentLevel = 0;
        public bool isGameRunning = true;
        public Transform levelContainer;

        private Level[] levels;

        private void Start()
        {
            //Get all levels within level container
            levels = levelContainer.GetComponentsInChildren<Level>(true);
            //Set level to current
            SetLevel(currentLevel);
        }
        public void GameOver()
        {

        }
        public void EndGame()
        {

        }
        public void SetLevel(int levelIndex)
        {
            for (int i = 0; i < levels.Length; i++)
            {
                GameObject level = levels[i].gameObject;
                level.SetActive(false); //Disable level
                //Is current index (i) the same as levelIndex?
                if(i == levelIndex)
                {
                    //Enable that level instead
                    level.SetActive(true);
                }
            }
        }

        //Switches to the next level when called
        public void NextLevel()
        {
            //Increase currentLevel
            currentLevel++;
            //If currentLevel exceeds level length
            if(currentLevel >= levels.Length)
            {
                //EndGame
            }
            else
            {
                //Set current level
                SetLevel(currentLevel);
            }
        }
     
        public void NewGame()
        {
            //Get current scene
            Scene currentScene = SceneManager.GetActiveScene();
            //Reload it
            SceneManager.LoadScene(currentScene.name);
        }

        public void Save()
        {
            data.score = currentScore;
            data.level = currentLevel;

            // C:/Users/Manny/AppData/Local/CompanyName/ProductName/GameSave.xml
            string fullPath = Application.dataPath + "/Examples/GoHome/Data/" + fileName + ".json";
            //data.Save(fullPath);
            string json = JsonUtility.ToJson(data);
            File.WriteAllText(fullPath, json);

            //Display where the file saved
            print("Saved to path: " + fullPath);
        }

        public void Load()
        {
            // C:/Users/Manny/AppData/Local/CompanyName/ProductName/GameSave.xml
            string fullPath = Application.dataPath + "/Examples/GoHome/Data/" + fileName + ".json";
            //data.Load(fullPath);
            string json = File.ReadAllText(fullPath);
            data = JsonUtility.FromJson<GameData>(json);

            currentScore = data.score;
            currentLevel = data.level;

            //Display where the file saved
            print("Loaded from path: " + fullPath);
        }

        public static bool Exists()
        {
            string fullPath = Application.dataPath + "/Examples/GoHome/Data/" + Instance.fileName + ".json";
            return File.Exists(fullPath);
        }

    }
}
