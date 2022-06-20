using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;


public class PinchObject
{
    private Transform _cameraTransform { get; }
    private UnityEvent _onCannotPinching { get; }
    private UnityEvent _onBeginPinching { get; }
    private UnityEvent _onCompletePinching { get; }
    private float _distance { get; }
    private float _speed { get; }
    private Ease _selectEase { get; }
    private GraspBehaviour GraspBehaviour { get; }
    private Transform _savedPinch { set;get; }

    private Vector3 _myTarget;
    public PinchObject(GraspBehaviour graspBehaviour, Transform cameraTransform, float _distance, float _speed,
        UnityEvent onBeginPinching,UnityEvent onCannotPinching, UnityEvent onCompletePinching, Ease selectEase)
    {
        _onCannotPinching = onCannotPinching;
        
        _cameraTransform = cameraTransform;
        
        _onBeginPinching = onBeginPinching;

        _onCompletePinching = onCompletePinching;

        this._distance = _distance;

        this._speed = _speed;

        _selectEase = selectEase;

        GraspBehaviour = graspBehaviour;
    }

    public void BeginPinching()
    {
        _myTarget = new Vector3(_cameraTransform.position.x, _cameraTransform.localPosition.y, _cameraTransform.localPosition.z + _distance);
        
        if (GraspBehaviour._myTarget != null)
        {
            if (GraspBehaviour._myTarget.GetComponent<InitializeGrab>())
            {
                _savedPinch = GraspBehaviour._myTarget;
                
                GraspBehaviour._myTarget.GetComponent<Collider>().enabled = false;
                
                _onBeginPinching.Invoke();

                DoLocalMove();
            }
            else
            {
                _onCannotPinching.Invoke();
            }
        }
    }

    void OnCompletePinching()
    {
        _savedPinch.GetComponent<Collider>().enabled = true;
        
        _onCompletePinching.Invoke();
    }

    public void DoMove()
    {
        throw new NotImplementedException();
    }

    public void DoLocalMove()
    {
        GraspBehaviour._myTarget.DOLocalMove(_myTarget,
            _speed).SetEase(_selectEase).OnComplete(() => OnCompletePinching());
    }

    public void DoSelectLocalMove(float myTarget)
    {
        throw new NotImplementedException();
    }

    public void DoSelectMove(float myTarget)
    {
        throw new NotImplementedException();
    }

    public void DoLocalRotate()
    {
        
    }

    public void DoLocalRotateQuaternion()
    {
        
    }

    public void DoFade(float endValue)
    {
        throw new NotImplementedException();
    }
}