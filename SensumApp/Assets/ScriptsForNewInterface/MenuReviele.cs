using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuReviele : MonoBehaviour
{
    private bool _isInUse = false;
    [SerializeField] private GameObject canvas;
    public void SetInUse(bool isInUse)
    {
        _isInUse = isInUse;
        if (_isInUse == false)
            canvas.SetActive(false);
        else
            canvas.SetActive(true);
    }
    //private void OnTriggerStay(Collider other)
    //{
    //    if (isInUse && other.CompareTag("Tip"))
    //        canvas.SetActive(true);
    //}
    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.CompareTag("Tip"))
    //        canvas.SetActive(false);
    //}
    public bool GetIsInUse()
    {
        return _isInUse;
    }
}
