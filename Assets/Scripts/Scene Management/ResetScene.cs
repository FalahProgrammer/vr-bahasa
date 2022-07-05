using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class ResetScene : MonoBehaviour
{
    [SerializeField] private SceneLoaderManager _sceneLoaderManager;
    
    public UnityEvent AfterClickSpace;
    
    private void Update()
    {
        if (Input.GetKey(KeyCode.R))
        {
            AfterClickSpace.Invoke();
            
            _sceneLoaderManager.SetScene(SceneManager.GetActiveScene().name);
        
            _sceneLoaderManager.ChangeScene();
        }
    }
}
