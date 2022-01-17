using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class RoleHoloTransmitterDeleter : MonoBehaviour // Kostyl'
{
    private void Start()
    {
        if (!FindObjectOfType<NetworkManager>().IsHeTeacher && PhotonNetwork.InRoom)
            Destroy(GetComponent<HoloTransmitter>());
    }
}
