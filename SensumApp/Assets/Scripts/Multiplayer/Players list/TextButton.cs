using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

public class TextButton : MonoBehaviour
{
    public InputField inputField;
    public bool isMale;
    
    public void AddLetter(string letter)
    {
        inputField.text += letter.ToUpper();
    }

    [PunRPC]
    public void AddNumber(string num)
    {
        inputField.text += num;
    }

    [PunRPC]
    public void AddOthers(string other)
    {
        inputField.text += other;
    }
    
    [PunRPC]
    public void ClearLetter()
    {
        inputField.text = inputField.text.ToString().Remove(inputField.text.ToString().Length - 1);
    }

    public void SyncAddNumber(string letter)
    {
        GetComponent<PhotonView>().RPC("AddNumber", RpcTarget.All, letter);
    }

    
    public void SyncAddOther(string other)
    {
        GetComponent<PhotonView>().RPC("AddOthers", RpcTarget.All, other);
    }

    public void SyncClearLetter()
    {
        GetComponent<PhotonView>().RPC("ClearLetter", RpcTarget.All);
    }
}
