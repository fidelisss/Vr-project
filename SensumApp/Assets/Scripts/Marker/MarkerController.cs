using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class MarkerController : MonoBehaviour
{
    public GameObject[] markers;
    public GameObject activeMarker;
    private AudioSource audio;
    int currentColor = 0;
    public Color color = Color.white;

    public LineRenderer snapHelperLine;
    public PhotonView photonView;

    public enum Mode
    {
        Pen,
        Line
    };

    public Mode mode;

    public bool _snap;

    public bool snap
    {
        get { return _snap; }
        set
        {
            _snap = value;

            if (snapHelperLine) snapHelperLine.enabled = value;

            if (snap && findClosestSnapPointInstance == null)
            {
                findClosestSnapPointInstance = FindClosestSnapPoint();
                StartCoroutine(findClosestSnapPointInstance);
            }
            else if (!snap && findClosestSnapPointInstance != null)
            {
                StopCoroutine(findClosestSnapPointInstance);
                findClosestSnapPointInstance = null;
            }
        }
    }

    IEnumerator findClosestSnapPointInstance;
    public SnapPoint closestSnapPoint;


    void Start()
    {
        audio = GetComponentInChildren<AudioSource>();
        if(audio)audio.Pause();
        if (mode == Mode.Pen)
        {
            activeMarker = markers[0];
            markers[0].SetActive(true);
        }
        else if (mode == Mode.Line)
        {
            activeMarker = markers[1];
            markers[1].SetActive(true);
        }

        snap = _snap;

        photonView = GetComponent<PhotonView>();
    }

    void Update()
    {
        if (snap && snapHelperLine)
        {
            snapHelperLine.SetPosition(0, snapHelperLine.transform.position);
            if (closestSnapPoint) snapHelperLine.SetPosition(1, (closestSnapPoint.transform.position + snapHelperLine.transform.position) / 2);
            else snapHelperLine.SetPosition(1, snapHelperLine.transform.position);
        }
    }

    [PunRPC]
    public void SwitchMode()
    {
        switch (mode)
        {
            case Mode.Pen:
                mode = Mode.Line;
                SwitchMarkerObject<HoloLine>();
                break;
            case Mode.Line:
                mode = Mode.Pen;
                SwitchMarkerObject<HoloPen>();
                break;
        }
    }

    public void SyncSwitchMode()
    {
        photonView.RPC("SwitchMode", RpcTarget.All);
    }

    public void SwitchMarkerObject<T>()
    {
        foreach (GameObject m in markers)
        {
            if (m.GetComponent<T>() != null)
            {
                m.SetActive(true);
                activeMarker = m;
            }
            else
            {
                m.SetActive(false);
            }
        }
    }

    [PunRPC]
    public void SwitchSnap()
    {
        snap = !snap;
    }

    public void SyncSwitchSnap()
    {
        photonView.RPC("SwitchSnap", RpcTarget.All);
    }
    
    [PunRPC]
    public void SwitchColor()
    {
        switch (currentColor)
        {
            case 0:
                color = Color.red;
                currentColor = 1;
                break;
            case 1:
                color = Color.blue;
                currentColor = 2;
                break;
            case 2:
                color = Color.green;
                currentColor = 3;
                break;
            case 3:
                color = Color.black;
                currentColor = 4;
                break;
            case 4:
                color = Color.white;
                currentColor = 0;
                break;
        }
    }

    public void SyncSwitchColor()
    {
        photonView.RPC("SwitchColor", RpcTarget.All);
    }
    
    [PunRPC]
    public void StartDraw()
    {
        if (!activeMarker.GetComponent<HoloMarker>().draw)
        {
            activeMarker.GetComponent<HoloMarker>().StartDraw();
            if (mode == Mode.Line && snap)
            {
                activeMarker.GetComponent<HoloMarker>().trailTransform.GetComponent<HoloTrail>().snapA =
                    closestSnapPoint;
            }
        }
    }
    
    public void SyncStartDraw()
    {
        Debug.Log(photonView);
        Debug.Log("SyncStartDrawInMarkerController");
        photonView.RPC("StartDraw", RpcTarget.All);
        if(audio)audio.UnPause();
    }

    [PunRPC]
    public void StopDraw()
    {
        if (activeMarker.GetComponent<HoloMarker>().draw)
        {
            if (mode == Mode.Line && snap)
            {
                activeMarker.GetComponent<HoloMarker>().trailTransform.GetComponent<HoloTrail>().snapB = closestSnapPoint;
            }

            activeMarker.GetComponent<HoloMarker>().StopDraw();
        }
    }
    
    public void SyncStopDraw()
    {
        GetComponent<PhotonView>().RPC("StopDraw", RpcTarget.All);
        if (audio) audio.Pause();
    }

    IEnumerator FindClosestSnapPoint()
    {
        while (DrawManager.Instance.SnapPoints.Count != 0)
        {
            float minDist = 9999;
            foreach (SnapPoint s in DrawManager.Instance.SnapPoints)
            {
                float dist = Vector3.Distance(transform.position, s.transform.position);
                if (dist < minDist)
                {
                    minDist = dist;
                    closestSnapPoint = s;
                }
            }

            // yield return new WaitForSeconds(0.1f);
            yield return null;
        }
    }

    public void SetHelperLineActive(bool b)
    {
        snapHelperLine.gameObject.SetActive(b);
    }
}
