using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisactivateOther : MonoBehaviour
{
    [SerializeField] private GameObject[] go;
    public void Disactivate()
    {
        foreach (GameObject GO in go)
        {
            GO.SetActive(false);
        }
    }
}
