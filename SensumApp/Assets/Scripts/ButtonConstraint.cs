using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonConstraint : MonoBehaviour
{
    private Transform t;

    void Awake()
    {
        t = transform;
    }

    void Update()
    {
        if (t.localPosition.y > 0.4f) t.localPosition = new Vector3 (0, 0.4f, 0);
    }
}
