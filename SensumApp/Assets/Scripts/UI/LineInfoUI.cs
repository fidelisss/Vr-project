using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class LineInfoUI : InfoUI
{
    [SerializeField] public Text text;

    private Transform _startPoint;
    public Transform startPoint
    {
        get { return _startPoint; }
        set
        {
            _startPoint = value;
            if (TryGetComponent(out PhotonView photonViewStart))
                if (photonViewStart.IsMine) photonViewStart.RPC(nameof(SetStartPointRPC), RpcTarget.Others, _startPoint.GetComponent<PhotonView>().ViewID);
            if (_startPoint.GetComponent<Item>())
                startItem = _startPoint.GetComponent<Item>();
        }
    }
    private Transform _endPoint;
    public Transform endPoint
    {
        get { return _endPoint; }
        set
        {
            _endPoint = value;
            if (TryGetComponent(out PhotonView photonViewEnd))
                if (photonViewEnd.IsMine) photonViewEnd.RPC(nameof(SetEndPointRPC), RpcTarget.Others, _endPoint.GetComponent<PhotonView>().ViewID);
            if (_startPoint.GetComponent<Item>())
                endItem = _endPoint.GetComponent<Item>();
        }
    }
    private Item startItem;
    private Item endItem;
    private float startBias = 0;
    private float endBias = 0;
    public float verticalBias = 0;
    public string textValue;
    private Vector3 direction;

    private void Update()
    {
        if (!startPoint || !endPoint) Destroy(gameObject);

        startBias = startItem.radius;
        endBias = endItem.radius;
        direction = Vector3.Normalize(endPoint.position - startPoint.position);
        MoveCanvas();
        // RotateCanvas();
    }

    private void RotateCanvas()
    {
        // transform.right = Vector3.ProjectOnPlane(direction, Vector3.up);
        // transform.LookAt(Camera.main.transform);
    }

    private void MoveCanvas()
    {
        // center + horizontal bias + vertical bias
        transform.position = (startPoint.position + endPoint.position + startBias * direction - endBias * direction) / 2 + new Vector3(0, verticalBias, 0);
    }

    [PunRPC]
    private void SetStartPointRPC(int viewID)
    {
        Transform t = PhotonView.Find(viewID).transform;
        startPoint = t;
    }

    [PunRPC]
    private void SetEndPointRPC(int viewID)
    {
        Transform t = PhotonView.Find(viewID).transform;
        endPoint = t;
    }
}
