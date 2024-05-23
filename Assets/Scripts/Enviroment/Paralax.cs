using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paralax : MonoBehaviour
{
    private float length;
    [SerializeField] private float lengthMultiplier;
    [SerializeField] private float startPos;
    [SerializeField] public GameObject cam;
    public float parallexEffect;

    void Start()
    {
        startPos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void Update()
    {

        float temp = (cam.transform.position.x * (1 - parallexEffect));
        float dist = ((cam.transform.position.x) * parallexEffect);

        transform.position = new Vector2(startPos + dist, transform.position.y);

        if (temp > startPos + length * lengthMultiplier)
        {
            startPos += length * lengthMultiplier;
        }
        else if (temp < startPos - length * lengthMultiplier)
        {
            startPos -= length * lengthMultiplier;
        }

    }
}
