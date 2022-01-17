using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class NewBehaviourScript : MonoBehaviour
{
    public Vector3 centerOfMass;
    public bool Awake;
    protected Rigidbody r;

    private void Start()
    {
        r = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        r.centerOfMass = centerOfMass;
        r.WakeUp();
        Awake = !r.IsSleeping();
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position + transform.rotation * centerOfMass, 0.01f);
    }



}
