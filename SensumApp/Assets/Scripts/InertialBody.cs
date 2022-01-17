using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Item))]
public class InertialBody : MonoBehaviour
{
    // This coefficient is unique for different shapes. E.g.: solid cylinder = 1/2, hollow cylinder = 1 (for main axis)
    public Vector3 InertiaCoef = Vector3.one;

    void Start()
    {
        // Physical parameters are from Item component
        Rigidbody rb = GetComponent<Rigidbody>();
        Item item = GetComponent<Item>();
        float m = item.mass;
        float r = item.radius;

        // Inertia tenzor is like a mass, but for spinning. More the inertia, harder it is to spin the object
        rb.inertiaTensor = new Vector3(InertiaCoef.x * m * r * r, InertiaCoef.y * m * r * r, InertiaCoef.z * m * r * r);
    }
}
