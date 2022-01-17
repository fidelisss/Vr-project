using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;
public class RpcToggle : MonoBehaviour
{
    private PhotonView _photonView;

    private void Awake()
    {
        _photonView = GetComponent<PhotonView>();
    }

    private void Start()
    {
        if (_photonView.ViewID == 0) PhotonNetwork.AllocateViewID(_photonView);
    }

    public void SyncToggle(bool toggle)
    {
        if (_photonView.IsMine)
            _photonView.RPC("SwitchToggle", RpcTarget.OthersBuffered, toggle);
    }

    [PunRPC]
    public void SwitchToggle(bool _toggle)
    {
        gameObject.GetComponent<Toggle>().isOn = _toggle;
    }
}
