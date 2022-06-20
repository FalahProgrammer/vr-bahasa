using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class ResetScene : MonoBehaviour
{
    public UnityEvent AfterClickSpace;
    
    private void Update()
    {
        if (Input.GetKey(KeyCode.R))
        {
            AfterClickSpace.Invoke();
        }
    }

    public void Changescene()
    {
       StartCoroutine("InvokeChangeScene",1f);
    }

    IEnumerator InvokeChangeScene()
    {
        SceneManager.LoadScene("Sequence Menu Test");
        yield return null;
    }
}
