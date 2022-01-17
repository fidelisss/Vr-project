using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class ValueChangeEvent : UnityEvent<float>
{
}

public abstract class AbstractSlider : MonoBehaviour
{
    public float Value { get; protected set; }
    protected float _lastValue;
    protected float _valueDifference;
    public float LastValue
    {
        get { return _lastValue; }
        set { _lastValue = value; }
    }
    [SerializeField] protected ValueChangeEvent ValueChanged;
    [SerializeField] protected ValueChangeEvent ValueChangedDifferential;
    [SerializeField] protected ValueChangeEvent Released;
    [SerializeField] protected ValueChangeEvent ReleasedDifferential;

    public float MaxValue = 1;
    public float MinValue = 0;

    protected Transform _parent;
}
