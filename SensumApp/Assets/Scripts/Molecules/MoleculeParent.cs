using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoleculeParent : MonoBehaviour
{
    public float Speed = 5;
    private float _lastSpeed;
    public float Amplitude = 0.2f;
    private float _lastAmplitude;
    private float _lastTemperature;

    private Item _item;

    private readonly List<Material> _moleculeMaterials = new List<Material>();

    private void Awake()
    {
        _lastSpeed = Speed;
        _lastAmplitude = Amplitude;
        _item = GetComponent<Item>();
        _lastTemperature = _item.temperature;
    }

    private void Start()
    {
        foreach (Transform t in transform)
        {
            Material mat = t.GetComponent<Renderer>().material;

            // Set random seed for each molecule. This will make them move asynchronous
            mat.SetFloat("_Seed", Random.Range(0, 1000));
            mat.SetFloat("_Speed", Speed);
            mat.SetFloat("_Amplitude", Amplitude);
            _moleculeMaterials.Add(mat);
        }
    }

    private void Update()
    {
        if (_item.temperature != _lastTemperature)
        {
            // Not scientific, but good-looking exponential function of temperature (close to Ice) 
            Amplitude = Mathf.Pow(1.03f, _item.temperature - 273) * 0.1f;

            // Update the material of child objects to make them move
            // Only update material on changes
            if (_lastSpeed != Speed)
            {
                foreach (Material m in _moleculeMaterials)
                {
                    m.SetFloat("_Speed", Speed);
                }
            }

            if (_lastAmplitude != Amplitude)
            {
                foreach (Material m in _moleculeMaterials)
                {
                    m.SetFloat("_Amplitude", Amplitude);
                }
            }

            _lastSpeed = Speed;
            _lastAmplitude = Amplitude;
        }
        _lastTemperature = _item.temperature;
    }
}
