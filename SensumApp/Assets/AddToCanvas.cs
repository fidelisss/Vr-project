using System;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class AddToCanvas : MonoBehaviour
{
    private AddToCalculation _addToCalculation;

    private void Awake()
    {
        _addToCalculation = GetComponent<AddToCalculation>();
    }

    public void SetGameObject(GameObject triggerredGameObject)
    {
        if (triggerredGameObject.TryGetComponent(out WorldUISegment segment))
        {
            segment.SetLetter(transform);
            _addToCalculation.SetText(segment.Letter);
        }
    }

    public void SetGameObjectRes(GameObject trigerredGameObject)
    {
        if (trigerredGameObject.TryGetComponent(out WorldUISegment segment))
        {
            segment.ReturnLetter();
        }
    }
}
