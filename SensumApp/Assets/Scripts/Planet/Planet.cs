using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    [Header("Planet properties")]
    public PlanetManager planetManager;

    private void Awake()
    {
        // disable collision for initialization
        gameObject.layer = 10; // (Ignore Collision)
    }

    private void Start()
    {
        AddToSystem();

        // enable collision after initialization
        gameObject.layer = 0; // (Default)
    }

    private void AddToSystem()
    {
        if (transform.parent) 
            if (transform.parent.TryGetComponent(out PlanetManager planetManager))
                planetManager.AddPlanet(this);
    }

    private void OnDestroy()
    {
        if (planetManager)
            planetManager.RemovePlanet(this);
    }

    private void OnTransformParentChanged()
    {
        AddToSystem();
    }
}
