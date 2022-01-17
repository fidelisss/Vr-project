using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Fire : MonoBehaviour
{
    public float Bandwidth = 0;
    public float MaxTemp = 415;
    
    private List<Item> _itemList = new List<Item>();

    private void Update()
    {
        foreach (var item in _itemList.Where(item => item.temperature < MaxTemp))
        {
            item.temperature += Bandwidth * Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Item item = other.GetComponent<Item>();
        if(item)
            if(!_itemList.Contains(item))
                _itemList.Add(item);
    }

    private void OnTriggerExit(Collider other)
    {
        Item item = other.GetComponent<Item>();
        if(item)
            if(_itemList.Contains(item))
                _itemList.Remove(item);
    }
}
