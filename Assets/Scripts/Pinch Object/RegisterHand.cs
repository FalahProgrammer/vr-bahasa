using System;
using System.Collections;
using System.Collections.Generic;
using Leap.Unity;
using UnityEngine;

public class RegisterHand : MonoBehaviour
{
    [SerializeField] private HandModelBase _lefthand;
    
    [SerializeField] private HandModelBase _righthand;

    [SerializeField] private PinchDetector _leftPinchDetector;
    
    [SerializeField] private PinchDetector _rightPinchDetector;

    private void Start()
    {
        
    }

    public void RegisterRightHand()
    {
        _rightPinchDetector.HandModel = _righthand;
    }
    
    public void UnRegisterRightHand()
    {
        _rightPinchDetector.HandModel = null;
    }
    
    
    public void RegisterLeftHand()
    {
        _leftPinchDetector.HandModel = _lefthand;
    }
    
    public void UnRegisterLeftHand()
    {
        _leftPinchDetector.HandModel = null;
    }
}
