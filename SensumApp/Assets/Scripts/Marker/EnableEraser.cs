using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class EnableEraser : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject toSwitch;

    [PunRPC]
    public void SetActive(bool isActive)
    {
        toSwitch.SetActive(isActive);
    }

    public void SyncSetActive(bool isActive)
    {
        GetComponent<PhotonView>().RPC("SetActive", RpcTarget.All, isActive);
    }
}
