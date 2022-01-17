using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Molecule : MonoBehaviour
{
    [SerializeField] private Vector3 _initialPosition;
    [SerializeField] private float _seedX;
    [SerializeField] private float _seedY;
    [SerializeField] private float _seedZ;
    [SerializeField] private int _seedOffset;
    [SerializeField] private float _time = 0;
    
    public float Speed = 5f;
    public float Amplitude = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        _initialPosition = transform.localPosition;
        _seedX = Random.value;
        _seedY = Random.value;
        _seedZ = Random.value;
        _seedOffset = Random.Range(0, 1000);
        _time += _seedOffset;
    }

    // Update is called once per frame
    void Update()
    {
        _time += Time.deltaTime * Speed;

        float x = Mathf.Asin(Mathf.Sin((_time * _seedX) % 6.283185f)) * Amplitude;
        float y = Mathf.Asin(Mathf.Sin((_time * _seedY) % 6.283185f)) * Amplitude;
        float z = Mathf.Asin(Mathf.Sin((_time * _seedZ) % 6.283185f)) * Amplitude;

        print(new Vector3(x, y, z));

        transform.localPosition = new Vector3(x, y, z) + _initialPosition;
    }
}
