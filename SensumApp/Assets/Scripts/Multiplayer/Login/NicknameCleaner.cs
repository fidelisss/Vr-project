using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class NicknameCleaner : MonoBehaviour
{
    [FormerlySerializedAs("_inputField")] [SerializeField] private InputField inputField;

    private void Start() => StartCoroutine(WaitCoroutine());

    private IEnumerator WaitCoroutine()
    {
        yield return new WaitForSeconds(0.1f);
        if(inputField) inputField.text = "";
    }
    
}
