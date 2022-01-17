using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class Rotator : MonoBehaviour
{
    public Text SpeedText;
    [SerializeField] WeightPos _weight;
    [SerializeField] private string _localizedNotationKey;
    [SerializeField] private string _localizedTextKey;
    [SerializeField] private string _stringFormat = "G";

    private string _text;
    private string _notation;
    private Vector3 Speed;
    private Vector3 Torque;
    private Rigidbody rb;
    private float YaxisSpeed;
    private PhotonView _photonView;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.maxAngularVelocity = 50f;
        _photonView = GetComponent<PhotonView>();
        LocalizeTexts();
    }

    private void OnEnable()
    {
        LocalizationManager.I.onLocalChange += OnLocalChanged;
    }

    private void OnDisable()
    {
        LocalizationManager.I.onLocalChange -= OnLocalChanged;
    }

    private void FixedUpdate()
    {
        Speed = Torque / _weight.roundedRadius;

        if (Speed.magnitude > 0.01f) transform.Rotate(Speed);

        if (Speed.y >= 0)
            SpeedText.text = $"{_text} = {Speed.y.ToString(_stringFormat)} {_notation}";        //Print speed to canvas 
        else
            SpeedText.text = $"{_text} = {(Speed.y * -1).ToString(_stringFormat)} {_notation}";
    }

    public void SyncSetTorqueY(float value)
    {
        SetTorqueY(value);
        print("localTorque");
        _photonView.RPC("SetTorqueY", RpcTarget.Others, value);
    }

    [PunRPC]
    public void SetTorqueY(float value)
    {
        print("networkTorque");
        Torque.y = value * Time.deltaTime * 72; 
    }

    private void OnLocalChanged()
    {
        LocalizeTexts();
    }

    private void LocalizeTexts()
    {
        _text = LocalizationManager.I.Localize(_localizedTextKey);
        _notation = LocalizationManager.I.Localize(_localizedNotationKey);
    }
}
