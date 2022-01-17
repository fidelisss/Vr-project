using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Photon.Pun;
using UnityEngine;

public class SceneTagDeleter : MonoBehaviour
{
    [SerializeField] private string _tagName;
    [SerializeField] private string _tagName2;
    [SerializeField] private SoundEffectPlayer _soundEffect;
    [SerializeField] private float _coolDownTimer;
    [SerializeField] private Transform[] _transformsToReset;
    private readonly List<Vector3> _originalPositions = new List<Vector3>();
    private readonly List<Quaternion> _originalRotations = new List<Quaternion>();

    private GameObject[] _deleter;
    private bool _cooldown =  true;

    private void Start()
    {
        foreach (var t in _transformsToReset)
        {
            _originalPositions.Add(t.position);
            _originalRotations.Add(t.rotation);
        }
    }

    public void SyncStartClean()
    {
        GetComponent<PhotonView>().RPC("StartClean", RpcTarget.All);
    }
    
    [PunRPC]
    public void StartClean()
    {
        ResetTransforms();

        #region Refactoring

        //Refactor: Delete searching from tag
        _deleter = GameObject.FindGameObjectsWithTag(_tagName);   
        foreach (var item in _deleter)       //finds every object of this tag and destroying it
            PhotonNetwork.Destroy(item);

        _deleter = GameObject.FindGameObjectsWithTag(_tagName2);
        foreach (var item in _deleter)       //finds every object of this tag and destroying it
            PhotonNetwork.Destroy(item);
        
        #endregion

        var drawers = FindObjectsOfType<Drawing>();
        foreach (var drawing in drawers) 
            PhotonNetwork.Destroy(drawing.gameObject);

        //maybe: Will be fixed...
        /*var spawners = FindObjectsOfType<Spawner>().Where(s => s.PhotonView.IsMine).ToArray();
        foreach (var spawner in spawners) 
            PhotonNetwork.Destroy(spawner.gameObject);*/

        GetComponent<PhotonView>().RPC("PenTrailDeletion", RpcTarget.All);

        if (!_cooldown) return;
        _soundEffect.Play("TrashCan"); _cooldown = false; StartCoroutine(CooldownTimer());

    }

    private IEnumerator CooldownTimer()
    { 
        yield return new WaitForSeconds(_coolDownTimer);

        _cooldown = true;
        yield return null;
    }

    private void ResetTransforms()
    {
        for (var i = 0; i < _transformsToReset.Length; i++)
        {
            _transformsToReset[i].position = _originalPositions[i];
            _transformsToReset[i].rotation = _originalRotations[i];
        }
    }
}
