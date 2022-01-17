using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasPlayerTracker : MonoBehaviour
{
    private Camera _camera;

    private void Awake() => _camera = Camera.main;

    private void Update() => transform.rotation =
        Quaternion.LookRotation((transform.position - _camera.transform.position).normalized);
}