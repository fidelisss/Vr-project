using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeBridge : MonoBehaviour
{
    public Transform Pipe1;
    public Transform Pipe2;

    private float _lastScale1 = 0;
    private float _lastScale2 = 0;

    private Material _material;

    private void Awake()
    {
        _material = GetComponent<Renderer>().material;
    }

    private void Update()
    {
        if(Pipe1.localScale.x != _lastScale1 || Pipe2.localScale.x != _lastScale2)
        {
            _material.SetFloat("_Size1", Pipe1.localScale.x);
            _material.SetFloat("_Size2", Pipe2.localScale.x);
        }

        _lastScale1 = Pipe1.localScale.x;
        _lastScale2 = Pipe2.localScale.x;
    }
}
