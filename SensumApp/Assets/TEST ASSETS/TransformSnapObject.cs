using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TransformSnapObject : MonoBehaviour
{
    public XRBaseInteractor Interactor;

    public void SetInteractor(SelectEnterEventArgs args)
    {
        Interactor = args.interactor;
    }
    
    public void ResetInteractor()
    {
        Interactor = null;
    }
}
