using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeightMass : MonoBehaviour
{
    [SerializeField] private string localizedNotationKey;
    [SerializeField] private string localizedTextKey;
    [SerializeField] private string stringFormat = "G";
    [SerializeField] private Text massText;
    [SerializeField] private float mass;

    private string _text;
    private string _notation;

    private void Awake()
    {
        LocalizeTexts();
    }

    private void OnEnable()
    {
        LocalizationManager.I.onLocalChange += OnLocalChanged;
    }

    private void OnDisable()
    {
        LocalizationManager.I.onLocalChange -= OnLocalChanged;
    }

    private void Start()
    {
        massText.text = $"{_text} = {mass.ToString(stringFormat)} {_notation}";
    }
    
    private void OnLocalChanged()
    {
        LocalizeTexts();
    }

    private void LocalizeTexts()
    {
        _text = LocalizationManager.I.Localize(localizedTextKey);
        _notation = LocalizationManager.I.Localize(localizedNotationKey);
    }
}
