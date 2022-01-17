using System.Collections;
using UnityEngine;
using Photon.Pun;
using UnityEngine.Networking;

public class NetworkPlayerSpawner : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject spawnedPlayerPrefab;
    [SerializeField] private Transform xrRig;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private bool[] isFree;
    [SerializeField] private GameObject _playerListMenu;
    private PhotonView _photonView;
    private Transform _spawnPoint;
    private int _mySpawnPos;
    private int _bufferDone = 0;
    private bool _isHeTeacher;
    private int _teacherCount = 0;
    private int _gender;

    private void Start()
    {
        _photonView = GetComponent<PhotonView>();
        isFree = new bool[spawnPoints.Length];
        for (var i = 0; i < spawnPoints.Length; i++)
        {
            isFree[i] = true;
        }
        
        StartCoroutine(GetGender("https://tamos.sensumvr.com/GetGender.php", PhotonNetwork.NickName));
        
        if (PhotonNetwork.NickName == "ADMINMAN" || PhotonNetwork.NickName == "ADMINWOMAN")
        {
            _isHeTeacher = true;
        }
    }

    
    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        foreach (var player in PhotonNetwork.PlayerList)
        {
            if (player.NickName == "ADMINMAN" || player.NickName == "ADMINWOMAN")
            {
                _teacherCount++;
            }
        }
        if (_isHeTeacher && _teacherCount == 1)
        {
            var temp = PhotonNetwork.NickName;
            _playerListMenu.SetActive(true);
            spawnedPlayerPrefab = temp switch
            {
                "ADMINWOMAN" => PhotonNetwork.Instantiate("Network Player Teacher", spawnPoints[0].position, spawnPoints[0].rotation),
                "ADMINMAN" => PhotonNetwork.Instantiate("Network Player Teacher Man", spawnPoints[0].position,
                    spawnPoints[0].rotation),
                _ => spawnedPlayerPrefab
            };

            SyncSetListSpawn(0, false);
        }
        else
        {
            StartCoroutine(WaitForBufferDone());
        }
    }

    private void SpawningStudents()
    {
        for (var i = 1; i < isFree.Length; i++)
        {
            if (!isFree[i]) continue;
            print(isFree[i]);
            _spawnPoint = spawnPoints[i];
            SyncSetListSpawn(i, false);
            _mySpawnPos = i;
            break;
        }

        var position = _spawnPoint.position;
        xrRig.position = position;
        var rotation = _spawnPoint.rotation;
        xrRig.rotation = rotation;
        spawnedPlayerPrefab = _gender switch
        {
            0 => PhotonNetwork.Instantiate("Network Player Student Man", position, rotation),
            1 => PhotonNetwork.Instantiate("Network Player Student", position, rotation),
            _ => spawnedPlayerPrefab
        };
        GameObject studentDesk = PhotonNetwork.Instantiate("Student Desk", position, rotation);
        studentDesk.transform.Find("MyObjects").gameObject.SetActive(true);
    }

    [PunRPC]
    private void SetListSpawn(int i, bool isItFree)
    {
        _bufferDone++;
        isFree[i] = isItFree;
    }

    private IEnumerator WaitForBufferDone()
    {
        while (_bufferDone < PhotonNetwork.CurrentRoom.PlayerCount - 1)
        {
            print(_bufferDone);
            yield return null;
        }
        SpawningStudents();
    }
    
    private void SyncSetListSpawn(int i, bool isItFree)
    {
        GetComponent<PhotonView>().RPC("SetListSpawn", RpcTarget.AllBuffered, i, isItFree);
    }
    
    public override void OnLeftRoom()
    {
        SyncSetListSpawn(_mySpawnPos, true);
        PhotonNetwork.Destroy(spawnedPlayerPrefab);
        base.OnLeftRoom();
    }
    
    private IEnumerator GetGender(string uri, string username)
    {
        var form = new WWWForm();
        form.AddField("username", username);
        using var webRequest = UnityWebRequest.Post(uri, form);
        yield return webRequest.SendWebRequest();
        if (webRequest.isNetworkError || webRequest.isHttpError)
        {
            Debug.Log(webRequest.error);
        }
        else
        {
            _gender = int.Parse(webRequest.downloadHandler.text);
        }
    }
}