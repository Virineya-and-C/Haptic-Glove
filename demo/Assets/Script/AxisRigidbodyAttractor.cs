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
            Vector3 draggableObjPos = other.gameObject.GetComponentInParent
                <RealPosition>().realPosition.position;

            Vector3 columnBasePos = transform.position;
            
            float lambda = Vector3.Dot(
                draggableObjPos - columnBasePos,
                axisDirection) / axisDirection.magnitude / axisLength;

            if (horizontalForce)
            {
                Vector3 horizontalForce;

                Vector3 intersection = columnBasePos +
                    axisDirection.normalized * lambda * axisLength;

                float distance = (intersection - draggableObjPos).magnitude;

                Vector3 forceDirection = (intersection - draggableObjPos).normalized;

                horizontalForce = forceDirection * distance * horizontalForceMultiplier;

                other.gameObject.GetComponentInParent<Rigidbody>()
                    .AddForce(horizontalForce);

                Debug.DrawRay(draggableObjPos, horizontalForce);
            }

            if (verticalForce)
            {
                Vector3 verticalForce;

                float attachedLambda = Mathf.Round(lambda * axisSegmentNumber) / axisSegmentNumber;

                float distance = (attachedLambda - lambda) * axisLength;

                Vector3 forceDirection = Mathf.Sign(distance) * axisDirection.normalized;

                verticalForce = forceDirection * Mathf.Abs(distance) * verticalForceMultiplier;

                other.gameObject.GetComponentInParent<Rigidbody>()
                    .AddForce(verticalForce);

                Debug.DrawRay(draggableObjPos, verticalForce);
            }

        }
    }
}
