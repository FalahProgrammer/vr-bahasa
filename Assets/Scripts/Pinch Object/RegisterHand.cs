using System;
using System.Collections;
using System.Collections.Generic;
using Leap.Unity;
using UnityEngine;

public class RegisterHand : MonoBehaviour
{
/*    [SerializeField] private HandModelBase _lefthand;
    
    [SerializeField] private HandModelBase _righthand;*/

    [SerializeField] private PinchDetector _leftPinchDetector;
    
    [SerializeField] private PinchDetector _rightPinchDetector;

    private void Start()
    {
        
    }

    public void RegisterRightHand()
    {
        _rightPinchDetector.enabled = true;
    }

    public void UnRegisterRightHand()
    {
        _rightPinchDetector.enabled = false;
    }
    
    
    public void RegisterLeftHand()
    {
        _leftPinchDetector.enabled = true;
    }
    
    public void UnRegisterLeftHand()
    {
        _leftPinchDetector.enabled = false;
    }
}
