using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoloSwingsReciever : HoloReciever
{
    [SerializeField] private Transform _plane;
    [SerializeField] private Transform _centerMass;
    [SerializeField] private Transform _originalPlane;
    [SerializeField] private Transform _originalCenterMass;

    protected override void Start()
    {
        base.Start();
        _originalPlane = Transmitter.GetComponentInChildren<HingeJoint>().transform;
        _originalCenterMass = _originalPlane.GetChild(0);
    }

    protected override void ApplyCustomData()
    {
        _plane.localRotation = _originalPlane.localRotation;
        _centerMass.localPosition = _originalCenterMass.localPosition;
    }
}
