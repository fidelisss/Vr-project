using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterCollision : MonoBehaviour
{
    public float Viscosity;
    public float Density;
    
    public void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<WaterSimul>())
        {
            other.GetComponent<WaterSimul>().enabled = true;
            // other.GetComponent<XRGrabInteractableOffset>().enabled = false;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<WaterSimul>())
        {
            other.GetComponent<WaterSimul>().enabled = false;
            // other.GetComponent<XRGrabInteractableOffset>().enabled = true;
        }
    }
}
