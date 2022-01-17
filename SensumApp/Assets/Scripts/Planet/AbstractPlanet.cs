using System;
using UnityEngine;

public abstract class AbstractPlanet : MonoBehaviour
{
    [SerializeField] private float _velocity;

    public float Velocity => _velocity;

    private Transform _transform;

    private void Awake() => _transform = transform;
    
    // private void FixedUpdate() => _transform.Rotate(new Vector3(0, _velocity * Time.fixedDeltaTime, 0));   //Vector3.up * (_velocity * Time.fixedDeltaTime));
}