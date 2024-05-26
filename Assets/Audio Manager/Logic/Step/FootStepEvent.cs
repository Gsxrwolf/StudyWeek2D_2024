using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootStepEvent : MonoBehaviour
{
    public void FootStepAnimEvent()
    {
        AudioManager.Instance.PlayWalkingSound();
    }
}
