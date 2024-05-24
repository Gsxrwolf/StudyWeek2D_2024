using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class IngameMapping : MonoBehaviour
{
    [SerializeField]
    private GameObject[] healthArray;
    [SerializeField]
    private GameObject hpBuff;
    [SerializeField]
    private GameObject dmgBuff;
    [SerializeField]
    private GameObject speedBuff;
    [SerializeField]
    private GameObject jumpBuff;

    // Start is called before the first frame update
    void Start()
    {
        PlayerController.OnHealthChange += NewHealth;
        ItemEvents.BuffStarted += ShowBuffs;
        ItemEvents.BuffEnd += HideBuffs;
        
    }

    private void OnDisable()
    {
        PlayerController.OnHealthChange -= NewHealth;
        ItemEvents.BuffStarted -= ShowBuffs;
        ItemEvents.BuffEnd -= HideBuffs;
    }

    private void NewHealth(int _new)
    {
        foreach (GameObject healthItem in healthArray)
        {
            healthItem.SetActive(true);
        }
        for (int i = _new; i < healthArray.Length; i++)
        {
            healthArray[i].SetActive(false);
        }
    }

    private void ShowBuffs(int _collectIndex)
    {
        switch(_collectIndex)
        {
            case 1:
                {
                    hpBuff.SetActive(true);
                    break;
                }
            case 2:
                {
                    dmgBuff.SetActive(true);
                    break;
                }
            case 3:
                {
                    speedBuff.SetActive(true);
                    break;
                }
            case 4:
                {
                    jumpBuff.SetActive(true);
                    break;
                }
        }
    }
    private void HideBuffs(int _collectIndex)
    {
        switch (_collectIndex)
        {
            case 1:
                {
                    hpBuff.SetActive(false);
                    break;
                }
            case 2:
                {
                    dmgBuff.SetActive(false);
                    break;
                }
            case 3:
                {
                    speedBuff.SetActive(false);
                    break;
                }
            case 4:
                {
                    jumpBuff.SetActive(false);
                    break;
                }
        }
    }
}
