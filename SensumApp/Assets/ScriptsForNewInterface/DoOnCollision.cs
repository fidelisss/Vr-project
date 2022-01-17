using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DoOnCollision : MonoBehaviour
{
    [SerializeField] private UnityEvent OnCollisionEnterEvent;

    private void OnTriggerEnter(Collider other)
    {
        OnCollisionEnterEvent?.Invoke();
    }
}
