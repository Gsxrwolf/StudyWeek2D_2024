using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    private void Start()
    {
        Load();
    }

    public void Save()
    {
        var filePath = Application.persistentDataPath + "/savefiles.txt";
        var data = JsonUtility.ToJson(saveFile);

        using (StreamWriter writer = File.CreateText(filePath))
        {
            writer.Write(data);
        }
    }
    public void Load()
    {
        var filePath = Application.persistentDataPath + "/savefiles.txt";

        using (StreamReader reader = File.OpenText(filePath))
        {
            var data = reader.ReadToEnd();
            JsonUtility.FromJsonOverwrite(data, saveFile);
        }
    }
}
