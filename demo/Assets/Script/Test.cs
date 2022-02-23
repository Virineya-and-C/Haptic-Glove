using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public Peripheral peripheral;

    void Start()
    {
        
    }

    void Update()
    {
        transform.position = new Vector3(
            mapper1(peripheral.readings[0]),
            transform.position.y,
            transform.position.z);
    }

    float mapper1(float x)
    {
        x = (x - 790) / 100;
        return x;
    }
}
