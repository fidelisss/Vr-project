using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Body : MonoBehaviour
{
    void Update()
    {
        //transform.up = Vector3.up;
        transform.forward = Vector3.ProjectOnPlane(transform.parent.forward, Vector3.up);
        //transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
    }
}
