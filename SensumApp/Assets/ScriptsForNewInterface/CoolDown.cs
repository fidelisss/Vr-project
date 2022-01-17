using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class CoolDown : MonoBehaviour
{
    [SerializeField] private UnityEvent onClickEvent;
    [SerializeField] private float value;
    private float timeUp;
    public void Reset()
    {
        timeUp = Time.time + value;
    }
    public bool isReady() => timeUp <= Time.time;
    public void onClick()
    {
        if (isReady())
        {
            onClickEvent?.Invoke();
            Reset();
        }
    }
}
