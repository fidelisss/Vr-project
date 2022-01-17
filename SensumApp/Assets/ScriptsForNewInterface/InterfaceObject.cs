using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterfaceObject : MonoBehaviour
{
    [SerializeField] private GameObject go;
    private bool inUse;
    private Transform originalTransform;
    private void Awake() 
    {
        originalTransform = go.transform; 
    }
    public void SetInUse(bool inUse_) => inUse = inUse_;
    public bool GetInUse() { return inUse; }
    
    public GameObject GetGO() { return go; }
    public void SetToOrigin()
    {
        go.transform.position = originalTransform.position;
        go.transform.rotation = originalTransform.rotation;
        go.gameObject.SetActive(false);
    }
}