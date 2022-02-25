using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidbodyBalanceKeeper : MonoBehaviour
{
    public float torqueMultiplier;

    private Rigidbody rigidbody;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float signedEulerAngleX =
            (transform.eulerAngles.x <= 180
                ? transform.eulerAngles.x
                : transform.eulerAngles.x - 360) / 720;
        rigidbody.AddTorque(
            -Vector3.right * signedEulerAngleX * torqueMultiplier,
            ForceMode.Force);

        float signedEulerAngleZ =
            (transform.eulerAngles.z <= 180
                ? transform.eulerAngles.z
                : transform.eulerAngles.z - 360) / 720;
        rigidbody.AddTorque(
            -Vector3.forward * signedEulerAngleZ * torqueMultiplier,
            ForceMode.Force);
    }
}
