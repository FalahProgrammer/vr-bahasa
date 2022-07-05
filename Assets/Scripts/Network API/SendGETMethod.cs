using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

public class SendGETMethod : MonoBehaviour
{
    [SerializeField] private bool _isStart;
    
    private string URL = "http://192.168.100.78/vr-bahasa/public/api/v1/tokens";
    
    public List<RequestHeader> Header = new List<RequestHeader>();
    
    [SerializeField] private RepositoryMateri _repositoryMateri;
    
    [SerializeField] private RepositoryLoginData _repositoryLoginData;

    [SerializeField] private RepositoryQuizQuestion _repositoryQuizQuestion;

    [SerializeField] private RepositoryLocation _repositoryLocation;
    
    [SerializeField] private RepositoryContentArea _repositoryContentArea;

    [SerializeField] private RepositoryPassingGrade _repositoryPassingGrade;

    
    
    private void Start()
    {
        URL = "http://"+_repositoryLoginData.API_URL;
        if (_isStart)
        {
            Invoke("asd",0.1f);
        }
        
    }
    
    private void OnValidate()
    {
        _repositoryLoginData = Resources.Load<RepositoryLoginData>("ScriptableObjects/Repository/Repository Login Data");
        
        _repositoryMateri = Resources.Load<RepositoryMateri>("ScriptableObjects/Repository/Repository Materi");
        
        _repositoryQuizQuestion = Resources.Load<RepositoryQuizQuestion>("ScriptableObjects/Repository/Repository Quiz Question");
        
        _repositoryLocation = Resources.Load<RepositoryLocation>("ScriptableObjects/Repository/Repository Location");
        
        _repositoryContentArea = Resources.Load<RepositoryContentArea>("ScriptableObjects/Repository/Repository Content Area");
        
        _repositoryPassingGrade = Resources.Load<RepositoryPassingGrade>("ScriptableObjects/Repository/Repository Passing Grade");
    }

    void asd()
    {
        GetToken();
        
        GetRepositoryMateri(URL+"/vr-bahasa/public/api/v1/materis");

        GetRepositoryLocation(URL+"/vr-bahasa/public/api/v1/chapters");
        
        GetRepositoryContentArea(URL+"/vr-bahasa/public/api/v1/contents");
        
        GetRepositoryDataQuiz(URL+"/vr-bahasa/public/api/v1/quiz-questions");

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
        
        Debug.Log("Get token status code : " + result.status_code);
    }
    
    public void SendGET(string url, Action<string> callback, Dictionary<string,RequestHeader> requestheader)
    {
        StartCoroutine(GetContentsCoroutine( url,callback,requestheader));
    }



    private IEnumerator GetContentsCoroutine(string url, Action<string> callback,Dictionary<string,RequestHeader> requestheader)
    {
        using (UnityWebRequest www = UnityWebRequest.Get(url))
        {
            www.method="GET";
            if (requestheader != null)
            {
                foreach (var singleheader in requestheader)
                {
                    www.SetRequestHeader(singleheader.Key,singleheader.Value.value);
                }
            }
            
            yield return www.SendWebRequest();
                
            //Error handling
            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log("Error connecting to the server");
                if(www.isHttpError)
                    Debug.Log($"{www.responseCode}");
            }
            else
            {
                callback.Invoke(www.downloadHandler.text);
                
            }
        }
    }
}
