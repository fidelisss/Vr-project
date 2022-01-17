using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ToolCollisionStabilizer : MonoBehaviour
{
    [SerializeField] private Transform _tip;

    private XRGrabInteractable _grab;
    // private bool _isGrabbed = true;
    private bool _isInZone;
    private CollisionStabilizerZone _currentZone;

    private void Awake()
    {
        _grab = GetComponent<XRGrabInteractable>();
    }

    // private void OnEnable()
    // {
    //     _grab.selectEntered.AddListener((SelectEnterEventArgs args) => { _isGrabbed = true; });
    //     _grab.selectExited.AddListener((SelectExitEventArgs args) => { _isGrabbed = false; });
    //     _grab.selectExited.AddListener((SelectExitEventArgs args) => { _isGrabbed = false; });
    // }
    //
    // private void OnDisable()
    // {
    //     _grab.selectEntered.RemoveListener((SelectEnterEventArgs args) => { _isGrabbed = true; });
    //     _grab.selectExited.RemoveListener((SelectExitEventArgs args) => { _isGrabbed = false; });
    // }

    private void Update()
    {
        if (_isInZone)
        {
            StabilizeTool();
        }
    }

    private void StabilizeTool()
    {
        Plane plane = new Plane(_currentZone.transform.up, _currentZone.transform.position);
        if (!plane.GetSide(_tip.position))
        {
            Vector3 tipTarget = plane.ClosestPointOnPlane(_tip.position);
            transform.position = transform.position - _tip.position + tipTarget;
            _tip.position = tipTarget;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out CollisionStabilizerZone zone))
        {
            _isInZone = true;
            _currentZone = zone;
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<CollisionStabilizerZone>())
        {
            _isInZone = false;
        }
    }
}
