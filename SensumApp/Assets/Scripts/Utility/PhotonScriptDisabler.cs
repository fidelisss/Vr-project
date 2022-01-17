using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Photon.Pun;
using Photon.Realtime;

public class PhotonScriptDisabler : MonoBehaviourPunCallbacks
{
    private PhotonView _photonView;
    [SerializeField] private string[] TypesToDisable;

    private void Awake()
    {
        _photonView = GetComponent<PhotonView>();
    }

    private void Start()
    {
        if (PhotonNetwork.InRoom) DisableComponents();
    }

    private void OnJoinedRoom()
    {
        DisableComponents();
    }

    private void DisableComponents()
    {
        foreach (var _typeStr in TypesToDisable)
        {
            MonoBehaviour comp = GetComponent(Type.GetType(_typeStr)) as MonoBehaviour;
            if (comp) comp.enabled = _photonView.IsMine;
        }
    }
}
