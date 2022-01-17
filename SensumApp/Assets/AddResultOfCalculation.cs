using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Data;

public class AddResultOfCalculation : MonoBehaviour
{
    [SerializeField] private TMP_Text _textOfResult;
    private AddToCalculation _addToCalculation;

    private void Awake()
    {
        _addToCalculation = GetComponent<AddToCalculation>();
    }

    public void Result()
    {
        string result = new DataTable().Compute(_addToCalculation._formula, null).ToString();
        _textOfResult.text = result;
    }
}
