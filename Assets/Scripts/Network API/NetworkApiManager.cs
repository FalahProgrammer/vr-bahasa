 using System;
using System.Collections;
 using System.Collections.Generic;
 using Leap;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
//using UniRx;

public class NetworkApiManager : MonoBehaviour
{
    /*public RepositoryUser repositoryUser;
    public RepositoryChapter repositoryChapter;
    public RepositoryContent repositoryContent;
    public RepositoryQuizLevel repositoryQuizLevel;
    public RepositoryQuizQuestion repositoryQuizQestion;*/
    

    
    [SerializeField] private RepositoryMateri _repositoryMateri;

    [SerializeField] private RepositoryQuizQuestion _repositoryQuizQuestion;

    [SerializeField] private RepositoryLocation _repositoryLocation;
    
    [SerializeField] private RepositoryContentArea _repositoryContentArea;
    
    //[SerializeField] private RepositoryContent _repositoryContent;

    
    private void OnValidate()
    {
        _repositoryMateri = Resources.Load<RepositoryMateri>("ScriptableObjects/Repository/Repository Materi");
        
        _repositoryQuizQuestion = Resources.Load<RepositoryQuizQuestion>("ScriptableObjects/Repository/Repository Quiz Question");
        
        _repositoryLocation = Resources.Load<RepositoryLocation>("ScriptableObjects/Repository/Repository Location");
        
        _repositoryContentArea = Resources.Load<RepositoryContentArea>("ScriptableObjects/Repository/Repository Content Area");
        
        //_repositoryContent = Resources.Load<RepositoryContent>("ScriptableObjects/Repository/Repository Content");
    }

    private void Start()
    {
        //GetRepositoryDataItems("http://" + "localhost" + "/unhan/public/raftels/quiz_question");
        
        //GetRepositoryDataContent("http://" + "localhost" + "/penembak/public/modul");
        
        GetRepositoryMateri("http://vr-bahasa.test/api/v1/materis");

        GetRepositoryLocation("http://vr-bahasa.test/api/v1/chapters");
        
        GetRepositoryContentArea("http://vr-bahasa.test/api/v1/contents");
        
        GetRepositoryDataQuiz("http://vr-bahasa.test/api/v1/quiz-questions");
    }
    
    public void GetRepositoryMateri(string url)
    {
        StartCoroutine(GetRepositoryData(url, _repositoryMateri.SetItems));
    }

    public void GetRepositoryDataQuiz(string url)
    {
        StartCoroutine(GetRepositoryData(url, _repositoryQuizQuestion.SetItems));
    }
    
    public void GetRepositoryLocation(string url)
    {
        StartCoroutine(GetRepositoryData(url, _repositoryLocation.SetItems));
    }
    
    public void GetRepositoryContentArea(string url)
    {
        StartCoroutine(GetRepositoryData(url, _repositoryContentArea.SetItems));
    }
    
    public void GetRepositoryDataContent(string url)
    {
        //StartCoroutine(GetRepositoryData(url, _repositoryContent.SetItems));
    }
    
    /*public void GetRepositoryDataUser(string url)
    {
        StartCoroutine(GetRepositoryData(url, repositoryUser.SetItems));
    }

    public void GetRepositoryDataChapter(string url)
    {
        StartCoroutine(GetRepositoryData(url, repositoryChapter.SetItems));
    }
    
    public void GetRepositoryDataContent(string url)
    {
        StartCoroutine(GetRepositoryData(url, repositoryContent.SetItems));
    }
    
    public void GetRepositoryDataQuizLevel(string url)
    {
        StartCoroutine(GetRepositoryData(url, repositoryQuizLevel.SetItems));
    }

    public void GetRepositoryDataQuizQuestion(string url)
    {
        StartCoroutine(GetRepositoryData(url, repositoryQuizQestion.SetItems));
    }*/
    
    
    IEnumerator GetRepositoryData(string url, Action<string> SetListData)
    {
        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();
        //sebaiknya pakai while true
        if (www.isNetworkError || www.isHttpError) {
            Debug.LogWarning("=Error GetRepositoryData = " + www.error);
            Debug.LogWarning("=Error GetRepositoryData www.isNetworkError = " + www.isNetworkError);
            Debug.LogWarning("=Error GetRepositoryData www.isHttpError = " + www.isHttpError);
            
            StartCoroutine(GetRepositoryData(url, SetListData));
        }
        else {
            Debug.LogWarning("GetRepositoryData www.downloadHandler.text = " + www.downloadHandler.text);

            SetListData(www.downloadHandler.text);
        }

        www.Dispose();

    }

    public void GetAllRepositoryData(string serverAdress, string usernameLogin, Action Response)
    {
        Debug.Log("GetAllRepositoryData Begin serverAdress = " + serverAdress);

        /*var parallel = Observable.WhenAll(
            ObservableWWW.Get("http://" + serverAdress + "/unhan/public/raftels/quiz_question"));
            //ObservableWWW.Get("http://" + serverAdress + "/penembak/public/modul"));
            /*ObservableWWW.Get("http://" + serverAdress +
                              "/penembak/public/getuserdata?username=" +
                              usernameLogin),
            ObservableWWW.Get("http://" + serverAdress + "/penembak/public/bagian"),
            ObservableWWW.Get("http://" + serverAdress + "/penembak/public/modul"),
            ObservableWWW.Get("http://" + serverAdress + "/penembak/public/quizlevel"),
            ObservableWWW.Get("http://" + serverAdress + "/penembak/public/quiz"));#1#
        
        parallel.Subscribe(rawJsonData =>
        {
            Debug.Log("xs.Length = " + rawJsonData.Length);
            for (int i = 0; i < rawJsonData.Length; i++)
            {
                Debug.Log("xs[" + i + "] = " + rawJsonData[i]);
            }
            _repositoryQuizQuestion.SetItems(rawJsonData[0]);
            //_repositoryContent.SetItems(rawJsonData[1]);
            /*repositoryChapter.SetItems(rawJsonData[1]);
            repositoryContent.SetItems(rawJsonData[2]);
            repositoryQuizLevel.SetItems(rawJsonData[3]);
            repositoryQuizQestion.SetItems(rawJsonData[4]);#1#

            
        }, (e) => Debug.LogWarning("Error e = " + e), Response);
        
        
        
        Debug.Log("GetAllRepositoryData Done");*/
    }
    
    
    
        
}
