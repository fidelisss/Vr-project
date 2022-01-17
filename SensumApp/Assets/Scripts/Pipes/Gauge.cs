using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gauge : MonoBehaviour
{
    public Transform Arrow;
    public float MinAngle;
    public float MaxAngle;
    public float MinValue;
    public float MaxValue;
    [Range(0, 10)]
    public float Value;
    public float ValueMultiplier = 1;
    private float _lastValue = -1;
    Vector3 _initialRotation;

    private void Awake()
    {
        _initialRotation = Arrow.localRotation.eulerAngles;
    }
    

    private void Update()
    {
        Value = Mathf.Clamp(Value, MinValue, MaxValue);
        if (_lastValue != Value)
        {
            Arrow.localRotation = Quaternion.Euler(_initialRotation.x, Convertor.Remap(Value, MinValue, MaxValue, MinAngle, MaxAngle), _initialRotation.z);
        }
        _lastValue = Value;
    }

    public void SetValue(float pipeScale)
    {
        Value = pipeScale * ValueMultiplier;
    }

}
