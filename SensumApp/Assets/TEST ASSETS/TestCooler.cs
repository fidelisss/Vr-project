using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCooler : MonoBehaviour
{
    public float Bandwidth = 0;
    public float EnvironmentTemp = 250;
    
    private Item _item;

    private void Awake()
    {
        _item = GetComponent<Item>();
    }

    private void Update()
    {
        if(_item.temperature > EnvironmentTemp)
            _item.temperature -= Bandwidth * Time.deltaTime;
    }
}
