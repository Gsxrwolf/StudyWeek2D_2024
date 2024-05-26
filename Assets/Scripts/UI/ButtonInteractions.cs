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
        AudioManager.Instance.PlayUISound(SoundType.ButtonClick);
    }

    public void OnCreditsClick()
    {
        SceneLoader.Instance.LoadScene(MyScenes.Credits);
        AudioManager.Instance.PlayUISound(SoundType.ButtonClick);
    }

    public void OnQuitClick()
    {
        AudioManager.Instance.PlayUISound(SoundType.ButtonClick);

        EditorApplication.ExitPlaymode();
        GameManager.Instance.Save();
    }

    public void OnBackClick()
    {
        SceneLoader.Instance.LoadScene(MyScenes.MainMenu);
        SceneLoader.Instance.UnloadScene(MyScenes.IngameUI);
        GameManager.Instance.Save();

        AudioManager.Instance.PlayUISound(SoundType.ButtonClick);
    }

    public void MainMenuAccept()
    {
        AudioManager.Instance.PlayUISound(SoundType.HandStamp);

        SceneLoader.Instance.LoadScene(MyScenes.LooseScreen);
    }

    public void MainMenuDecline()
    {
        AudioManager.Instance.PlayUISound(SoundType.HandStamp);

        GameManager.Instance.MainMenuDecline();
    }
}
