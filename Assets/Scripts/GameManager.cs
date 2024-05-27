using System.IO;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    #endregion

    public SaveFile saveFile;
    string filePath;
    public bool gamePaused = false;

    private void Start()
    {
        filePath = Application.persistentDataPath + "/savefiles.txt";
        if (File.Exists(filePath))
            Load();

        PoolSpawner.LevelFinished += nextLevelIndex =>
        {
            saveFile.currentLevel = nextLevelIndex;
            MainMenuDecline();
        };
    }
    public void MainMenuDecline()
    {
        switch (saveFile.currentLevel)
        {
            case 0:
                {
                    SceneLoader.Instance.LoadScene(MyScenes.Lvl1);
                    break;
                }
            case 1:
                {
                    SceneLoader.Instance.LoadScene(MyScenes.Lvl2);
                    break;
                }
            case 2:
                {
                    SceneLoader.Instance.LoadScene(MyScenes.Lvl3);
                    break;
                }
            case 3:
                {
                    saveFile.currentLevel = 0;
                    saveFile.currentDifficulty += 3;
                    SceneLoader.Instance.LoadScene(MyScenes.Lvl1);
                    break;
                }
        }
        SceneLoader.Instance.LoadScene(MyScenes.IngameUI, UnityEngine.SceneManagement.LoadSceneMode.Additive);
    }

    public void Save()
    {

        var data = JsonUtility.ToJson(saveFile);

        using (StreamWriter writer = File.CreateText(filePath))
        {
            writer.Write(data);
        }
    }
    public void Load()
    {
        if (File.Exists(filePath))
        {
            using (StreamReader reader = File.OpenText(filePath))
            {
                var data = reader.ReadToEnd();
                JsonUtility.FromJsonOverwrite(data, saveFile);
            }
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        gamePaused = true;
        SceneLoader.Instance.LoadScene(MyScenes.PauseUI, UnityEngine.SceneManagement.LoadSceneMode.Additive);
    }
    public void UnpauseGame()
    {
        Time.timeScale = 1;
        gamePaused = false;
        SceneLoader.Instance.UnloadScene(MyScenes.PauseUI);
    }
}
