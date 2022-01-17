using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoloLine : HoloMarker
{
    new void Update()
    {
        base.Update();

        if (draw && trailTransform)
        {
            trailTransform.GetComponent<LineRenderer>().SetPosition(1, transform.position - trailTransform.position);
        }
    }

    public override void ChangeHoloMaterial()
    {
        // choose material according to mode
        LineRenderer lr = hologram.GetComponent<HoloMarker>().trailTransform.GetComponent<LineRenderer>();
        lr.startWidth *= holodesk.scaleMultiplier;
        lr.material = holoLineMaterial;
        lr.material.SetColor("_Color", markerController.color);
        lr.material.SetVector("_Position", holoCenter);
        lr.material.SetVector("_Extents", holoExtents);
    }
}
