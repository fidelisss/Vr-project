using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{ 
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out SceneTagDeleter sceneTagDeleter))
        {
            sceneTagDeleter.StartClean();
        }
    }
}
