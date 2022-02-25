using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidbodyController : MonoBehaviour
{
    public float speed;

    private Rigidbody rigidbody;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();

    }

    void Update()
    {
        rigidbody.AddTorque(Vector3.right * Input.GetAxis("Vertical") * speed);
        rigidbody.AddTorque(-Vector3.forward * Input.GetAxis("Horizontal") * speed);
    }
}
