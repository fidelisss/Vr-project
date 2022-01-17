using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoloMoleculeReciever : HoloReciever
{
    protected override void ApplyCustomData()
    {
        GetComponent<Item>().temperature = Transmitter.GetComponent<Item>().temperature;
    }
}
