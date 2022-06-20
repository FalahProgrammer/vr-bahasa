using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader
{
    private TextMeshProUGUI _loadingValue;
    
    private Slider _loadingSlider;
    
    private UnityEvent _onFinishedLoadScene;

    public string _sceneName;
    
    private const float scene_max_progress = 0.9f;

    public  SceneLoader (string sceneName,TextMeshProUGUI loadingValue, Slider loadingSlider, UnityEvent onFinishedLoadScene)
   {
       _sceneName = sceneName;
       
       _loadingValue = loadingValue;
       
       _loadingSlider = loadingSlider;
       
       _onFinishedLoadScene = onFinishedLoadScene;
   }

   

   public void ResetLoading()
   {
       _loadingValue.text = "0 %";
   }
   
   public IEnumerator LoadScene(Action onFinishedLoadScene)
   {
       AsyncOperation asyncHomeLoad = SceneManager.LoadSceneAsync(_sceneName , LoadSceneMode.Single);
       
       while (!asyncHomeLoad.isDone)
       {
           float progress = Mathf.Clamp01(asyncHomeLoad.progress / scene_max_progress);

           _loadingSlider.value = progress;
           
           float textprogress = progress * 100f;
           
           if (progress <= scene_max_progress*100.0f)
           {
               _loadingValue.text = textprogress.ToString("F0") + " %";
           }
           
           yield return null;
           
           onFinishedLoadScene.Invoke();
           
       }
       
       DOTween.Clear(true);
   }

   public void OnFinishedLoadScene()
   {
       _onFinishedLoadScene.Invoke();
   }
}
