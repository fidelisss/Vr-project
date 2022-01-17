using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HoloDynamicUIReciever : HoloUiReciever
{
    protected Text[] _text;
    protected Text[] _originalText;
    
    protected override void Start()
    {
        base.Start();

        _text = _canvas.GetComponentsInChildren<Text>();
        _originalText = Transmitter.GetComponentsInChildren<Text>();
    }
    protected override void ApplyCustomData()
    {
        base.ApplyCustomData();

        int i = 0;
        foreach (var text in _originalText)
        {
            _text[i].text = text.text;
            i++;
        }
    }
}
