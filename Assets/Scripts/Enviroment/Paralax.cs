using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paralax : MonoBehaviour
{
    private float length;
    [SerializeField] private float lengthMultipliar;
    [SerializeField] private float startPosX;
    [SerializeField] private float startPosY;
    [SerializeField] public GameObject cam;
    public float parallexEffect;

    void Start()
    {
        startPosX = transform.position.x;
        startPosY = transform.position.y;
        length = GetComponent<SpriteRenderer>().bounds.size.x * lengthMultipliar;
    }

    void LateUpdate()
    {
        
        float temp = (cam.transform.position.x * (1 - parallexEffect));
        float dist = ((cam.transform.position.x) * parallexEffect);

        transform.position = new Vector2(startPosX + dist, startPosY);

        if (temp > startPosX + length)
        {
            startPosX += length;
        }
        else if (temp < startPosX - length )
        {
            startPosX -= length;
        }

    }
}
