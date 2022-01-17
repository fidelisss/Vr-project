using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour
{
    [SerializeField] private GameObject _target;

    private void FixedUpdate()
    {
        transform.position = _target.transform.position;
    }
}
