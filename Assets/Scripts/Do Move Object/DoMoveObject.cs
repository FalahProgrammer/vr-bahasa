using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class DoMoveObject 
{
    private Transform _targetLocation { set; get; }
    private DoMoveBehaviour _doMoveBehaviour { set; get; }
    private Transform _myTransform { set; get; }
    private Ease _selectEase { set; get; }
    private UnityEvent _onCompleteMove { set; get; }
    private float _targetValue { set; get; }
    private float _speed { set; get; }
    private InitialPosRotBehaviour InitialPosRotBehaviour { set; get; }

    public DoMoveObject(DoMoveBehaviour doMoveBehaviour, Transform myTransform,Transform targetLocation, Ease selectEase, 
        UnityEvent onCompleteMove, float targetValue, float speed, InitialPosRotBehaviour initialPosRotBehaviour)
    {
        _targetLocation = targetLocation;
        
        _doMoveBehaviour = doMoveBehaviour;
        
        _myTransform = myTransform;
        
        _selectEase = selectEase;
        
        _onCompleteMove = onCompleteMove;
        
        _targetValue = targetValue;
        
        _speed = speed;
        
        InitialPosRotBehaviour = initialPosRotBehaviour;
    }

    public void BeginDoSelectMove()
    {
        _doMoveBehaviour.StopAllCoroutines();

        DoSelectMove(_targetValue);
    }
    
    public void BeginDoSelectLocalMove()
    {
        _doMoveBehaviour.StopAllCoroutines();

        DoSelectLocalMove(_targetValue);
    }

    /*public void BeginDoMove()
    {
        _doMoveBehaviour.StartCoroutine(CoroutineBeginDoMove());
    }

    IEnumerator CoroutineBeginDoMove()
    {
        yield return new WaitForSeconds(2);
        
        DoMove();
    }
    
    public void BeginDoLocalMove()
    {
        _doMoveBehaviour.StartCoroutine(CoroutineBeginDoLocalMove());
    }

    IEnumerator CoroutineBeginDoLocalMove()
    {
        yield return new WaitForSeconds(2);
        
        DoLocalMove();
    }*/

    /*---------------------------------------------------------------------------------------------------------------------------------------------*/
    
    public void DoMoveBacktoInitPosition(Vector3 _myTarget)
    {
        _doMoveBehaviour.StartCoroutine(CoroutineDoMoveBacktoInitPosition(_myTarget));
    }
    
    IEnumerator CoroutineDoMoveBacktoInitPosition(Vector3 _myTarget)
    {
        yield return new WaitForSeconds(2);
        
        _myTransform.DOMove(_myTarget, _speed)
            .SetEase(_selectEase).SetId("DoMove").OnComplete(_doMoveBehaviour.OnCompleteMoving);
    }
    
    public void DoLocalMoveBacktoInitPosition(Vector3 _myTarget)
    {
        _doMoveBehaviour.StartCoroutine(CoroutineDoLocalMoveBacktoInitPosition(_myTarget));
    }
    
    IEnumerator CoroutineDoLocalMoveBacktoInitPosition(Vector3 _myTarget)
    {
        yield return new WaitForSeconds(2);
        
        _myTransform.DOLocalMove(_myTarget, _speed)
            .SetEase(_selectEase).SetId("DoLocalMove").OnComplete(_doMoveBehaviour.OnCompleteMoving);
    }
    public void DoMove()
    {
        _myTransform.DOMove(_targetLocation.position, _speed)
            .SetEase(_selectEase).SetId("DoMove");
        
        _myTransform.DORotateQuaternion(_targetLocation.rotation, _speed)
            .SetEase(_selectEase).SetId("DoMove").OnComplete(_doMoveBehaviour.OnCompleteMoving);
    }

    public void DoLocalMove()
    {
        _myTransform.DOLocalMove(_targetLocation.localPosition, _speed)
            .SetEase(_selectEase).SetId("DoLocalMove");
        
        _myTransform.DOLocalRotateQuaternion(_targetLocation.rotation, _speed)
            .SetEase(_selectEase).SetId("DoMove").OnComplete(_doMoveBehaviour.OnCompleteMoving);
    }

    public void DoSelectLocalMove(float myTarget)
         {
             switch (_doMoveBehaviour.Mode)
             {
                 case DoMoveBehaviour.MyMode.X:
                     _myTransform.DOLocalMoveX(myTarget, _speed)
                         .SetEase(_selectEase).SetId("DoSelectLocalMove").OnComplete(_doMoveBehaviour.OnCompleteMoving);
                     break;
                 case DoMoveBehaviour.MyMode.Y:
                     _myTransform.DOLocalMoveY(myTarget, _speed)
                         .SetEase(_selectEase).SetId("DoSelectLocalMove").OnComplete(_doMoveBehaviour.OnCompleteMoving);
                     break;
                 case DoMoveBehaviour.MyMode.Z:
                     _myTransform.DOLocalMoveZ(myTarget, _speed)
                         .SetEase(_selectEase).SetId("DoSelectLocalMove").OnComplete(_doMoveBehaviour.OnCompleteMoving);
                     break;
             }
         }

    public void DoSelectMove(float myTarget)
    {
        switch (_doMoveBehaviour.Mode)
        {
            case DoMoveBehaviour.MyMode.X:
                _myTransform.DOMoveX(myTarget, _speed)
                    .SetEase(_selectEase).SetId("DoSelectMove").OnComplete(_doMoveBehaviour.OnCompleteMoving);
                break;
            case DoMoveBehaviour.MyMode.Y:
                _myTransform.DOMoveY(myTarget, _speed)
                    .SetEase(_selectEase).SetId("DoSelectMove").OnComplete(_doMoveBehaviour.OnCompleteMoving);
                break;
            case DoMoveBehaviour.MyMode.Z:
                _myTransform.DOMoveZ(myTarget, _speed)
                    .SetEase(_selectEase).SetId("DoSelectMove").OnComplete(_doMoveBehaviour.OnCompleteMoving);
                break;
        }
    }
}
