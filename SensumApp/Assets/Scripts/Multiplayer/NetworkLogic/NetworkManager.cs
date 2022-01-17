using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

[System.Serializable]
public class DefaultRoom
{   
    public string name;
    public int sceneIndex;
    public int maxPlayer;
}

public class NetworkManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private List<DefaultRoom> defaultRooms;
    [SerializeField] private GameObject room1;
    [SerializeField] private GameObject room2;
    [SerializeField] private GameObject room3;
    [SerializeField] private int playerCount;
    [SerializeField] private Transform playListContent;
    [SerializeField] private GameObject playListPrefab;
    [SerializeField] private List<GameObject> playersList = new List<GameObject>();
    
    public List<GameObject> PlayerList => playersList;

    private bool _isHeTeacher;
    public bool IsHeTeacher
    {
        get => _isHeTeacher;
    }
    
    private int _teacherCount = 0;

    public event UnityAction ConnectedPlayer;
    
    
    private void Start()
    {
        if (PhotonNetwork.NickName == "ADMINMAN" || PhotonNetwork.NickName == "ADMINWOMAN") 
        {
            _isHeTeacher = true;
        }
    }

    public void ConnectToServer()
    {
        PhotonNetwork.ConnectUsingSettings();
        Debug.Log("Try to connect");
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to master");
        base.OnConnectedToMaster();
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        Debug.Log("Joined to Lobby");
        room1.SetActive(true);
        //room2.SetActive(true);
        //room3.SetActive(true);
    }

    public void InitializeRoom(int defaultRoomIndex)
    {
        var roomSettings = defaultRooms[defaultRoomIndex];
        if (playerCount < 0) return;
        PhotonNetwork.LoadLevel(roomSettings.sceneIndex);

        var roomOptions = new RoomOptions
        {
            MaxPlayers = (byte) roomSettings.maxPlayer,
            IsVisible = true,
            IsOpen = true
        };

        PhotonNetwork.JoinOrCreateRoom(roomSettings.name, roomOptions, TypedLobby.Default);
    }

    public override void OnJoinedRoom()
    {
        InitiatePlayerList();
        foreach (var player in PhotonNetwork.PlayerList)
        {
            if (player.NickName == "ADMINMAN" || player.NickName == "ADMINWOMAN")
            {
                _teacherCount++;
            }
        }
        Debug.Log("Joined to lo bby");
        base.OnJoinedRoom();
    }

    [PunRPC]
    private void LeaveGame()
    {
        if (_isHeTeacher)
        {
            LeaveStudents();
            Leave();
        }
        Leave();
    }

    [PunRPC]
    private void LeaveStudents()
    {
        GetComponent<NetworkManager>().Leave();
    }
    
    [PunRPC]
    private void Leave()
    {
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.Disconnect();
        SceneManager.LoadScene(0);
    }

    public void SyncLeaveGame()
    {
        Leave();
    }

    public bool isTeacher(Player player)
    {
        return player.NickName == "ADMINMAN" || player.NickName == "ADMINWOMAN";
    }
    
    [PunRPC]
    private void InitiatePlayerList()
    {
        var players = PhotonNetwork.PlayerList;
        foreach (var player in players)
        {
            if (isTeacher(player)) continue;
            var playerInfo = (GameObject) Instantiate(playListPrefab, playListContent);
            playerInfo.GetComponent<PlayerListItem>().SetUp(player);
            playersList.Add(playerInfo);
            ConnectedPlayer?.Invoke();
        }
    }
    
    private void UpdatePlayerList()
    {
        var players = PhotonNetwork.PlayerList;
        var playerInfo = (GameObject) Instantiate(playListPrefab, playListContent);
        playerInfo.GetComponent<PlayerListItem>().SetUp(players.Last());
        playersList.Add(playerInfo);
        ConnectedPlayer?.Invoke();
    }
    

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        UpdatePlayerList();
        base.OnPlayerEnteredRoom(newPlayer);
    }
    
    public override void OnPlayerLeftRoom(Player player)
    {
        if (isTeacher(player)) Leave();
        base.OnPlayerEnteredRoom(player);
    }

    public override void OnLeftRoom()
    {
        //if (_isHeTeacher && _teacherCount == 1)
        //{
            //GetComponent<PhotonView>().RPC("Leave", RpcTarget.OthersBuffered);
        //}
        Leave();
        base.OnLeftRoom();
    }
}