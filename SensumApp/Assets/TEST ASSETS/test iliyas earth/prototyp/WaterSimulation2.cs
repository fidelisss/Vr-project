using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSimulation2 : MonoBehaviour
{
    public Rigidbody rigidBody;
    public float depthBeforeSubmerged = 1f;
    public float dispLacementAmount = 3f;
    public Transform gj;

    private void FixedUpdate()
    {
        
            if (transform.position.y < gj.position.y)
        {
            float dispLacemenMultiplier = Mathf.Clamp01(gj.position.y-transform.position.y / depthBeforeSubmerged) * dispLacementAmount;
            rigidBody.AddForce(new Vector3(0f, Mathf.Abs(Physics.gravity.y) * dispLacemenMultiplier, 0f), ForceMode.Acceleration);
        }
    }
}

