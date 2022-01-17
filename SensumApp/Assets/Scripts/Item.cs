using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;


public class Item : MonoBehaviour
{
    bool _isHeld = false;

    public bool isHeld
    {
        get { return _isHeld; }
        set { _isHeld = value; }
    }

    [Header("Item properties")] public string itemName = "Item";
    public float mass;
    public float volume;
    public float density;
    public float radius;
    public float temperature;

    public Vector3 spawnPoint;
    public Vector3 initialScale;
    public Quaternion initialRotation;

    private IEnumerator _respawnCoroutineInstance;


    // OLD SYSTEM START

    float time;

    public void Start()
    {
        spawnPoint = transform.position;
        initialScale = transform.localScale;
        initialRotation = transform.rotation;
    }

    public virtual float CalcMass()
    {
        return mass;
    }

    public void Respawn()
    {
        if (_respawnCoroutineInstance == null)
        {
            _respawnCoroutineInstance = RespawnCoroutine();
            StartCoroutine(_respawnCoroutineInstance);
        }
    }

    public void Delete()
    {
        StartCoroutine(DeleteCoroutine());
    }

    IEnumerator RespawnCoroutine()
    {
        Rigidbody rigid = GetComponent<Rigidbody>();
        Collider col = GetComponent<Collider>();
        rigid.velocity = Vector3.zero;
        bool initialKinematic = rigid.isKinematic;
        rigid.isKinematic = true;
        // col.enabled = false;

        time = 0;
        while (time < 1f)
        {
            time += Time.deltaTime;
            transform.localScale =
                Vector3.Lerp(transform.localScale, new Vector3(0.0001f, 0.0001f, 0.0001f), time / 1f);
            yield return null;
        }

        transform.position = spawnPoint;
        transform.rotation = initialRotation;
        rigid.velocity = Vector3.zero;

        time = 0;
        col.enabled = true;
        rigid.isKinematic = initialKinematic;
        _respawnCoroutineInstance = null;
        while (time < 1f)
        {
            time += Time.deltaTime;
            transform.localScale = Vector3.Lerp(transform.localScale, initialScale, time / 1f);
            yield return null;
        }
    }

    IEnumerator DeleteCoroutine()
    {
        time = 0;
        while (time < 1f)
        {
            time += Time.deltaTime;
            transform.localScale =
                Vector3.Lerp(transform.localScale, new Vector3(0.0001f, 0.0001f, 0.0001f), time / 1f);
            yield return null;
        }

        PhotonNetwork.Destroy(gameObject);
    }

    // OLD SYSTEM END
}