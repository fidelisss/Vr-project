using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoloRampReciever : HoloReciever
{
    [SerializeField] private Transform _plane;
    [SerializeField] private Transform _originalPlane;

    protected override void Start()
    {
        base.Start();
        _originalPlane = Transmitter.GetComponentInChildren<HingeJoint>().transform;
    } 

    protected override void ApplyCustomData()
    {
        _plane.localRotation = Quaternion.Euler(_originalPlane.localRotation.eulerAngles.x, _plane.localRotation.eulerAngles.y, _plane.localRotation.eulerAngles.z);
    }
}
