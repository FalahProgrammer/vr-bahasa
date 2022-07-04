using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;
using Valve.VR;

public class RecenterHTC5 : MonoBehaviour
{
    /*public Text Debugging;
    public Transform Transform;
    
    public Text DebuggingMainCam;
    public Transform TransformMainCam;*/
    private void Awake()
    {
        RecenterCamera();
    }

    void Update()
    {
        /*Debugging.text = Transform.localPosition.ToString();
        
        DebuggingMainCam.text = TransformMainCam.localPosition.ToString();*/
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            RecenterCamera();
        }
    }

    private void RecenterCamera()
    {
        InputTracking.Recenter();
    }
}