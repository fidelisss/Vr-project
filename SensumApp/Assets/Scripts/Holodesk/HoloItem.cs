using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoloItem : Hologram
{
    private IEnumerator opacityCoroutine;

    public new void Start()
    {
        base.Start();
        InitiateDesk(holodesk);
        ChangeOpacity(0);
    }

    protected override void OnTriggerEnter(Collider col)
    {
        base.OnTriggerEnter(col);
        if (hologram && col.gameObject == holodesk.gameObject)
        {
            ChangeOpacity(1);
        }
    }

    protected override void OnTriggerExit(Collider col)
    {
        base.OnTriggerExit(col);
        if (hologram && col.gameObject == holodesk.gameObject)
        {   
            ChangeOpacity(0);
        }
    }
    
    protected override void InitiateDesk(Holodesk h)
    {
        if (!isHolo)
        {
            holodesk = h;
            hologram = Instantiate(gameObject, holodesk.projector).transform;
            if (hologram.GetComponent<Renderer>()) hologram.GetComponent<Renderer>().material.shader = Shader.Find("Shader Graphs/Dither");
            hologram.GetComponent<Hologram>().isHolo = true;
            hologram.GetComponent<Hologram>().enabled = false;
            if (hologram.GetComponent<Rigidbody>()) hologram.GetComponent<Rigidbody>().isKinematic = true;
            if (hologram.GetComponent<Collider>()) hologram.GetComponent<Collider>().enabled = false;
            hologram.GetComponent<Renderer>().material.SetFloat("_Opacity", 0);
        }
    }

    void ChangeOpacity(float opacity)
    {
        if (opacityCoroutine != null)
            StopCoroutine(opacityCoroutine);

        opacityCoroutine = OpacityCoroutine(opacity);
        StartCoroutine(opacityCoroutine);
    }

    IEnumerator OpacityCoroutine(float target)
    {
        float op = hologram.GetComponent<Renderer>().material.GetFloat("_Opacity");

        foreach(Transform t in hologram.transform)
            t.gameObject.SetActive(target == 0 ? false : true);

        while (Mathf.Abs(op - target) > 0.001f)
        {
            op = Mathf.Lerp(op, target, Time.deltaTime*10);
            hologram.GetComponent<Renderer>().material.SetFloat("_Opacity", op);
            yield return null;
        }

        // if (target == 0)
        // {
        //     DeinitiateDesk();
        // }
    }
}
