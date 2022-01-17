using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoloBaleroReciever : HoloReciever
{
    [SerializeField] private Transform[] _weights;
    [SerializeField] private Transform _head;

     private WeightPos[] _originalWeights;
     private Transform _originalHead;

    protected override void Start()
    {
        base.Start();
        
        _originalHead = Transmitter.GetComponentInChildren<Rotator>().transform;
        _originalWeights = Transmitter.GetComponentsInChildren<WeightPos>();
    } 

    protected override void ApplyCustomData()
    {
        ApplyCustomTransform(_originalWeights[0].transform, _weights[0]);
        ApplyCustomTransform(_originalWeights[1].transform, _weights[1]);
        ApplyCustomTransform(_originalHead, _head);
    }

    private void ApplyCustomTransform(Transform original, Transform holo)
    {
        holo.localPosition = original.localPosition;
        holo.localRotation = original.localRotation;
    }
}
