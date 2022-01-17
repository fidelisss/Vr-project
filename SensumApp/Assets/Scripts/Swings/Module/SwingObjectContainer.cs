using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class SwingObjectContainer : MonoBehaviour
{
    public List<SwingWeightSegment> Objects { get; private set; }
    public SwingPlane Plane { get; private set; }

    public event UnityAction FoundCenterOfMass;
    public event UnityAction ObjectMoved;

    private void Awake()
    {
        Objects = new List<SwingWeightSegment>();
        Plane = GetComponentInParent<SwingPlane>();
    }

    private void LateUpdate()
    {
        foreach (var unused in Objects.Where(change 
            => change.VelocityMagnitude > 0.1f && change))
            ObjectMoved?.Invoke();
    }

    private void OnTriggerEnter(Collider other)
    {
        CheckToValid(other);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.TryGetComponent(out SwingWeightSegment weight) || !weight.IsBound) return;
        Objects.Remove(weight.Disable());
        FoundCenterOfMass?.Invoke();
    }

    public void CheckToValid(Collider other)
    {
        if (!other.TryGetComponent(out SwingWeightSegment weight) || weight.IsBound) return;
        if (weight.IsSelect) return;
        
        Objects.Add(weight.Init(this));
        FoundCenterOfMass?.Invoke();

    }
    
}