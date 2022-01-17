using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HoloDynamicUIRecieverTMP : HoloUiReciever
{
    protected TMP_Text[] _text;
    protected TMP_Text[] _originalText;
    protected override void Start()
    {
        base.Start();
        //for (int i = 0; i < _canvas.GetComponentsInChildren<Text>().Length; i++)
        //{
        //    text[i] = _canvas.GetComponentsInChildren<Text>()[i];
        //}

        _text = _canvas.GetComponentsInChildren<TMP_Text>();
        _originalText = Transmitter.GetComponentsInChildren<TMP_Text>();
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
