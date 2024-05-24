using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ButtonInteractions : MonoBehaviour
{
    private void Start()
    {
        PoolSpawner.LevelFinished += nextLevelIndex =>
        {
            if(nextLevelIndex == 3)
            {
                SceneLoader.Instance.UnloadScene(MyScenes.IngameUI);
                SceneLoader.Instance.LoadScene(MyScenes.WinScreen);
            }
        };
    }
    public void OnSettingsClick()
    {
        SceneLoader.Instance.LoadScene(MyScenes.Settings);
    }

    public void OnCreditsClick()
    {
        SceneLoader.Instance.LoadScene(MyScenes.Credits);
    }

    public void OnQuitClick()
    {
        EditorApplication.ExitPlaymode();
        GameManager.Instance.Save();
    }

    public void OnBackClick()
    {
        SceneLoader.Instance.LoadScene(MyScenes.MainMenu);
        SceneLoader.Instance.UnloadScene(MyScenes.IngameUI);
        GameManager.Instance.Save();
    }

    public void MainMenuAccept()
    {
        SceneLoader.Instance.LoadScene(MyScenes.LooseScreen);
    }

    public void MainMenuDecline()
    {
        GameManager.Instance.MainMenuDecline();
    }
}
