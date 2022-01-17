using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using Photon.Pun;
public class LocalPlayer : MonoBehaviourPunCallbacks
{
    public GameObject networkLeft;
    public GameObject networkRight;
    public GameObject networkHead;
    private PhotonView photonView;
    void Start()
    {
        photonView = GetComponent<PhotonView>();
        if (photonView.IsMine)
        {
            networkLeft.GetComponent<ActionBasedController>().enabled = true;
            networkLeft.GetComponent<XRDirectInteractor>().enabled = true;
            networkHead.GetComponent<Camera>().enabled = true;
            networkRight.GetComponent<ActionBasedController>().enabled = true;
            networkRight.GetComponent<XRDirectInteractor>().enabled = true;
            networkLeft.GetComponentInChildren<Animator>().enabled = true;
            networkRight.GetComponentInChildren<Animator>().enabled = true;
        }
    }
}