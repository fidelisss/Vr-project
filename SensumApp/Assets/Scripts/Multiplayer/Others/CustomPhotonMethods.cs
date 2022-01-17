using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class CustomPhotonMethods : MonoBehaviourPunCallbacks
{
    private byte CustomManualInstantiationEventCode = 159;
    private GameObject _toInstantiate;
    
    public GameObject CustomInstantiate(GameObject objectToInstantiate, Vector3 objectPos, Quaternion objectRot)
    {
        _toInstantiate = Instantiate(objectToInstantiate, objectPos, objectRot);
        PhotonView photonView = _toInstantiate.GetComponent<PhotonView>();

        if (PhotonNetwork.AllocateViewID(photonView))
        {
            object[] data = new object[]
            {
                _toInstantiate.transform.position, _toInstantiate.transform.rotation, photonView.ViewID, 
            };

            RaiseEventOptions raiseEventOptions = new RaiseEventOptions
            {
                Receivers = ReceiverGroup.Others,
                CachingOption = EventCaching.AddToRoomCache
            };

            SendOptions sendOptions = new SendOptions
            {
                Reliability = true
            };

            PhotonNetwork.RaiseEvent(CustomManualInstantiationEventCode, data, raiseEventOptions, sendOptions);
        }
        else
        {
            Debug.LogError("Failed to allocate a ViewId.");

            Destroy(_toInstantiate);
        }

        return _toInstantiate;
    }


    public void OnEvent(EventData photonEvent)
    {
        if (photonEvent.Code == CustomManualInstantiationEventCode)
        {
            object[] data = (object[]) photonEvent.CustomData;

            GameObject objectInstantiated = (GameObject) Instantiate(_toInstantiate, (Vector3) data[0], (Quaternion) data[1]);
            PhotonView photonView = objectInstantiated.GetComponent<PhotonView>();
            photonView.ViewID = (int) data[2];
        }
    }
}
