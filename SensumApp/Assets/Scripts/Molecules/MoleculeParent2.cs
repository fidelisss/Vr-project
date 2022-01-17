using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoleculeParent2 : MonoBehaviour
{
    private List<Molecule> _molecules = new List<Molecule>();
    private Item _item;
    private float _lastTemperature;

    public float Amplitude = 0.2f;
    private float _lastAmplitude;
    public float Speed = 5f;
    private float _lastSpeed;

    private void Awake()
    {
        _lastSpeed = Speed;
        _lastAmplitude = Amplitude;
        _lastTemperature = _item.temperature;
        _item = GetComponent<Item>();
    }

    private void Start()
    {
        foreach (Transform t in transform)
        {
            _molecules.Add(t.GetComponent<Molecule>());
        }
    }

    private void Update()
    {
        if (_lastTemperature != _item.temperature)
        {
            Amplitude = Mathf.Pow(1.03f, _item.temperature - 273) * 0.1f;
        }
            
        if (_lastSpeed != Speed)
        {
            foreach (Molecule m in _molecules)
            {
                m.Speed = Speed;
            }
        }

        if (_lastAmplitude != Amplitude)
        {
            foreach (Molecule m in _molecules)
            {
                m.Amplitude = Amplitude;
            }
        }
        
        _lastSpeed = Speed;
        _lastAmplitude = Amplitude;
    }
}
