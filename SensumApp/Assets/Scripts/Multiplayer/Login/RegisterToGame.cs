using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class RegisterToGame : MonoBehaviour
{
    
    [SerializeField] private InputField usernameField;
    [SerializeField] private InputField passwordField;
    [SerializeField] private Text textSuccess;

    [SerializeField] private GameObject registerObjects;  
    [SerializeField] private GameObject letters;
    [SerializeField] private Dropdown sexOptions;
    [SerializeField] private Dropdown classOptions;

    [SerializeField] private Button backButton;
    
    public void Register()
    {
        var dropDownValueGender = sexOptions.value;
        var dropDownStringGender = sexOptions.options[dropDownValueGender].text;
        var dropDownValueClass = classOptions.value;
        var dropDownStringClass = classOptions.options[dropDownValueGender].text;
        switch (dropDownStringGender)
        {
            case "Female":
                StartCoroutine(GetRequest("https://tamos.sensumvr.com/Register.php", usernameField.text, passwordField.text, 1, dropDownStringClass));
                break;
            case "Male":
                StartCoroutine(GetRequest("https://tamos.sensumvr.com/Register.php", usernameField.text, passwordField.text, 0, dropDownStringClass));
                break;
        }
    }
    
    private IEnumerator GetRequest(string uri, string username, string password, int gender, string classname)
    {
        var form = new WWWForm();
        form.AddField("username", username);
        form.AddField("password", password);
        form.AddField("gender", gender);
        form.AddField("class", classname);
        using var webRequest = UnityWebRequest.Post(uri, form);
        yield return webRequest.SendWebRequest();
        if (webRequest.isNetworkError || webRequest.isHttpError)
        {
            Debug.Log(webRequest.error);
        }
        else
        {
            textSuccess.text = webRequest.downloadHandler.text;
            if (webRequest.downloadHandler.text != "Creating new userNew record created successfully") yield break;
            registerObjects.SetActive(false);
            letters.SetActive(false);
            backButton.SetActive(false);
            textSuccess.SetActive(false);
            backButton.onClick.Invoke();
        }
    }
}
