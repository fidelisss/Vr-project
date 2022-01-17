using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSimul : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private Transform _waterLvl;
    private WaterCollision _liquid;
    private Item _item;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _item = GetComponent<Item>();
    }

    private void FixedUpdate()
    {
        float divePercent = GetDivePercent();
        ApplyPhys(divePercent);
    }

    private void ApplyPhys(float divePercent)
    {
        // Buoyancy
        // F = rho * g * V
        Vector3 buoyancy = divePercent * _liquid.Density * -Physics.gravity * 4/3 * Mathf.PI * Mathf.Pow(_item.radius, 3);

        // Viscous drag
        // F = 6 * pi * eta * v * r
        Vector3 drag = divePercent * 6 * Mathf.PI * _liquid.Viscosity * -_rigidbody.velocity * _item.radius;
        _rigidbody.AddForce(buoyancy + drag);
        _rigidbody.angularDrag = 2;
    }

    private float GetDivePercent()
    {
        // Works only with the pivot on the bottom of the object
        float divePercent = (_waterLvl.position.y - transform.position.y) / _item.radius / 2;
        divePercent = Mathf.Clamp(divePercent, 0f, 1f);
        return divePercent;
    }

    private void OnDisable()
    {
        _rigidbody.drag = 0;
        _rigidbody.angularDrag = 0.05f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<WaterCollision>()) //Try
        {
            _waterLvl = other.GetComponent<WaterCollision>().transform;
            _liquid = other.GetComponent<WaterCollision>();
        } 
    }
}
