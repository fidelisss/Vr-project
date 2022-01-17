using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Text))]
    public class LocalizeText : MonoBehaviour
{
    [SerializeField] private string key;
    [SerializeField] private bool capitalize;
    private Text text;
    private void Awake()
    {   
        text = GetComponent<Text>();
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
        text.text = capitalize? localized.ToUpper() : localized;
    }
}
