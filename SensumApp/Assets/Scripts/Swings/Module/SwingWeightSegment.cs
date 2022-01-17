using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SwingWeightSegment : MonoBehaviour
{
    public float Mass => _rigidbody.mass;
    public float VelocityMagnitude => _rigidbody.velocity.magnitude;
    public bool IsBound { get; private set; }

    private SwingObjectContainer _swingObjectContainer;
    private Rigidbody _rigidbody;
    private Vector3 _lastRelativePosition;


    private XRGrabInteractableOffset _interactableOffset;

    public bool IsSelect { get; private set; }


   private void Awake()
   {
       _rigidbody = GetComponent<Rigidbody>();
       _interactableOffset = GetComponent<XRGrabInteractableOffset>();
   }

   private void OnEnable()
   {
   }

   public SwingWeightSegment Init(SwingObjectContainer point)
    {
        IsBound = true;
        _swingObjectContainer = point;
        return this;
    }

   public SwingWeightSegment Disable()
    {
        IsBound = false;
        _swingObjectContainer = null;
        return this;
    }

    public float GetRelativeSpeed(Transform target)
    {
        var currentPosition = target.InverseTransformPoint(transform.position);
        var speed = Vector3.Distance(currentPosition, _lastRelativePosition);
        _lastRelativePosition = currentPosition;
        return speed;
    }
    
    public float GetFromCenterCoordinate(Transform center) => center.InverseTransformPoint(transform.position).x;
    
    public void SetGrabActive(bool flag) => IsSelect = flag;
}