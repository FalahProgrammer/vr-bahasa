using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

public class SendGETMethod : MonoBehaviour
{
    [SerializeField] private bool debugMod;
    [Space(10)] 
    [SerializeField] private bool _isStart;
    
    // hardcoded ?
    private string URL = "http://localhost/vr-bahasa/public/api/v1/tokens";
    
    [Space(10)] 
    public List<RequestHeader> Header = new List<RequestHeader>();
    
    [SerializeField] private RepositoryMateri _repositoryMateri;
    
    [SerializeField] private RepositoryLoginData _repositoryLoginData;

    [SerializeField] private RepositoryQuizQuestion _repositoryQuizQuestion;

    [SerializeField] private RepositoryLocation _repositoryLocation;
    
    [SerializeField] private RepositoryContentArea _repositoryContentArea;

    [SerializeField] private RepositoryPassingGrade _repositoryPassingGrade;
    
    [Space(10)]
    [SerializeField] private UnityEvent _onAllDataReceived;
    
    [SerializeField] private UnityEvent _onContentsDataReceived;

    private int _dataRequired;
    private int _dataReceived;
    
    private void Start()
    {
        URL = "http://"+_repositoryLoginData.API_URL;
        if (_isStart)
        {
            //Invoke("asd",0.1f);
        }
    }

    private void OnValidate()
    {
        _repositoryLoginData = Resources.Load<RepositoryLoginData>("ScriptableObjects/Repository/Repository Login Data");
        _repositoryMateri = Resources.Load<RepositoryMateri>("ScriptableObjects/Repository/Repository Materi");
        _repositoryQuizQuestion = Resources.Load<RepositoryQuizQuestion>("ScriptableObjects/Repository/Repository Exam Question");
        _repositoryLocation = Resources.Load<RepositoryLocation>("ScriptableObjects/Repository/Repository Location");
        _repositoryContentArea = Resources.Load<RepositoryContentArea>("ScriptableObjects/Repository/Repository Content Area");
        _repositoryPassingGrade = Resources.Load<RepositoryPassingGrade>("ScriptableObjects/Repository/Repository Passing Grade");
    }

    public void GetDataFromServer()
    {
        _dataReceived = 0;
        _dataRequired = 6;
        
        GetToken();
        
        // get data
        GetRepositoryMateri(URL+"/vr-bahasa/public/api/v1/materis");
        GetRepositoryLocation(URL+"/vr-bahasa/public/api/v1/chapters");
        GetRepositoryContentArea(URL+"/vr-bahasa/public/api/v1/contents");
        GetRepositoryDataQuiz(URL+"/vr-bahasa/public/api/v1/exam-questions");
        GetRepositoryPassingGrade(URL+"/vr-bahasa/public/api/v1/passgrade");
    }
    
    public void GetRepositoryMateri(string url)
    {
        //GetToken();
        SendGET(url,x=>_repositoryMateri.SetItems(x),Header.ToDictionary(x=>x.key));
    }
    
    public void GetRepositoryDataQuiz(string url)
    {
        //GetToken();
        SendGET(url,x=>_repositoryQuizQuestion.SetItems(x),Header.ToDictionary(x=>x.key));
    }
    
    public void GetRepositoryLocation(string url)
    {
        //GetToken();
        SendGET(url,x=>_repositoryLocation.SetItems(x),Header.ToDictionary(x=>x.key));
    }
    
    public void GetRepositoryContentArea(string url)
    {
        //GetToken();
        SendGET(url,x=>_repositoryContentArea.SetItems(x),Header.ToDictionary(x=>x.key));
    }
    
    public void GetRepositoryPassingGrade(string url)
    {
        //GetToken();
        SendGET(url,x=>_repositoryPassingGrade.SetItems(x),Header.ToDictionary(x=>x.key));
    }
    public void GetToken()
    {
        //_repositoryLoginData.token = currentToken;
        Header = new List<RequestHeader>(){new RequestHeader(){key="Authorization",value = $"Bearer {_repositoryLoginData.token}"}};

        if (debugMod)
        {
            Debug.Log("Header Count: " + Header.Count + " Get Token: " + Header[0].value);
        }
        
        SendGET(URL + "/vr-bahasa/public/api/v1/tokens",(x)=>
        {
            DeserializeLoginResult(x);
        },Header.ToDictionary(x=>x.key));
    }
    void DeserializeLoginResult(string www)
    {
        var result = JsonUtility.FromJson<LoginResponse>(www);
        
        _repositoryLoginData.status_code = result.status_code;
        
        _repositoryLoginData.Header.Add(Header[0]);

        if (debugMod)
        {
            Debug.Log("Get token status code : " + result.status_code);
        }
    }
    
    public void SendGET(string url, Action<string> callback, Dictionary<string,RequestHeader> requestheader)
    {
        StartCoroutine(GetContentsCoroutine( url,callback,requestheader));
    }

    private IEnumerator GetContentsCoroutine(string url, Action<string> callback,Dictionary<string,RequestHeader> requestheader)
    {
        //Debug.Log("UnityWebRequest.Get: " + url);
        
        using (UnityWebRequest www = UnityWebRequest.Get(url))
        {
            www.method="GET";
            if (requestheader != null)
            {
                foreach (var singleheader in requestheader)
                {
                    string requestName = singleheader.Key;
                    string value = singleheader.Value.value;
                    www.SetRequestHeader(requestName,value);
                    
                    //Debug.Log("Header Name: " + requestName + ", Header Value: " + value);
                }
            }
            
            yield return www.SendWebRequest();
                
            //Error handling
            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.Log("Error connecting to the server: " + www.error + ", " + $"{www.responseCode}");
                /*if(www.isHttpError)
                    Debug.Log($"{www.responseCode}");*/
            }
            else
            {
                callback.Invoke(www.downloadHandler.text);
                _dataReceived += 1;

                if (debugMod)
                {
                    Debug.Log("Data Received: " + _dataReceived);
                }

                if (url == URL + "/vr-bahasa/public/api/v1/contents")
                {
                    if (debugMod)
                    {
                        Debug.Log("Contents Loaded");
                    }

                    _onContentsDataReceived?.Invoke();
                }

                if (_dataReceived == _dataRequired)
                {
                    if (debugMod)
                    {
                        Debug.Log("All data received");
                    }
                    
                    _onAllDataReceived?.Invoke();
                }
            }
        }
    }
}



public class BypassCertificate : CertificateHandler
{
    protected override bool ValidateCertificate(byte[] certificateData)
    {
        return true;
    }
}
