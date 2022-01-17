using System.Collections;
using Photon.Pun;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class LoginToGame : MonoBehaviour
{
    [SerializeField] private InputField _usernameField;
    [SerializeField] private InputField _passwordField;
    [SerializeField] private Text _textSuccess;

    [SerializeField] private GameObject _loginObjects;  
    [SerializeField] private GameObject _letters;
    [SerializeField] private GameObject _connectButton;

    [SerializeField] private Button _backButton;
    
    private void LogIn()
    {
        var playerNickname = _usernameField.text;
        PhotonNetwork.NickName = playerNickname;
        PlayerPrefs.SetString("PlayerName", playerNickname);
        StartCoroutine(GetRequest("https://tamos.sensumvr.com/Login.php", _usernameField.text, _passwordField.text));
    }
    
    private IEnumerator GetRequest(string uri, string username, string password)
    {
        var form = new WWWForm();
        form.AddField("username", username);
        form.AddField("password", password);
        using var webRequest = UnityWebRequest.Post(uri, form);
        yield return webRequest.SendWebRequest();
        if (webRequest.isNetworkError || webRequest.isHttpError)
        {
            Debug.Log(webRequest.error);
        }
        else
        {
            _textSuccess.text = webRequest.downloadHandler.text;
            if (webRequest.downloadHandler.text != "Login success") yield break;
            _loginObjects.SetActive(false);
            _letters.SetActive(false);
            _backButton.SetActive(false);
            _textSuccess.SetActive(false);
            _connectButton.SetActive(true);
        }
    }
}
