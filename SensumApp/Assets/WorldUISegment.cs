using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
    
public class WorldUISegment : MonoBehaviour
{
    [SerializeField] private string letter;
    private Canvas _canvas;
    private TMP_Text _text;
    public string Letter => letter;

    private void Awake()
    {
        _canvas = GetComponentInChildren<Canvas>();
        _text = GetComponentInChildren<TMP_Text>();
        _text.text = letter;
    }

    public void SetLetter(Transform parent)
    {
        _text.transform.SetParent(parent);
        _canvas.enabled = false;
    }

    public void ReturnLetter()
    {
        _canvas.enabled = true;
        _text.transform.SetParent(_canvas.transform);
    }
    
}
