using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    Vector3 test;
    void Start()
    {
    }

    void Update()
    {
        test = GameObject.FindWithTag("Player").transform.position;

        transform.position = new Vector3(test.x, 0, transform.position.z);
    }
}
