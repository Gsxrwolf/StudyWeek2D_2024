using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ButtonInteractions : MonoBehaviour
{
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
        GameManager.Instance.Save();
    }

    public void MainMenuAccept()
    {
        SceneLoader.Instance.LoadScene(MyScenes.LooseScreen);
    }

    public void MainMenuDecline()
    {
        switch (GameManager.Instance.saveFile.currentLevel)
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
        }
    }
}
