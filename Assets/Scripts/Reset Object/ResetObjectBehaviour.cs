using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class ResetObjectBehaviour : MonoBehaviour, iResetable
{
    public float _speed;

    [Tooltip("Object will move in different way")] [SerializeField]
    private Ease _currentEase;

    [SerializeField] private ScriptableListTransform _scriptableListTransform;

    [SerializeField] private UnityEvent OnBeginReset;

    [SerializeField] private UnityEvent OnCompleteReset;
    private int i;

    private Vector3 _myTarget;
    //private ResetableObject _resetableObject;

    private void Awake()
    {
        //_resetableObject = new ResetableObject(this, _speed, _currentEase, OnBeginReset, OnCompleteReset);
    }

    /*public void BeginReset()
    {
        _resetableObject.Reset();
    }*/

    public void ClearObjectReset()
    {
        _scriptableListTransform.MyTransforms.Clear();
    }

    public void Reset()
    {
        OnBeginReset.Invoke();

        for (i = 0; i < _scriptableListTransform.MyTransforms.Count; i++)
        {
            if (_scriptableListTransform.MyTransforms[i].GetComponent<InitialPosRotBehaviour>())
            {
                _myTarget = _scriptableListTransform.MyTransforms[i].GetComponent<InitialPosRotBehaviour>().GetPos;

                DoLocalMove();

                DoLocalRotateQuaternion();
            }
        }
    }

    public void DoLocalMove()
    {
        _scriptableListTransform.MyTransforms[i].DOMove(_myTarget, _speed).SetEase(_currentEase).OnComplete(OnResetComplete);
        
        // dimaz's revision, if do local move, object will not reset correctly
        // _scriptableListTransform.MyTransforms[i].DOLocalMove(_myTarget, _speed).SetEase(_currentEase).OnComplete(OnResetComplete);
    }

    public void DoLocalRotateQuaternion()
    {
        _scriptableListTransform.MyTransforms[i].DOLocalRotateQuaternion
        (_scriptableListTransform.MyTransforms[i].GetComponent<InitialPosRotBehaviour>().GetRot,
            _speed).SetEase(_currentEase);
    }

    void OnResetComplete()
    {
        OnCompleteReset.Invoke();
    }
}