using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AddToCalculation : MonoBehaviour
{
    [SerializeField] private TMP_Text _textOfCalculation;
    [NonSerialized] public string _formula = "";
    private AddResultOfCalculation _addResult;

    private void Awake()
    {
        _addResult = GetComponent<AddResultOfCalculation>();
    }

    public void SetText(String s)
    {
        _formula += s;
        _textOfCalculation.text = _formula;
        _addResult.Result();
    }

    public void ClearLast()
    {
        _formula = _formula.Remove(_formula.Length - 1, 1);
        _textOfCalculation.text = _formula;
        _addResult.Result();
    }

    public void ClearAll()
    {
        _formula = "";
        _textOfCalculation.text = _formula;
        _addResult.Result();
    }
}
