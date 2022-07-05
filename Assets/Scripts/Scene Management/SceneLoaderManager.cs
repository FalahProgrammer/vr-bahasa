using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoaderManager : MonoBehaviour
{
    private SceneLoader _sceneLoader;
    
    private string SceneName;
    
    public TextMeshProUGUI LoadingValue;

    public Slider _loadingSlider;

    public RepositoryLoginData RepositoryLoginData;

    public UnityEvent OnFinishedLoadScene;
    void Awake()
    {
        _sceneLoader = new SceneLoader(SceneName,LoadingValue,_loadingSlider,OnFinishedLoadScene);
    }

    public void SetScene(string sceneName)
    {
        SceneName = sceneName;
    }

    public void ChangeScene()
    {
        _sceneLoader._sceneName = SceneName;
        
        StartCoroutine(_sceneLoader.LoadScene(_sceneLoader.OnFinishedLoadScene));
    }
    

    public void ResetLoading()
    {
        _sceneLoader.ResetLoading();
    }
}
