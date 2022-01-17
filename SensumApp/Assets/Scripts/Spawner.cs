using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private Vector3 _spawnPoint;
    private Vector3 _initialScale;
    private Quaternion _initialRotation;
    private IEnumerator _respawnCoroutineInstance;
    private Rigidbody _rigidbody;
    private float _time;

    public PhotonView PhotonView { get; private set; }

    private void Awake()
    {
        _spawnPoint = transform.position;
        _initialScale = transform.localScale;
        _initialRotation = transform.rotation;
        _rigidbody = GetComponent<Rigidbody>();
        PhotonView = GetComponent<PhotonView>();
    }

    public void Respawn()
    {
        if (_respawnCoroutineInstance == null)
        {
            _respawnCoroutineInstance = RespawnCoroutine();
            StartCoroutine(_respawnCoroutineInstance);
        }
    }

    public void Despawn()
    {
        StartCoroutine(DespawnCoroutine());
    }

    private IEnumerator RespawnCoroutine()
    {
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
        bool initialKinematic = _rigidbody.isKinematic;
        _rigidbody.isKinematic = true;

        float time = 0;

        // Shrink
        while (time < 1f)
        {
            time += Time.deltaTime;
            transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(0.0001f, 0.0001f, 0.0001f), time);
            yield return null;
        }

        transform.position = _spawnPoint;
        transform.rotation = _initialRotation;

        time = 0;
        _rigidbody.isKinematic = initialKinematic;
        _respawnCoroutineInstance = null;

        // Unshrink
        while (time < 1f)
        {
            time += Time.deltaTime;
            transform.localScale = Vector3.Lerp(transform.localScale, _initialScale, time);
            yield return null;
        }
    }

    private IEnumerator DespawnCoroutine()
    {
        float time = 0;
        while (time < 1f)
        {
            time += Time.deltaTime;
            transform.localScale =
                Vector3.Lerp(transform.localScale, new Vector3(0.0001f, 0.0001f, 0.0001f), time / 1f);
            yield return null;
        }

        PhotonNetwork.Destroy(gameObject);
    }
}
