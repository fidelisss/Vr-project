using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NewFire : MonoBehaviour
{
    [SerializeField] private float _bandwidth;
    [SerializeField] private float _maxTemperature;
    
    public float Bandwidth => _bandwidth;
    public float MaxTemperature => _maxTemperature;
}
