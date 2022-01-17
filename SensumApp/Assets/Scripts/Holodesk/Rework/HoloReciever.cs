using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class HoloReciever : MonoBehaviour
{
    /*[HideInInspector]*/public HoloTransmitter Transmitter;
    protected Transform _transmitterTransform;
    protected Holodesk _holodesk;
    protected List<Renderer> _renderers = new List<Renderer>();

    protected virtual void Awake()
    {
        StoreRenderers();
        SetVisibility(false);
    }

    protected virtual void Start()
    {
        _transmitterTransform = Transmitter.transform;
        _holodesk = Transmitter.Holodesk;
    }

    public void Update()
    {
        ApplyTransform();
        ApplyCustomData();
    }

    // Store renderers to control visibility
    private void StoreRenderers()
    {
        Renderer firstRend = GetComponent<Renderer>();
        if (firstRend) _renderers.Add(firstRend);
        foreach (Renderer rend in GetComponentsInChildren<Renderer>())
        {
            _renderers.Add(rend);
        }
    }

    // Apply transform of original object to hologram
    protected virtual void ApplyTransform()
    {
        transform.localPosition = (_transmitterTransform.position - _holodesk.referencePoint.position) * _holodesk.scaleMultiplier;
        transform.localRotation = _transmitterTransform.rotation;
        transform.localScale = _transmitterTransform.lossyScale * _holodesk.scaleMultiplier; // be careful with lossy scale, might become a problem later
    }

    
    //protected void SyncApplyCustomData()
    //{ 
    //    GetComponent<PhotonView>().RPC("ApplyCustomData", RpcTarget.AllBuffered);
    //}

    //<summary>
    // Here custom parameters are synchronized for hologram
    // Override it for custom objects
    //</summary>

    protected virtual void ApplyCustomData()
    {

    }

    // Control visibility
    public virtual void SetVisibility(bool b)
    {
        foreach (Renderer rend in _renderers)
        {
            rend.enabled = b;
        }
    }
}
