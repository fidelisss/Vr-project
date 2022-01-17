using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class InteractGrabIcon : XRGrabInteractable
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private string _parentUniqueID;
    
    private PhotonView _photonView;
    public bool _isGrip;
    
    protected override void Awake()
    {
        base.Awake();
        _photonView = GetComponent<PhotonView>();
    }

    protected override void OnSelectEntering(SelectEnterEventArgs args)
    {
        GameObject obj = null;
        if (PhotonNetwork.InRoom)
        {
            obj = PhotonNetwork.Instantiate(_prefab.name, transform.position, Quaternion.identity);
            _photonView.RequestOwnership();
            obj.tag = "SpawnedItem";
            obj.GetComponent<PhotonView>().RPC("SetParentByID", RpcTarget.All, _parentUniqueID);
        }
        else
        {
            obj = Instantiate(_prefab, transform.position, Quaternion.identity);
            obj.tag = "SpawnedItem";
            if (obj.TryGetComponent(out SyncNetworkTransform syncNetworkTransform))
                syncNetworkTransform.SetParentByID(_parentUniqueID);
        }        
        GetComponent<XRGrabInteractable>().interactionManager
            .ForceSelect(args.interactor, obj.GetComponent<XRGrabInteractable>());
    }

    // [PunRPC]
    // void SyncParent(GameObject obj, Transform parent)
    // {
    //     obj.transform.parent = parent;
    // }
    // private Transform GetParent()
    // {kj
    //     if (targetParent) return targetParent;
    //     return ParentManager.instance.GetParent(parentUniqueID);
    // }
}
