using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LateUpdateFollow : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Vector3 offset;
    [SerializeField] private Vector3 q;
    private void OnEnable()
    {   
        Vector3 newForwardVector = (Quaternion.Euler(offset) * player.forward).normalized;
        Vector3 targetPosition = new Vector3(player.position.x + newForwardVector.x, Mathf.Clamp(player.position.y + newForwardVector.y, 1.4f, 2.3f), player.position.z + newForwardVector.z);
        transform.position = targetPosition; 
        
    }
    private void Update()
    {
        transform.LookAt(player);
        transform.eulerAngles = new Vector3(-transform.eulerAngles.x + q.x, transform.eulerAngles.y + q.y, transform.eulerAngles.z + q.z);
    }
}
