using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XRGrabInteractableOffset : XRGrabInteractable
{
    Vector3 interactorPosition = Vector3.zero;
    Quaternion interactorRotation = Quaternion.identity;

    private PhotonView photonView;
    public new void Awake()
    {
        base.Awake();
        photonView = GetComponent<PhotonView>();
    }
    
    protected override void OnSelectEntering(SelectEnterEventArgs args)
    {
        // GetComponent<PhotonView>().RequestOwnership();
        base.OnSelectEntering(args);

        XRBaseInteractor interactor = args.interactor;
        StoreInteractor(interactor);
        MatchAttachmentPoints(interactor);
        
        if (photonView) photonView.RequestOwnership();
        else Debug.Log("No photon view!");
    }
    

    void StoreInteractor(XRBaseInteractor interactor)
    {
        interactorPosition = interactor.attachTransform.localPosition;
        interactorRotation = interactor.attachTransform.localRotation;
    }

    void MatchAttachmentPoints(XRBaseInteractor interactor)
    {
        bool hasAttach = attachTransform != null;
        interactor.attachTransform.position = hasAttach ? attachTransform.position : transform.position;
        interactor.attachTransform.rotation = hasAttach ? attachTransform.rotation : transform.rotation;
    }

    protected override void OnSelectExiting(SelectExitEventArgs args)
    {
        base.OnSelectExiting(args);
        XRBaseInteractor interactor = args.interactor;
        ReserAttachmentPoints(interactor);
        ClearInteractor(interactor);
    }

    void ReserAttachmentPoints(XRBaseInteractor interactor)
    {
        interactor.attachTransform.localPosition = interactorPosition;
        interactor.attachTransform.localRotation = interactorRotation;
    }

    void ClearInteractor(XRBaseInteractor interactor)
    {
        interactorPosition = Vector3.zero;
        interactorRotation = Quaternion.identity;
    }

    // protected override void OnSelectEntered(XRBaseInteractor interactor)
    // {
    //     photonView.RequestOwnership();
    //     base.OnSelectEntered(interactor);
    // }
}
