using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class PlaybackTimeController : MonoBehaviour
{
    private Animator _animator;
    private float _time = 0f;
    public float CurrentTime
    {
        get { return _time; }
    }
    private PhotonView _photonView;

    public float Speed { get; set; }
    public bool Active { get; set; }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _photonView = GetComponent<PhotonView>();
        
        // give speed standard slider value
        Speed = 0.5f;
    }

    private void Update()
    {
        if (_photonView.IsMine) PlayAnimation();
    }

    private void PlayAnimation()
    {
        _time += Time.deltaTime * Speed * (Active ? 1 : 0);
        _animator.SetFloat("PlaybackTime", _time);
        if (_time >= 1f) _time = 0f;
    }

    // [PunRPC]
    // private void SetPlaybackTime(float time)
    // {
    //     _animator.SetFloat("PlaybackTime", time);
    // }
    //
    // private void SyncPlaybackTime(float time)
    // {
    //     GetComponent<PhotonView>().RPC("SetPlaybackTime", RpcTarget.All, time);
    // }
}
