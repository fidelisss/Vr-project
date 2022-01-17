using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hologram : MonoBehaviour
{
    [Header("Hologram properties")]
    public Holodesk holodesk;
    public Transform hologram;

    public bool isOnDesk = false;
    public bool isHolo = false;

    public void Start()
    {
        if (!holodesk) holodesk = Holodesk.instance;
    }

    public void Update()
    {
        if (holodesk && hologram)
        {
            MoveHolo();
        }
    }

    protected virtual void InitiateDesk(Holodesk h)
    {

    }

    protected void DeinitiateDesk()
    {
        Destroy(hologram.gameObject);
        holodesk = null;
    }

    void MoveHolo()
    {
        Vector3 pos = transform.position-holodesk.referencePoint.position;
        hologram.transform.localPosition = pos*holodesk.scaleMultiplier; // + holodesk.projector.position;
        hologram.transform.localRotation = transform.rotation;
        hologram.transform.localScale = transform.localScale*holodesk.scaleMultiplier;
    }

    protected virtual void OnTriggerEnter(Collider col)
    {
        if (hologram && col.gameObject == holodesk.gameObject)
        {
            isOnDesk = true;
        }
    }

    protected virtual void OnTriggerExit(Collider col)
    {
        if (hologram && col.gameObject == holodesk.gameObject)
        {   
            isOnDesk = false;
        }
    }

    private void OnDestroy()
    {
        if (hologram) Destroy(hologram.gameObject);
    }
}
