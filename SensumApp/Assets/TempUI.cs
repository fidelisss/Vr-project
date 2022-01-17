using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TempUI : MonoBehaviour
{
    [SerializeField] private Text _tempText;
    [SerializeField] private Text _energyText;
    [SerializeField] private SubmergedBehaviour _submerged;
    private LiquidBehaviour _liquidBehaviour;
    private float _energy;

    private void Awake()
    {
        _liquidBehaviour = GetComponent<LiquidBehaviour>();
    }

    private void FixedUpdate()
    {
        SetTemp();
    }
    
    private void SetTemp()
    {
        if (_liquidBehaviour.fill == 0)
        {
            _liquidBehaviour.temperature = 0;
            _tempText.text = _liquidBehaviour.temperature.ToString(format:"F2") + "K";
        }
        else
        {
            //_liquidBehaviour.temperature -= Time.deltaTime * 10;
            _tempText.text = _liquidBehaviour.temperature.ToString(format:"F2") + "K";
        }
    }

    public void TempBalance()
    {
        float massOfWater = _liquidBehaviour.fill * _liquidBehaviour.volume;
        float massOfSubmerged = _submerged.mass / 1000;  
        float capacityOfSubmerged = _submerged.HeatCapacity;
        float capacityOfWater = 4200;
        float tempOfWater = _liquidBehaviour.temperature;
        float tempOfSubmerged = _submerged.temperature;
        float tempBalance =
            (massOfWater * capacityOfWater * tempOfWater + massOfSubmerged * capacityOfSubmerged * tempOfSubmerged) /
            (massOfWater * capacityOfWater + massOfSubmerged * capacityOfSubmerged);
        
        _liquidBehaviour.temperature += (tempBalance - _liquidBehaviour.temperature) * 0.01f;
        _submerged.temperature += (tempBalance - _submerged.temperature) * 0.01f;
        
        _tempText.text = _liquidBehaviour.temperature.ToString("F2") + "K";
    }

    public void DecreaseTempAfterAddFill()
    {
        float massOfWaterBefore = _liquidBehaviour.CurrentMassOfWater;
        float massOfWaterAfter = _liquidBehaviour.fill * _liquidBehaviour.volume;
        float massOfAddedWater = massOfWaterAfter - massOfWaterBefore; 
        float tempOfWater = _liquidBehaviour.temperature;
        float tempOfTapWater = 298;
        float tetta = (massOfWaterBefore * tempOfWater + massOfAddedWater * tempOfTapWater) /
                      (massOfWaterBefore + massOfAddedWater);
        
        _liquidBehaviour.temperature += (tetta - _liquidBehaviour.temperature) * 0.01f;
    }

    public void CalculateEnergy()
    {
        float tempOfWater = _liquidBehaviour.temperature;
        float capacity = _submerged.HeatCapacity;
        float mass = _submerged.mass / 1000;
        float tempOfSubmerged = _submerged.temperature;
        _energy = capacity * mass * Math.Abs(tempOfSubmerged - tempOfWater);
        _energyText.text = _energy.ToString(format: "F2") + "J";
    }
}
