using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
[RequireComponent(typeof(TextMeshProUGUI))]
    public class LocalizeTextTextMeshPro : MonoBehaviour
{
    [SerializeField] private string key;
    [SerializeField] private bool capitalize;
    private TextMeshProUGUI tmPro;
    private void Awake()
    {   
        tmPro = GetComponent<TextMeshProUGUI>();
        LocalizationManager.I.onLocalChange += OnLocalChanged;
        Localize();

    }

    private void OnLocalChanged()
    {
        Localize();
    }

    private void Localize()
    {
        var localized = LocalizationManager.I.Localize(key);
        tmPro.text = capitalize? localized.ToUpper() : localized;
    }
}
