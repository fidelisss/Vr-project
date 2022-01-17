using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class AngleCalculator : MonoBehaviour
{
    public Text Angle;
    private float XaxisAngle;
    private void FixedUpdate()
    {
        GetComponent<PhotonView>().RPC("AngleCalculate", RpcTarget.AllViaServer);
    }

    [PunRPC]
    private void AngleCalculate()
    {
        XaxisAngle = Mathf.Round(gameObject.transform.localEulerAngles.x);    //prints as a string objcts angle in local world space
        Angle.text = XaxisAngle.ToString() + " °";
    }
}
