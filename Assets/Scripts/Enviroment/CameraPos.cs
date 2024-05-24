using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPos : MonoBehaviour
{
    private float Y;

    
    // Start is called before the first frame update
    void Start()
    {
        Y = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, Y, transform.position.z);
    }
}
