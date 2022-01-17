using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterMass : MonoBehaviour
{
    public Transform centerOfMassTransform;

    private void Update()
    {
        // GetComponent<Rigidbody>().centerOfMass = centerOfMassTransform.localPosition; //Vector3.Scale(centerOfMassTransform.localPosition, transform.localScale);
        //  GetComponent<Rigidbody>().WakeUp();
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(GetComponent<Rigidbody>().worldCenterOfMass, 0.1f);
    }
}
