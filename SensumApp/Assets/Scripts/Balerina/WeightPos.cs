using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeightPos : MonoBehaviour
{
    public Text radiusText;

    public float roundedRadius;
    [SerializeField] GameObject _Center;
    private Vector3 v1, v2;
    private float distance;

    [SerializeField] private string _localizedNotationKey;
    [SerializeField] private string _localizedTextKey;
    [SerializeField] private string _stringFormat = "G";

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
    
    private void FixedUpdate()
    {
        GetRadius();
        RoundedRadius();
        radiusText.text = $"{_text} = {roundedRadius.ToString(_stringFormat)} {_notation}";

    }

    private void RoundedRadius()
    {
        roundedRadius = (float)(Mathf.Round(distance * 100) / 100.0);
    }

    private void GetRadius()
    {
        v1 = _Center.transform.localPosition;
        v2 = transform.localPosition;
        
        // 27.08, Dima: Both weights now have positive local position, so I will change subtraction to addition
        Vector3 difference = new Vector3(
            v1.x + v2.x,
            v1.y + v2.y,
            v1.z + v2.z
        );

        distance = (float)Math.Sqrt(
            Math.Pow(difference.x, 2f) +
            Math.Pow(difference.y, 2f) +
            Math.Pow(difference.z, 2f)
        );
    }
    
    private void OnLocalChanged()
    {
        LocalizeTexts();
    }

    private void LocalizeTexts()
    {
        _text = LocalizationManager.I.Localize(_localizedTextKey);
        _notation = LocalizationManager.I.Localize(_localizedNotationKey);
    }
}
