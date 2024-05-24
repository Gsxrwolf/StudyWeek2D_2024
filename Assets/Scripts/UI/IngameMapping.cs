using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class IngameMapping : MonoBehaviour
{
    [SerializeField]
    private GameObject[] healthArray;

    // Start is called before the first frame update
    void Start()
    {
        PlayerController.OnHealthChange += NewHealth;
    }

    private void OnDisable()
    {
        PlayerController.OnHealthChange -= NewHealth;
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
}
