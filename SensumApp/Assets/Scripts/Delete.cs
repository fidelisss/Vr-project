using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Delete : MonoBehaviour
{ // please change name
    void Start()
    {
        StartCoroutine(TrashHandler());
    }

    IEnumerator TrashHandler()
    {
        while (true)
        {
            int t = 0;
            foreach (Transform g in FindObjectsOfType<Transform>())
            {
                if (g.position.z < -80 || g.position.z > 80) Destroy(g.gameObject);

                t++;
                if (t > 20) yield return null;
            }
        }
        
    }
}
