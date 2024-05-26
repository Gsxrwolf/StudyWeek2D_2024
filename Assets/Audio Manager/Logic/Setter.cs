using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Setter : MonoBehaviour
{
    public BackgroundMusicType BackgroundMusic;
    public bool PlayFoley = true;
    public FoleyType FoleySoundsType;

    void Start()
    {
        AudioManager.Instance.SetBackgroundMusic(BackgroundMusic);

        if(PlayFoley)
        {
            AudioManager.Instance.SetBackgroundFoley(FoleySoundsType);
        }
    }
}
