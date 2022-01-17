using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Drawing))]
public class HoloDrawingReciever : HoloReciever
{
    private Drawing _holoDrawing;
    private Drawing _originalDrawing;

    protected override void Awake()
    {
        // do not disable renderers
        return;
    }

    protected override void Start()
    {
        base.Start();

        ApplyTransform();

        _holoDrawing = GetComponent<Drawing>();
        _originalDrawing = Transmitter.GetComponent<Drawing>();

        _holoDrawing.SetPoints(_originalDrawing.GetPoints());
        _holoDrawing.Color = _originalDrawing.Color;
        _holoDrawing.LineWidth = _originalDrawing.LineWidth * _holodesk.scaleMultiplier;
        _holoDrawing.StartWidget = _originalDrawing.StartWidget;
        _holoDrawing.EndWidget = _originalDrawing.EndWidget;

        if (_originalDrawing.GetComponent<RotationFixer>()) gameObject.AddComponent(typeof(RotationFixer)); // warning: kostyl' for ForceVectorLineDrawer

        //<summary>
        // not in OnEnable, because depends on Transmitter, which is assigned after Instantiation
        //</summary>
        _originalDrawing.AddedPoint += OnAddedPoint; 
        _originalDrawing.MovedPoint += OnMovedPoint;
        _originalDrawing.SettedPoints += OnSettedPoints;
        _originalDrawing.ColorChanged += OnColorChanged;
        _originalDrawing.LineWidthChanged += OnLineWidthChanged;
    }

    private void OnDisable()
    {
        _originalDrawing.AddedPoint -= OnAddedPoint;
        _originalDrawing.MovedPoint -= OnMovedPoint;
        _originalDrawing.SettedPoints -= OnSettedPoints;
        _originalDrawing.ColorChanged -= OnColorChanged;
        _originalDrawing.LineWidthChanged -= OnLineWidthChanged;
    }

    private void OnAddedPoint(Vector3 position)
    {
        _holoDrawing.AddPoint(position);
    }

    private void OnMovedPoint(int index, Vector3 position)
    {
        // Debug.Log($"Index: {index} Position: {position}");
        _holoDrawing.MovePoint(index, position);
    }

    private void OnSettedPoints(Vector3[] points)
    {
        _holoDrawing.SetPoints(points);
    }

    private void OnColorChanged(Color color)
    {
        _holoDrawing.Color = color;
    }

    private void OnLineWidthChanged(float width)
    {
        _holoDrawing.LineWidth = width * _holodesk.scaleMultiplier;
    }
}
