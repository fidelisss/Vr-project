using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HoloUI : Hologram
{
    public Text holoText;
    public Text text;


    public new void Start()
    {
        base.Start();
        if (!holodesk) holodesk = Holodesk.instance;
        InitiateDesk(holodesk);
    }

    public new void Update()
    {
        base.Update();
        holoText.text = text.text;
        hologram.transform.LookAt(Camera.main.transform);
    }
    
    protected override void InitiateDesk(Holodesk h)
    {
        if (!hologram)
        {
            holodesk = h;
            hologram = Instantiate(gameObject, holodesk.projector).transform;
            hologram.GetComponent<Hologram>().enabled = false;
            hologram.GetComponent<InfoUI>().enabled = false;
            if (GetComponent<AbstractChecker>()) hologram.GetComponent<AbstractChecker>().enabled = false;
            holoText = hologram.GetComponent<HoloUI>().text;
        }
    }

    protected override void OnTriggerEnter(Collider col)
    {
        base.OnTriggerEnter(col);
        if (hologram && col.gameObject == holodesk.gameObject)
        {
            hologram.gameObject.SetActive(true);
        }
    }

    protected override void OnTriggerExit(Collider col)
    {
        base.OnTriggerExit(col);
        if (hologram && col.gameObject == holodesk.gameObject)
        {   
            hologram.gameObject.SetActive(false);
        }
    }
}
