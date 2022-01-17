using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GrabToolSpawner : XRGrabInteractable
{
    [SerializeField] private GameObject _tool;

    protected override void OnSelectEntering(SelectEnterEventArgs args)
    {
        print("hjf");
        _tool.SetActive(true);
        _tool.transform.position = transform.position;
        _tool.transform.rotation = transform.rotation;


        interactionManager.ForceSelect(args.interactor, _tool.GetComponent<XRGrabInteractable>());
    }
}
