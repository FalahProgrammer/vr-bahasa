using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class ResetableObject : iResetable
{
    private UnityEvent _onBeginReset { set;get; }
    private UnityEvent _onCompleteReset { set;get; }
    private Ease _selectEase { set;get; }
    private ResetObjectBehaviour _resetObjectBehaviour { set;get; }
    private float _speed { set;get; }

    private int i;

    private Vector3 _myTarget;
    public ResetableObject(ResetObjectBehaviour resetObjectBehaviour, float speed, Ease selectEase, UnityEvent onBeginReset, UnityEvent onCompleteReset)
    {
        _onBeginReset = onBeginReset;
        
        _onCompleteReset = onCompleteReset;
        
        _selectEase = selectEase;
        
        _resetObjectBehaviour = resetObjectBehaviour;
        
        _speed = speed;
    }

    private void Init()
    {
        
    }
    public void Reset()
    {
        /*for (i = 0; i < _resetObjectBehaviour._scriptableListTransform.MyTransforms.Count; i++)
        {
            _myTarget = _resetObjectBehaviour._scriptableListTransform.MyTransforms[i].GetComponent<IntialPosRotBehaviour>().GetPos;
            
            DoLocalMove();
            
            DoLocalRotateQuaternion();
        }*/
    }

    public void DoMove()
    {
        throw new NotImplementedException();
    }

    public void DoLocalMove()
    {
        //_resetObjectBehaviour._scriptableListTransform.MyTransforms[i].DOLocalMove(_myTarget, _speed).SetEase(_selectEase);
    }

    public void DoSelectLocalMove(float myTarget)
    {
        throw new NotImplementedException();
    }

    public void DoSelectMove(float myTarget)
    {
        throw new NotImplementedException();
    }

    public void DoLocalRotateQuaternion()
    {
        /*_resetObjectBehaviour._scriptableListTransform.MyTransforms[i].DOLocalRotateQuaternion
        (_resetObjectBehaviour._scriptableListTransform.MyTransforms[i].GetComponent<IntialPosRotBehaviour>().getRot,
            _speed).SetEase(_selectEase);*/
    }

    public void DoFade(float endValue)
    {
        throw new NotImplementedException();
    }

    public void DoLocalRotate()
    {
        throw new NotImplementedException();
    }

    void OnBeginReset()
    {
        _onBeginReset.Invoke();
    }

    void OnCompleteReset()
    {
        _onCompleteReset.Invoke();
    }
}
