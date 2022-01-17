using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class SendToGoogleSheet : MonoBehaviour
{
    [SerializeField] private InputField result;
    [SerializeField] private Dropdown _dropdownOfTopic;
    private Text[] _names;
    private Text[] _scores;

    private PlayerListItem[] _playerListItems;
    private NetworkManager _manager;

    private string _name;
    private string _score;
    private string classOfStudent;

    public SendToGoogleSheet(Text[] names, Text[] scores)
    {
        this._names = names;
        this._scores = scores;
    }

    private void Awake() => _manager = FindObjectOfType<NetworkManager>();

    private void OnEnable() => _manager.ConnectedPlayer += OnConnectedPlayer;

    private void OnDisable() => _manager.ConnectedPlayer -= OnConnectedPlayer;

    private void OnConnectedPlayer() => _playerListItems = GetComponentsInChildren<PlayerListItem>();
    
    public void Send()
    {
        var result = this.result.text;
        var dropDownValueTopic = _dropdownOfTopic.value;
        var dropDownStringTopic = _dropdownOfTopic.options[dropDownValueTopic].text;
        foreach (var player in _playerListItems)
        {
            _name = player.TextName.text;
            _score = player.TextAnswer.text;
            StartCoroutine(GetClass("https://tamos.sensumvr.com/GetName.php", _name));
            StartCoroutine(GetRequest("https://tamos.sensumvr.com/GetData.php", _name, _score, result, dropDownStringTopic));
        }
    }
    
    private IEnumerator GetRequest(string uri, string name_of, string score_of, string result, string topic)
    {
        var form = new WWWForm();
        form.AddField("name", name_of);
        form.AddField("score", score_of);
        form.AddField("class", classOfStudent);
        form.AddField("result", result);
        form.AddField("topic", topic);
        using var webRequest = UnityWebRequest.Post(uri, form);
        yield return webRequest.SendWebRequest();
        if (webRequest.isNetworkError || webRequest.isHttpError)
        {
            Debug.Log(webRequest.error);
        }
    }
    
    private IEnumerator GetClass(string uri, string username)
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
            classOfStudent = webRequest.downloadHandler.text;
        }
    }
}
