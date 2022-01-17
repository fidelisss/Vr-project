using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class AddNumber : MonoBehaviour
{
    [SerializeField] private string _symbol;
    [SerializeField] private AddToCalculation _addToCalculation;
    private TMP_Text _textOfButton;
    
    private void Awake()
    {
        _textOfButton = GetComponentInChildren<TMP_Text>();
        _textOfButton.text = _symbol;
    }

    public void SetSymbol()
    {
        _addToCalculation.SetText(_symbol);
    }

    public void ClearLast()
    {
        _addToCalculation.ClearLast();
    }

    public void ClearAll()
    {
        _addToCalculation.ClearAll();
    }
}
