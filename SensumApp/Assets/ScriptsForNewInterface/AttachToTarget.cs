using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachToTarget : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float neededDist;
    [SerializeField] private float smooth;
    [SerializeField] private MenuReviele UsageInfo;
    private float dist;
    private void LateUpdate()
    {
        GoToTarget();
    }
    private void GoToTarget()
    {
        if (dist < neededDist && !UsageInfo.GetIsInUse())
        {
            transform.position = Vector3.Lerp(transform.position, target.position, dist);
            transform.rotation = Quaternion.Lerp(transform.rotation, target.rotation, smooth);
        }
    }
    private void FindDist()
    {
        dist = Vector3.Magnitude(transform.position - target.position);
    }
    public float GetDist()
    {
        return dist;
    }
}
