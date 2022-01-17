using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Marker : MonoBehaviour
{
    [Header("Marker properties")]
    public bool draw;
    public GameObject trailPrefab;
    public Transform trailTransform;
    public Transform trailParent;
    public MarkerController markerController;
    
    public void StartDraw()
    {
        if (!draw)
        {
            draw = true;
            trailTransform = Instantiate(trailPrefab, transform.position, Quaternion.identity, trailParent).transform;
            trailTransform.GetComponent<Renderer>().material.SetColor("_Color", markerController.color);
        }
    }
    
    public void StopDraw()
    {
        if (draw)
        {
            draw = false;
            trailTransform.GetComponent<Trail>().FinishTrail();
            trailTransform = null;
        }
    }
}
