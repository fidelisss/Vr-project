using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class SyncNetworkTransform : MonoBehaviour
{
    [PunRPC]    
    public void SetParentByID(string ID)
    {
        transform.parent = ParentManager.instance.GetParent(ID);
    }
}
