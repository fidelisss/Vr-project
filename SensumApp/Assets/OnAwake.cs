using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
public class OnAwake : MonoBehaviour
{
    [SerializeField] private List<GameObject> _ActivateObjectOnEnabled;   //
    [SerializeField] private List<GameObject> _DiactivateObjectOnEnabled; // 2 list of objects to be activated or diactivated on object's Enable event
    [SerializeField] private List<GameObject> _DiactivateObject;       //  diactivate object through button's OnClick event
    private Component comp;
    private void OnEnable()
    {
        foreach (var item in _ActivateObjectOnEnabled)
        {
            item.SetActive(true);
        }
        foreach (var item in _DiactivateObjectOnEnabled)
        {
            item.SetActive(false);
         

        }
    }
    public void Diactivate()
    {
        foreach (var item in _DiactivateObject)
        {
            item.SetActive(false);
        }
    }


        
        
    
}
