using UnityEngine;
using UnityEngine.SceneManagement;

public enum MyScenes
{
    InitScene,
    MainMenu,
    Settings,
    Credits,
    Lvl1,
    Lvl2,
    Lvl3,
    WinScreen, 
    LooseScreen,
    IngameUI,
    PauseUI
}
public class SceneLoader : MonoBehaviour
{
    #region Singleton
    public static SceneLoader Instance { get; private set; }
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

    [SerializeField] private MyScenes startScene = MyScenes.MainMenu;

    private void Start()
    {
        LoadScene(startScene);
    }
    public void LoadScene(MyScenes scene, LoadSceneMode _loadSceneMode = LoadSceneMode.Single)
    {
        SceneManager.LoadSceneAsync((int)scene, _loadSceneMode);
    }
    public void UnloadScene(MyScenes scene)
    {
        if (SceneManager.GetSceneByName(scene.ToString()).isLoaded || SceneManager.GetSceneByName(scene.ToString()).isSubScene)
        {
            SceneManager.UnloadSceneAsync((int)scene);
        }
    }
}
