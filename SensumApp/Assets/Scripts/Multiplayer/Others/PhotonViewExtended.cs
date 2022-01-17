using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.Events;

// NOT FINISHED, DO NOT USE
public class PhotonViewExtended : PhotonView
{
    public event UnityAction OwnerChanged;

    private Player _owner;
    new public Player Owner 
    { 
        get { return _owner; }
        private set
        {
            _owner = value;
            OwnerChangedInvoke();
        } 
    }

    new public void TransferOwnership(Player newOwner)
    {
        base.TransferOwnership(newOwner);
        OwnerChangedInvoke();
    }

    public void OwnerChangedInvoke()
    {
        print("OwnerChanged event triggered");
        print(Owner);
        OwnerChanged?.Invoke();
    }
}
