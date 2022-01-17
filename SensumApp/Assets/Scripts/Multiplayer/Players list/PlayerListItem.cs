using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

public class PlayerListItem : MonoBehaviourPunCallbacks
{
    [SerializeField] private Text textName;
    [SerializeField] private Text textAnswer;
    public Text TextName => textName;
    public Text TextAnswer => textAnswer;
    private Player _player;

    public void SetUp(Player player)   
    {
        this._player = player;
        textName.text = player.NickName;
    }

    public string GetName()
    {
        return textName.text;
    }

    public void SetAnswer(string textFrom)
    {
        textAnswer.text = textFrom;
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        if (Equals(_player, otherPlayer))
        {
            Destroy(gameObject);
        }
    }
}
