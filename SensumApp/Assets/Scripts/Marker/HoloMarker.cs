using System.Collections;
using System.Collections.Generic;
using Mirror;
using Photon.Pun;
using UnityEngine;

public class HoloMarker : Hologram
{
    [Header("Marker properties")]
    public bool draw;
    public GameObject trailPrefab;
    public Transform trailTransform;
    public Material holoLineMaterial;

    public Vector3 holoCenter;
    public Vector3 holoExtents;

    public Transform trailParent;

    public MarkerController markerController;
    
    public PhotonView photonView;

    public string DrawingTag;

    public new void Start()
    {
        base.Start();
        if (!isHolo)
        {
            holoCenter = (holodesk.GetComponent<Collider>().bounds.center - holodesk.referencePoint.position) * holodesk.scaleMultiplier + holodesk.projector.position;
            holoExtents = holodesk.GetComponent<Collider>().bounds.extents * holodesk.scaleMultiplier;

            hologram = Instantiate(gameObject, holodesk.projector).transform;
            hologram.GetComponent<Hologram>().isHolo = true;
            hologram.GetComponent<Collider>().enabled = false;
            hologram.GetComponent<Hologram>().holodesk = null;
        }
        photonView = GetComponent<PhotonView>();
    }

    // void OnTriggerEnter(Collider col)
    // {
    //     if (col.GetComponent<Holodesk>())
    //     {
    //         bool d = draw;
    //         if (d) StopDraw();
    //         InitiateDesk(col.GetComponent<Holodesk>());
    //         if (d) StartDraw();
    //     }
    // }

    // void OnTriggerExit(Collider col)
    // {
    //     if (col.GetComponent<Holodesk>())
    //     { 
    //         bool d = draw;
    //         if (d) StopDraw();
    //         DeinitiateDesk();
    //         if (d) StartDraw();
    //     }
    // }

    protected override void InitiateDesk(Holodesk h)
    {
        if (!hologram)
        {
            holodesk = h;
            hologram = Instantiate(gameObject, holodesk.projector).transform;
            if (hologram.GetComponent<Collider>())
                hologram.GetComponent<Collider>().enabled = false;
        }
    }
    
    
    public void RpcStartDraw()
    {
        StartDraw();
    }
    
    [PunRPC]
    public void StartDraw()
    {
        if (!draw)
        {
            draw = true;
            trailTransform = Instantiate(trailPrefab, transform.position, Quaternion.identity, trailParent).transform;
            print(trailTransform);
            trailTransform.GetComponent<Renderer>().material.SetColor("_Color", markerController.color);

            // if (mode == Mode.Line)
            //         trailTransform.GetComponent<LineRenderer>().SetPosition(0, Vector3.zero);

            if (hologram)
            {
                hologram.GetComponent<HoloMarker>().StartDraw();
                ChangeHoloMaterial();

                trailTransform.GetComponent<HoloTrail>().holoTrail = hologram.GetComponent<HoloMarker>().trailTransform.gameObject;
                trailTransform.GetComponent<HoloTrail>().holoTrail.GetComponent<HoloTrail>().isHolo = true;
            }
        }
    }
    
    [PunRPC]
    public void StopDraw()
    {
        if (draw && trailTransform)
        {
            if (hologram)
            {
                hologram.GetComponent<HoloMarker>().StopDraw();
            }

            draw = false;
            trailTransform.GetComponent<HoloTrail>().FinishTrail();
            trailTransform.gameObject.tag = DrawingTag;
            trailTransform = null;
        }
    }
    
    public void SyncStartDraw()
    {
        Debug.Log(photonView);
        Debug.Log("SyncStartDrawInHoloMarker");
        photonView.RPC("StartDraw", RpcTarget.All);
    }
    
    public void SyncStopDraw()
    {
        GetComponent<PhotonView>().RPC("StopDraw", RpcTarget.All);
    }
    public virtual void ChangeHoloMaterial()
    {
        // choose material according to mode
    }

    void OnDestroy()
    {
        if (hologram) Destroy(hologram.gameObject);

        if (trailTransform)
            StopDraw();
    }

    void OnDisable()
    {
        if (trailTransform)
            StopDraw();
    }
}
