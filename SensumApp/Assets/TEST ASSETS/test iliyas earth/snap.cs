using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class snap : MonoBehaviour
{
    private bool grabbed;
    private bool insidsnapeZone;
    public bool Snapped;
    public GameObject obj;
    public GameObject SnapRotationReference;


    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == obj.name)
        {
            insidsnapeZone = true;
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.name == obj.name)
        {
            insidsnapeZone = false;
        }
    }
    void SnapObject()
    {
        if(grabbed == false && insidsnapeZone == true)
        {
            obj.gameObject.transform.position = transform.position;
            obj.gameObject.transform.rotation = SnapRotationReference.transform.rotation;
            Snapped = true;
        }
    }
    //private void Update()
    //{
        //grabbed = obj.GetComponent<OVRGrabbable>().isGrabbed;
        //SnapObject();
    }
//}
