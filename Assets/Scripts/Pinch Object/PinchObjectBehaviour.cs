using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class PinchObjectBehaviour : MonoBehaviour
{
    [Tooltip("The distance value that object will come in front of camera")]
    [SerializeField] private Transform _pinchPivot;

    [SerializeField] private GraspBehaviour _graspBehaviour;

    [Tooltip("The speed that object will move, e.g : 0 faster than 10")] [SerializeField]
    private float _speed = 2f;

    [Tooltip("Object will move in different way")] [SerializeField]
    private Ease _currentEase;
    
    [SerializeField] private ScriptableListTransform _scriptableListTransform;

    [SerializeField] private UnityEvent OnBeginPinching;

    [Tooltip("Because the target object is not have Position Object Manager Script")] [SerializeField]
    private UnityEvent OnCannotPinching;

    [SerializeField] private UnityEvent OnCompletePinching;
    
    //[SerializeField] private PinchObject _pinchObject;

    //private Vector3 _myTarget;

    private Transform _savedPinch { set; get; }

    /*private void Awake()
    {
        _pinchObject = new PinchObject(graspBehaviour,_cameraTransform, _distance, _speed, OnBeginPinching,OnCannotPinching,OnCompletePinching, _currentEase);
    }

    public void BeginPinching()
    {
        BeginPinching();
    }*/

    public void BeginPinching()
    {
        //_myTarget = new Vector3(_pinchPivot.position.x, _pinchPivot.localPosition.y, _pinchPivot.localPosition.z + _ObjdistanceFromCam);

      if (_pinchPivot)
        {
            if (_graspBehaviour._myTarget)
            {
                var initGrab = _graspBehaviour._myTarget.GetComponent<InitializeGrab>();
            
                if (initGrab)
                {
                    if (initGrab._canGrab)
                    {
                        _savedPinch = _graspBehaviour._myTarget;

                        if (_scriptableListTransform.MyTransforms.Contains(_graspBehaviour._myTarget))
                        {
                            OnBeginPinching.Invoke();

                            TeleportObject();
                        }
                        else
                        {
                            OnCannotPinching.Invoke();
                        }
                    }
                    else
                    {
                        OnCannotPinching.Invoke();
                    }
                }
            }
        }
    }

    private void TeleportObject()
    {
        _graspBehaviour._myTarget.GetComponent<Collider>().enabled = false;

        var position = _pinchPivot.position;
        
        _graspBehaviour._myTarget.DOMove(new Vector3(
                position.x, 
                position.y,
                position.z),
            _speed).SetEase(_currentEase).OnComplete(() => OnCompletePinch());
    }

    void OnCompletePinch()
    {
        _savedPinch.GetComponent<Collider>().enabled = true;

        OnCompletePinching.Invoke();
    }
}