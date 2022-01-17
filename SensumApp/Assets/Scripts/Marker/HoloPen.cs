using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoloPen : HoloMarker
{
    new void Update()
    {
        base.Update();

        if (draw)
        {
            trailTransform.position = transform.position;
        }
    }

    public override void ChangeHoloMaterial()
    {
        // choose material according to mode
        TrailRenderer tr = hologram.GetComponent<HoloMarker>().trailTransform.GetComponent<TrailRenderer>();
        tr.startWidth *= holodesk.scaleMultiplier;
        tr.material = holoLineMaterial;
        tr.material.SetColor("_Color", markerController.color);
        tr.material.SetVector("_Position", holoCenter);
        tr.material.SetVector("_Extents", holoExtents);
    }
}
