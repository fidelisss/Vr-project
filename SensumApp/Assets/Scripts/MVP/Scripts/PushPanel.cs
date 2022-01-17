using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushPanel : MonoBehaviour
{
    public GameObject Animative;

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Hand")
        {
            Animative.GetComponent<Animator>().SetBool("start", true);
        }
    }
}
