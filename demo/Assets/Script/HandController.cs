using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour
{
    const string FINGER1_JOINT1 = "L_Hand_Thumb_1";
    const string FINGER1_JOINT2 = "L_Hand_Thumb_2";
    const string FINGER2_JOINT1 = "L_Hand_Index_1";
    const string FINGER2_JOINT2 = "L_Hand_Index_2";
    const string FINGER2_JOINT3 = "L_Hand_Index_3";
    const string FINGER3_JOINT1 = "L_Hand_Middle_1";
    const string FINGER3_JOINT2 = "L_Hand_Middle_2";
    const string FINGER3_JOINT3 = "L_Hand_Middle_3";

    public Transform[] finger1 = new Transform[2]; //constraint: 0~80
    public Transform[] finger2 = new Transform[3]; //constraint: -45~80, -10~90, -10~90
    public Transform[] finger3 = new Transform[3];

    public float[] flexValues = new float[3];

    void Awake()
    {
        // Assign all the joints on hand to variables in the class
        foreach (Transform nowTransform in GetComponentsInChildren<Transform>())
        {
            switch (nowTransform.gameObject.name)
            {
                case FINGER1_JOINT1: finger1[0] = nowTransform; break;
                case FINGER1_JOINT2: finger1[1] = nowTransform; break;
                case FINGER2_JOINT1: finger2[0] = nowTransform; break;
                case FINGER2_JOINT2: finger2[1] = nowTransform; break;
                case FINGER2_JOINT3: finger2[2] = nowTransform; break;
                case FINGER3_JOINT1: finger3[0] = nowTransform; break;
                case FINGER3_JOINT2: finger3[1] = nowTransform; break;
                case FINGER3_JOINT3: finger3[2] = nowTransform; break;
            }
        }
    }

    void Update()
    {
        finger1[0].localRotation = Quaternion.Euler(
            finger1[0].localEulerAngles.x,
            25 - flexValues[0],
            finger1[0].localEulerAngles.z);
        finger1[1].localRotation = Quaternion.Euler(
            finger1[1].localEulerAngles.x,
            -30 - flexValues[0] * 2,
            finger1[1].localEulerAngles.z);

        for (int joint = 1; joint <= 2; joint++)
        {
            finger2[joint].localRotation = Quaternion.Euler(
                finger2[joint].localRotation.eulerAngles.x,
                finger2[joint].localRotation.eulerAngles.y,
                flexValues[1]);

            finger3[joint].localRotation = Quaternion.Euler(
                finger3[joint].localRotation.eulerAngles.x,
                finger3[joint].localRotation.eulerAngles.y,
                flexValues[2]);
        }
    }
}
