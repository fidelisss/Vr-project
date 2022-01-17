using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class ChangeGender : MonoBehaviour
{
    public bool isMale;

    public void setBool(bool isAc)
    {
        isMale = isAc;
    }
  
    public void MaleInput()
    {
         string temp = PhotonNetwork.NickName + "M";
            PhotonNetwork.NickName = temp;
            PlayerPrefs.SetString("PlayerName", temp);
        
    }

    public void FemaleInput()
    {
        string temp = PhotonNetwork.NickName + "F";
            PhotonNetwork.NickName = temp;
            PlayerPrefs.SetString("PlayerName", temp);
        
    }

    public void SexChanges()
    {
        if (isMale)
        {
            MaleInput();
        }
        else
        {
            FemaleInput();
        }
    }
}
