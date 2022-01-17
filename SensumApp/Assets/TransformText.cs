using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformText : MonoBehaviour
{
    private WorldUISegment _rootObject;

    private void Awake()
    {
        _rootObject = GetComponentInParent<WorldUISegment>();
    }

    private void FixedUpdate()
    {
        transform.position = _rootObject.transform.position;
    }
}
