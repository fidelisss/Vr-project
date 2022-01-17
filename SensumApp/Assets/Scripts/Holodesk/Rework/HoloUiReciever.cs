using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;
public class HoloUiReciever : HoloReciever
{
    public float ScaleMulti;
    protected Toggle _toggle;
    protected Canvas _canvas;
    protected bool _outOfBounds;
    public bool AllowRotation;

    protected override void Awake()
    {
        base.Awake();
        
        _canvas = GetComponent<Canvas>();
        if (!_canvas) _canvas = GetComponentInChildren<Canvas>();
    }
    
    protected override void Start()
    {
        base.Start();
        _toggle = Transmitter.transform.GetComponentInChildren<Toggle>();
    }

    protected override void ApplyTransform()
    {
        transform.localPosition = (_transmitterTransform.position - _holodesk.referencePoint.position) * _holodesk.scaleMultiplier;
        transform.localScale = _transmitterTransform.localScale * _holodesk.scaleMultiplier * ScaleMulti;
        if (AllowRotation) { transform.localRotation = _transmitterTransform.rotation; }

    }

    protected override void ApplyCustomData()
    {
        if (!_outOfBounds && _toggle) _canvas.enabled = _toggle.isOn;
    }

    public override void SetVisibility(bool flag)
    {
        _outOfBounds = !flag;
        if (_canvas) _canvas.enabled = flag;
    }
}
