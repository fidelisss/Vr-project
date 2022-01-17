using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XRGrabInteractableHandBased : XRGrabInteractable
{
    [SerializeField] private Transform _attachTransformRight;
    [SerializeField] private Transform _attachTransformLeft;
    
    protected override void OnSelectEntering(SelectEnterEventArgs args)
    {
        base.OnSelectEntering(args);

        XRHandController hand = args.interactor.GetComponentInChildren<XRHandController>();
        if (hand)
        {
            attachTransform =
                hand.handType == HandType.Left ? _attachTransformLeft : _attachTransformRight;
        }
    }
}
