using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandControl : MonoBehaviour
{
    public Transform[] thumb = new Transform[2]; //constraint: 0~80
    public Transform[] index = new Transform[3]; //constraint: -45~80, -10~90, -10~90
    public Transform[] middle = new Transform[3];

    public float[] flex_values = new float[3];
    void Start()
    {

    }

    void Update()
    {
        thumb[1].localRotation = Quaternion.Euler(flex_values[0], -flex_values[0] - 17, flex_values[0]);
        for (int i = 1; i < 3; i++)
        {
            index[i].localRotation = Quaternion.Euler(index[i].localRotation.eulerAngles.x, index[i].localRotation.eulerAngles.y, flex_values[1]);
            middle[i].localRotation = Quaternion.Euler(middle[i].localRotation.eulerAngles.x, middle[i].localRotation.eulerAngles.y, flex_values[2]);
        }
    }
}
