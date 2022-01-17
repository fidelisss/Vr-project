using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Slider3dContraint : AbstractSlider
{
    public Transform MaxTransform;  
    public Transform MinTransform;

    private Vector3 _maxPosition;
    private Vector3 _minPosition;
    
    private float lastLocalHeight = 0;

    private void Awake()
    {
        _maxPosition = MaxTransform.localPosition;
        _minPosition = MinTransform.localPosition;
        _parent = transform.parent;
    }

    private void Start()
    {   
        Value = Convertor.Remap(transform.localPosition.y, _minPosition.y, _maxPosition.y, MinValue, MaxValue);
        Value = Mathf.Clamp(Value, MinValue, MaxValue);
        ValueChanged.Invoke(Value);
    }

    private void Update()
    {
        Vector3 localPosition = _parent.InverseTransformPoint(transform.position);
        
        if (localPosition.x != 0 || localPosition.z != 0)
                transform.position = _parent.TransformPoint(new Vector3(0, localPosition.y, 0));

        if (lastLocalHeight != localPosition.y)
        {
            if (localPosition.y > _maxPosition.y) 
                transform.position = MaxTransform.position;
            else if (localPosition.y < _minPosition.y) 
                transform.position = MinTransform.position;

            Value = Convertor.Remap(localPosition.y, _minPosition.y, _maxPosition.y, MinValue, MaxValue);
            Value = Mathf.Clamp(Value, MinValue, MaxValue);
            ValueChanged.Invoke(Value);
        }
        lastLocalHeight = localPosition.y;
    }
}
