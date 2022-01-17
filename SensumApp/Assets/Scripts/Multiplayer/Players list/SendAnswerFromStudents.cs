using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class SendAnswerFromStudents : MonoBehaviour
{
    private List<GameObject> _playerListing;
    private NetworkManager _networkManager;
    [SerializeField] private InputField sendNumber;
    
    
    private void Start()
    {
        _networkManager = FindObjectOfType<NetworkManager>();
    }

    private void Update()
    {
        _playerListing = _networkManager.PlayerList;
    }

    [PunRPC]
    public void SendAnswer(string name, string answer)
    {
        foreach (var t in _playerListing)
        {
            if (name == t.GetComponent<PlayerListItem>().GetName())
            {
                t.GetComponent<PlayerListItem>().SetAnswer(answer);
            }
        }
    }

    public void SyncSendAnswer()
    {
        GetComponent<PhotonView>().RPC("SendAnswer", RpcTarget.All, PhotonNetwork.NickName, sendNumber.text);
    }
}
