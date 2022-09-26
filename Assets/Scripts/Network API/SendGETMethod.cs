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
    
    // default url
    [SerializeField] private string URL = "http://localhost/vr-bahasa/public/api/v1/tokens";
    
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
    [SerializeField] private UnityEvent _onLocationDataReceived;

    private int _dataRequired;
    private int _dataReceived;
    
    private void Awake()
    {
        GetServerURL();
    }

    void GetServerURL()
    {
        URL = "http://"+_repositoryLoginData.API_URL;
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
        GetServerURL();
        GetToken();
        
        _dataReceived = 0;
        _dataRequired = 1;
        
        // get data
        
        GetRepositoryMateri(URL+"/vr_bahasa_v2/public/api/v1/language");
        //GetRepositoryLocation(URL+"/vr-bahasa/public/api/v1/chapters");
        //GetRepositoryPassingGrade(URL+"/vr-bahasa/public/api/v1/passgrade");     -> obsolete
        //GetRepositoryDataQuiz(URL+"/vr-bahasa/public/api/v1/exam-questions");    -> move to get per NPC
    }

    // user can validate content area data from home -> settings
    public void ValidateData()
    {
        GetServerURL();
        GetToken();
        
        _dataReceived = 0;
        _dataRequired = 1;
        GetRepositoryContentArea(URL + "/vr_bahasa_v2/public/api/v1/area"); 
        //"/vr-bahasa/public/api/v1/contents");
    }

    public void GetScenarioData (string language_id, string scenario_id)
    {
        _repositoryQuizQuestion.Items.Clear();  
        
        GetServerURL();
        GetToken();
        
        _dataReceived = 0;
        _dataRequired = 1;
        
        // format: http://localhost/vr_bahasa_v2/public/api/v2/area/15/location/1/language/5
        GetRepositoryDataQuiz(URL+"/vr_bahasa_v2/public/api/v1/sentence/language/" + language_id + "/scenario?scenario[]=" + scenario_id);
    }

    public void GetLocationData(string language_id)
    {
        GetServerURL();
        GetToken();
        
        _dataReceived = 0;
        _dataRequired = 1;
        GetRepositoryLocation(URL+"/vr_bahasa_v2/public/api/v1/location/language/" + language_id);
    }

    public void GetRepositoryMateri(string url)
    {
        //GetToken();
        SendGET(url, x => _repositoryMateri.SetItems(x, DataReceived), Header.ToDictionary(x => x.key),
            () => Callback());

        void Callback()
        {
            
        }
    }
    
    public void GetRepositoryDataQuiz(string url)
    {
        //GetToken();
        SendGET(url,x=>_repositoryQuizQuestion.SetItems(x, DataReceived),Header.ToDictionary(x=>x.key),
            () => Callback());

        void Callback()
        {
            
        }
    }
    
    public void GetRepositoryLocation(string url)
    {
        //GetToken();
        SendGET(url,x=>_repositoryLocation.SetItems(x, DataReceived),Header.ToDictionary(x=>x.key),
            () => Callback());

        void Callback()
        {
            _onLocationDataReceived?.Invoke();
        }
    }
    
    public void GetRepositoryContentArea(string url)
    {
        //GetToken();
        SendGET(url,x=>_repositoryContentArea.SetItems(x, DataReceived),Header.ToDictionary(x=>x.key),
            () => Callback());

        void Callback()
        {
            _onContentsDataReceived?.Invoke();
        }
    }
    
    public void GetRepositoryPassingGrade(string url)
    {
        //GetToken();
        SendGET(url,x=>_repositoryPassingGrade.SetItems(x, DataReceived),Header.ToDictionary(x=>x.key),
            () => Callback());

        void Callback()
        {
            
        }
    }

    void DataReceived()
    {
        _dataReceived += 1;
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
        },Header.ToDictionary(x=>x.key),
            () => Callback());

        void Callback()
        {
            
        }
    }
    void DeserializeLoginResult(string www)
    {
        var result = JsonUtility.FromJson<LoginResponse>(www);
        Debug.Log(www);
        _repositoryLoginData.status_code = result.status_code;

        /*if (!_repositoryLoginData.Header.Contains(Header[0]))
        {
            _repositoryLoginData.Header.Add(Header[0]);
        }*/
        

        if (debugMod)
        {
            Debug.Log("Get token status code : " + result.status_code);
        }
    }
    
    public void SendGET(string url, Action<string> callback, Dictionary<string,RequestHeader> requestheader, Action callback2)
    {
        StartCoroutine(GetContentsCoroutine( url,callback,requestheader, callback2));
    }

    private IEnumerator GetContentsCoroutine(string url, Action<string> callback,Dictionary<string,RequestHeader> requestheader, Action callback2)
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
                
                Debug.Log("Error API: " + url);
            }
            else
            {
                callback.Invoke(www.downloadHandler.text);
                //_dataReceived += 1;

                if (debugMod)
                {
                    Debug.Log("Data Received: " + _dataReceived);
                }

                /*if (url == URL + "/vr_bahasa_v2/public/api/v1/area")
                {
                    if (debugMod)
                    {
                        Debug.Log("Contents Loaded");
                    }

                    _onContentsDataReceived?.Invoke();
                }*/

                if (_dataReceived == _dataRequired)
                {
                    if (debugMod)
                    {
                        Debug.Log("All data received");
                    }
                    
                    _onAllDataReceived?.Invoke();
                }
                
                callback2?.Invoke();
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
