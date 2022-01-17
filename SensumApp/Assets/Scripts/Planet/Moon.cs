using System;
using UnityEngine;

public class Moon : AbstractPlanet
{
    [SerializeField] private Earth _earth;
    [SerializeField] private float _angularSpeed;
    private float _originalAngularSpeed;

    private void Awake()
    {
        _originalAngularSpeed = _angularSpeed;
    }
    
    private void Update() => 
        transform.RotateAround(_earth.transform.position, transform.up, _angularSpeed * Time.deltaTime);

    public void Pause(int i)
    {
        _angularSpeed = i * _originalAngularSpeed;
    }
}