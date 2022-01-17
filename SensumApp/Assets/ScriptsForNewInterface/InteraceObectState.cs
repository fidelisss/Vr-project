using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class InteraceObectState : MonoBehaviour
{
    [SerializeField] private InterfaceObject[] interfaceObjects; 
    public void SetActive()
    {
        foreach (InterfaceObject go_ in interfaceObjects)
            go_.GetGO().SetActive(true);
    }
    public void SetInActive()
    {
        foreach (InterfaceObject go_ in interfaceObjects)
            if (go_.GetInUse() == false)
                go_.GetGO().SetActive(false);
    }
}