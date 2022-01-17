using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public abstract class AbstractChecker : MonoBehaviour
{
    [SerializeField] public LineInfoUI lineInfo;
    [SerializeField] protected float _scaleFactor;
    public float scaleFactor
    {
        get => _scaleFactor;
        set
        {
            _scaleFactor = value;
            if (TryGetComponent(out PhotonView photonView))
                if (photonView.IsMine) photonView.RPC(nameof(SetScaleFactorRPC), RpcTarget.Others, _scaleFactor);
        }
    }
    [SerializeField] protected string _localizedNotationKey;
    protected string _notation;

    protected void Awake()
    {
        LocalizeNotation();
    }

    protected void OnEnable()
    {
        LocalizationManager.I.onLocalChange += OnLocalChanged;
    }

    protected void OnDisable()
    {
        LocalizationManager.I.onLocalChange -= OnLocalChanged;
    }

    protected void OnLocalChanged()
    {
        LocalizeNotation();
    }

    protected void LocalizeNotation()
    {
        _notation = LocalizationManager.I.Localize(_localizedNotationKey);
    }

    [PunRPC]
    protected void SetScaleFactorRPC(float scale)
    {

    }
}
