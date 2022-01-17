using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PipeScale : MonoBehaviour
{
    [Range(0.25f, 2)]
   
    public float Scale = 1;
    private float _lastScale;
    private Vector3 _initialScale;
    [SerializeField] private Text Text;
    private float roundedDiameter;
    private void Awake()
    {
        _initialScale = transform.localScale;
    }

    private void Update()
    {
        if (_lastScale != Scale)
            transform.localScale = Vector3.Scale(new Vector3(Scale, 1, Scale), _initialScale);
        _lastScale = Scale;
        if (Text)
        { Text.text = roundedDiameter.ToString() + " MM"; }
        
        roundedDiameter = (float)(Mathf.Round(_lastScale * 10000) / 100.0);
    }

    public void SetScale(float value)
    {
        Scale = value;
    }
}
