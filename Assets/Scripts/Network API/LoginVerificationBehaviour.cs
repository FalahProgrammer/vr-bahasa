using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.Networking;


public class LoginVerificationBehaviour : MonoBehaviour
{
    public string url /*= "http://192.168.100.78/vr-bahasa/public/api/v1/login"*/;
    
    [SerializeField] private SendGETMethod _sendGetMethod;
    
    public List<RequestHeader> Header = new List<RequestHeader>();

    [SerializeField] private SendPOSTMethod _sendPostMethod;

    [SerializeField] private RepositoryLoginData _repositoryLoginData;

    [SerializeField] private TMP_InputField _inputFieldEmail;
    
    [SerializeField] private TMP_InputField _inputFieldPass;
    
    [SerializeField] private UnityEvent _onFinished;
    
    [SerializeField] private UnityEvent _onInvalidLogin;
    
    [SerializeField] private UnityEvent _onAccountNotVerified;
    
    [SerializeField] private UnityEvent _onError;

    [SerializeField] private UnityEvent _onEnable;

    private void Awake()
    {
        /*if(GetComponent<SendPOSTMethod>()==null)
        {
            _sendPostMethod = gameObject.AddComponent<SendPOSTMethod>();
        }
        else
        {
            _sendPostMethod = gameObject.GetComponent<SendPOSTMethod>();
        }*/

        url = "http://"+_repositoryLoginData.API_URL + "/vr-bahasa/public/api/v1/login";
        
        //Login();
    }

    private void OnEnable()
    {
        _onEnable?.Invoke();
    }


    public void Login()
    {
        _repositoryLoginData.Header.Clear();
        
        if (url == null)
            return;
        
        var datatosend = new LoginData(){username = _inputFieldEmail.text, password = _inputFieldPass.text};
        //{username = /*_inputFieldEmail.text*/"falah", password = /*_inputFieldPass.text*/"admin123"};
        
        string JSON = JsonUtility.ToJson(datatosend);
        
        Debug.Log(JSON);

        _sendPostMethod.SendPOST(url,JSON,(x)=> { DeserializeLoginResult(x); },Header.ToDictionary(x => x.key, x => x.value));
        
        //_onFinished.AddListener(() =>_sendGetMethod.Invoke("GetDataFromServer",0.1f));
    }

   
    void DeserializeLoginResult(string www)
    {
        _repositoryLoginData.Header.Add(Header[0]);
        
        var result = JsonUtility.FromJson<LoginResponse>(www);
        
        _repositoryLoginData.status_code = result.status_code;
        
        _repositoryLoginData.token = result.token;
        
        Debug.Log("Token code is : " +_repositoryLoginData.token);
        
        Debug.Log("Login status code : " +_repositoryLoginData.status_code);
        
        
        
        switch (result.status_code)
        {
            case 200:
                _onFinished?.Invoke();
                break;
            case 401:
                _onInvalidLogin?.Invoke();
                break;
            case 403:
                _onAccountNotVerified?.Invoke();
                break;
            default:
                _onError?.Invoke();
                break;
        }
        
    }
}

[Serializable]
public class LoginData
{
    public string username;
    public string password;
}

[Serializable]
public class LoginResponse
{
    public int status_code;
    public string token;
    public ClientData data;
}
[Serializable]
public class ClientData
{
    public int id;
    public string username;
    public string email;
    public string name;
    public string email_verified;
    public string licence;

}

