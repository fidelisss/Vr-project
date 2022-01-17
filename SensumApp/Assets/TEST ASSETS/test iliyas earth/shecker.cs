using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shecker : MonoBehaviour
{
    public GameObject cube;
    public void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<WaterSimul>())
        {
        }
    }
}
