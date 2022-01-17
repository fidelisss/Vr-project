using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class OnButtonDoubleClick : MonoBehaviour
{
    private bool clicked = false;
    [SerializeField] private UnityEvent onFirstClick;
    [SerializeField] private UnityEvent onSecondClick;
    public void Click()
    {
        if (clicked == false)
        {
            onFirstClick?.Invoke();
            clicked = true;
        } else
        {
            onSecondClick?.Invoke();
            clicked = false;
        }
    }
    public void SetBoolClick(bool clicked_)
    {
        clicked = clicked_;
    }
    public void ResetClick()
    {
        OnButtonDoubleClick[] button = FindObjectsOfType<OnButtonDoubleClick>();
        foreach (OnButtonDoubleClick b in button)
            b.SetBoolClick(false);
    }
}
