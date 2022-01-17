
using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class StopWatch : MonoBehaviour
{
    private bool timerActive = false;
    private float currentTime;          
    private String testText;
    public Text currentTimeText;

    void Start()
    {
        currentTime = 0;    
    }

    void Update()
    {
        if (timerActive == true)
        {
            currentTime = currentTime + Time.deltaTime; 
        }
        TimeSpan time = TimeSpan.FromSeconds(currentTime);
        currentTimeText.text = time.ToString(@"mm\:ss\:fff");
    }

    [PunRPC]
    private void StartTime()
    {
        currentTime = 0;
        timerActive = true;
    }

    [PunRPC]
    private void StopTime(float finalTime)
    {
        currentTime = finalTime;
        timerActive = false;
    }

    public void SyncStartTime()
    {
        if (TryGetComponent(out PhotonView photonView))
        {
            if (photonView.IsMine) photonView.RPC("StartTime", RpcTarget.All);
        }
        else StartTime();
    }
    
    public void SyncStopTime()
    {
        if (TryGetComponent(out PhotonView photonView))
        {
            if (photonView.IsMine) photonView.RPC("StopTime", RpcTarget.All, currentTime);
        }
        else StopTime(currentTime);
    }
}