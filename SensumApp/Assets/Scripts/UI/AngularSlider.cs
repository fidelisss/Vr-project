using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AngularSlider : AbstractSlider
{
    private float _rotationY = 0;
    [SerializeField] private Transform _sliderHandleTransform;
    [SerializeField] private Transform _sliderOriginalTransform;

    [SerializeField] private float _offset = 0;

    private void Awake()
    {
        _parent = transform.parent;
        _lastValue = Value;
    }

    private void Start()
    {
        _rotationY = transform.rotation.eulerAngles.y;
    }

    private void Update()
    {
        FixTransform();

        Value = _rotationY + _offset;
        _valueDifference = Value - _lastValue;
        if (Value != _lastValue)
        {
            OnValueChanged();
            OnValueChangedDifferential();
        }

        _lastValue = Value;
    }

    public void OnValueChanged()
    {
        ValueChanged.Invoke(Value);
    }

    public void OnValueChangedDifferential()
    {
        ValueChangedDifferential.Invoke(_valueDifference);
    }

    public void OnReleased()
    {
        Released.Invoke(Value);
    }

    public void OnReleasedDifferential()
    {
        ReleasedDifferential.Invoke(_valueDifference);
    }

    private void FixTransform()
    {
        Vector3 direction = Vector3.ProjectOnPlane(_sliderHandleTransform.position - transform.position, transform.up);
        _rotationY = Vector3.SignedAngle(_parent.forward, direction, transform.up);
        transform.localRotation = Quaternion.Euler(0, _rotationY, 0);
    }

    public void ReturnSlider()
    {
        _sliderHandleTransform.position = _sliderOriginalTransform.position;
    }
}
