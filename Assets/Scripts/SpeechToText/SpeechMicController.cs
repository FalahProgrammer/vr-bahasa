using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeechMicController : MonoBehaviour
{
    [SerializeField] private AudioRecorder _audioRecorder;
    public GameObject GameObjectLanguage;


    public void SetAudioRecorder(AudioRecorder rec)
    {
        if(rec==null)
            Debug.LogWarning("Can't find AudioRecorder");
        _audioRecorder = rec;
    }
    
    public void UnMuteMic()
    {
        //_audioRecorder.MicrophoneIndex = 0;
        //GameObjectLanguage.SetActive(true);
    }

    public void MuteMic()
    {
        //_audioRecorder.MicrophoneIndex = 1;
        //GameObjectLanguage.SetActive(false);
    }

    /*private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            UnMuteMic();
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            MuteMic();

        }
    }*/
}
