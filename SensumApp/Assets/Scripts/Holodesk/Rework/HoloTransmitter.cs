using UnityEngine;

public class HoloTransmitter : MonoBehaviour
{
    public GameObject HologramPrefab;
    public GameObject Hologram;
    public Holodesk Holodesk;

    private void Awake()
    {
        Holodesk = FindObjectOfType<Holodesk>();
    }

    private void Start()
    {
        Hologram = Instantiate(HologramPrefab, Holodesk.projector);
        Hologram.GetComponent<HoloReciever>().Transmitter = this;
        if (Hologram.activeSelf == false)
        {
            Hologram.SetActive(true);
        }
        Hologram.GetComponent<HoloReciever>().SetVisibility(false);
        // if (Hologram.GetComponent<HoloDynamicUIReciever>())
        //     Hologram.GetComponent<HoloDynamicUIReciever>().enabled = true;
        // if (Hologram.GetComponent<HoloUiReciever>())
        //     Hologram.GetComponent<HoloUiReciever>().enabled = true;
    }

    protected void OnTriggerStay(Collider col)
    {
        if (Holodesk == col.GetComponent<Holodesk>())
        {
            Hologram.GetComponent<HoloReciever>().SetVisibility(true);
        }
    }

    protected void OnTriggerExit(Collider col)
    {
        if (Holodesk == col.GetComponent<Holodesk>())
        {
            Hologram.GetComponent<HoloReciever>().SetVisibility(false);
        }
    }

    private void OnDestroy()
    {
        Destroy(Hologram);
    }
}