using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetActive : MonoBehaviour
{
    [SerializeField] private GameObject go;
    public void DisactivateObject()
    {
        go.SetActive(false);
    }
}
