using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class SkyboxChange : MonoBehaviour
{
    public Cubemap InitialSkybox;
    public float InitialIntensity;
    public Cubemap[] Skyboxes;
    public float[] SkyboxIntensities;
    private Material _skyboxMaterial;
    public float ChangeSpeed = 1;
    private IEnumerator _skyboxChangeCoroutineInstance;
    private IEnumerator _waitCoroutineInstance;

    private int _lastPos = 0;

    public void Start()
    {
        _skyboxMaterial = RenderSettings.skybox;
        _skyboxMaterial.SetTexture("_Skybox1", InitialSkybox);
        _skyboxMaterial.SetTexture("_Skybox2", InitialSkybox);
        _skyboxMaterial.SetFloat("_Intensity1", InitialIntensity);
        _skyboxMaterial.SetFloat("_Intensity2", InitialIntensity);
        _skyboxMaterial.SetFloat("_Transition", 1);
    }

    [PunRPC]
    public void ChangeSkybox(int pos)
    {
        // RenderSettings.skybox = myMaterials[pos];
        if (_lastPos != pos)
        {
            if (_skyboxChangeCoroutineInstance == null)
            {
                _skyboxChangeCoroutineInstance = SkyboxChangeCoroutine(pos);
                StartCoroutine(_skyboxChangeCoroutineInstance);
            }
            else if (_waitCoroutineInstance == null)
            {
                _waitCoroutineInstance = WaitCoroutine(pos);
                StartCoroutine(_waitCoroutineInstance);
            }
            else
            {
                StopCoroutine(_waitCoroutineInstance);
                _waitCoroutineInstance = WaitCoroutine(pos);
                StartCoroutine(_waitCoroutineInstance);
            }
            _lastPos = pos;
        }
    }

    public void SyncChangeSkybox(int pos)
    {
        if (TryGetComponent(out PhotonView photonView) && PhotonNetwork.InRoom)
        {
            photonView.RPC("ChangeSkybox", RpcTarget.AllViaServer, pos);
        }
        else
        {
            ChangeSkybox(pos);
        }
        // GetComponent<PhotonView>().RPC("ChangeSkybox", RpcTarget.AllViaServer, pos);
    }

    private IEnumerator SkyboxChangeCoroutine(int pos)
    {
        _skyboxMaterial.SetFloat("_Transition", 1);
        _skyboxMaterial.SetTexture("_Skybox1", Skyboxes[pos]);
        _skyboxMaterial.SetFloat("_Intensity1", SkyboxIntensities[pos]);

        float time = 0.5f;
        while (time > -1)
        {
            time -= Time.deltaTime * ChangeSpeed;
            _skyboxMaterial.SetFloat("_Transition", time);
            yield return null;
        }

        _skyboxMaterial.SetTexture("_Skybox2", Skyboxes[pos]);
        _skyboxMaterial.SetFloat("_Intensity2", SkyboxIntensities[pos]);
        _skyboxChangeCoroutineInstance = null;
    }

    private IEnumerator WaitCoroutine(int pos)
    {
        while (_skyboxChangeCoroutineInstance != null)
        {
            yield return null;
        }
        _skyboxChangeCoroutineInstance = SkyboxChangeCoroutine(pos);
        StartCoroutine(_skyboxChangeCoroutineInstance);
        _waitCoroutineInstance = null;
    }


    public void IncrementSkybox(int i)
    {
        ChangeSkybox(_lastPos + i);
    }
}
