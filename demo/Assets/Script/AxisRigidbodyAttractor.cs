using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxisRigidbodyAttractor : MonoBehaviour
{
    public Vector3 axisDirection;
    public float axisLength;

    public bool horizontalForce = true;
    public float horizontalForceMultiplier = 10.0f;

    public bool verticalForce = true;
    public float verticalForceMultiplier = 10.0f;
    public float axisSegmentNumber = 5;

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Attractable"))
        {
            Vector3 attractableObjPos = other.transform.parent.position;

            Vector3 columnBasePos = transform.position;
            
            float lambda = Vector3.Dot(
                attractableObjPos - columnBasePos,
                axisDirection) / axisDirection.magnitude / axisLength;

            if (horizontalForce)
            {
                Vector3 intersection = columnBasePos +
                    axisDirection.normalized * lambda * axisLength;

                other.gameObject.GetComponentInParent<Rigidbody>().AddForce(
                    (intersection - attractableObjPos)
                    * horizontalForceMultiplier);
            }

            if (verticalForce)
            {
                float attachedLambda = Mathf.Round(lambda * axisSegmentNumber) / axisSegmentNumber;

                float distance = (attachedLambda - lambda) * axisLength;

                Vector3 forceDirection = Mathf.Sign(distance) * axisDirection.normalized;

                other.gameObject.GetComponentInParent<Rigidbody>().AddForce(
                    forceDirection * Mathf.Abs(distance)
                    * verticalForceMultiplier);
            }

        }
    }
}
