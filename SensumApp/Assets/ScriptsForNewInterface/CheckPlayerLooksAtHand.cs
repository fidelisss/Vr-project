using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CheckPlayerLooksAtHand : MonoBehaviour
{
    [SerializeField] private UnityEvent onLooking;
    [SerializeField] private UnityEvent onNotLooking;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Transform handVector;
    private bool isInCollider = false;
    [Range(0f, 1f)]
    [SerializeField] private float neededDot1;


    private void FixedUpdate()
    {
        isLooking();
    }
    public void isLooking()
    {
        Vector3 handUp = handVector.up;
        Vector3 playerForward = playerTransform.forward;
        float dot1 = Vector3.Dot(playerForward, handUp);
        if (isInCollider)
        {
            if (dot1 <= 1.0f && dot1 >= neededDot1)
            {
                onLooking?.Invoke();
            }
            else onNotLooking?.Invoke();
        }
        else onNotLooking?.Invoke();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("LeftHand"))
        {
            isInCollider = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("LeftHand"))
        {
            isInCollider = false;
        }
    }
}
