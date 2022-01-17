using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoloTrail : Trail
{
    public GameObject holoTrail;
    public bool isHolo = false;

    void OnDestroy()
    {
        Destroy(holoTrail);
    }

    public override void FinishTrail()
    {
        if (isHolo)
            return;
        else
            base.FinishTrail();
    }

    public override void SnapAftermathA() 
    {
        if (!isHolo && snapA.GetComponent<HoloTransmitter>())
            holoTrail.GetComponent<HoloTrail>().snapA = snapA.GetComponent<HoloTransmitter>().Hologram.GetComponent<SnapPoint>();
        else if (holoTrail)
            Destroy(holoTrail);
    }
    
    public override void SnapAftermathB() 
    {
        if (!isHolo && snapB.GetComponent<HoloTransmitter>())
            holoTrail.GetComponent<HoloTrail>().snapB = snapB.GetComponent<HoloTransmitter>().Hologram.GetComponent<SnapPoint>();
        else if (holoTrail)
            Destroy(holoTrail);
    }
}
