using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class DoMoveBehaviour : MonoBehaviour
{
    [HideInInspector]
    public Transform MyTarget;
    
    [HideInInspector]
    public Transform TargetLocation;
    
    //[SerializeField] private IntialPosRotBehaviour intialPosRotBehaviour;
    [HideInInspector]
    [Tooltip("Select your move type in ease mode")]
    public Ease _selectEase;
    
    [HideInInspector]
    [Tooltip("Select your move direction")]
    public MyMode Mode;
    
    [HideInInspector]
    [Tooltip("Adjust your move speed / duration, e.g : 1 is faster than 10")]
    public float _speed;
    
    [HideInInspector]
    [Tooltip("Start function with delayed")]
    public float DelayedTime = 2;
    
    [HideInInspector]
    [Tooltip("Set your target (X/Y/Z) value")]
    public float _targetValue;
    
    [HideInInspector]
    public UnityEvent OnCompleteMove;
    
    [HideInInspector]
    [Tooltip("Start function with delayed")]
    public bool BoolDelayTime;
    public enum MyMode
    {
        X,
        Y,
        Z
    }
    public enum Type
    {
        Unset,
        SpesificTransform,
        XYZMode
    }
    [HideInInspector]
    public Type SelectType;

    private DoMoveObject _doMoveObject;
    
    [HideInInspector]
    public Texture2D Logo = null;

    private void OnValidate()
    {
        if (MyTarget.GetComponent<InitialPosRotBehaviour>())
        {
           return;
        }
        
        MyTarget.gameObject.AddComponent<InitialPosRotBehaviour>();
    }

    private void Awake()
    {
        if (MyTarget.GetComponent<InitialPosRotBehaviour>() != null)
        {
            _doMoveObject = new DoMoveObject(this,MyTarget, TargetLocation,_selectEase,OnCompleteMove,_targetValue,_speed, MyTarget.GetComponent<InitialPosRotBehaviour>());
        }
        else
        {
            Debug.LogWarning("You need to attach ''InitialPosRotBehaviour'' to your Target transform !");
        }
        
        
        
    }

    public void SetTargetLocation(Transform transform)
    {
        TargetLocation = transform;
        
        _doMoveObject = new DoMoveObject(this,MyTarget, TargetLocation,_selectEase,OnCompleteMove,_targetValue,_speed, MyTarget.GetComponent<InitialPosRotBehaviour>());
    }
    /// <summary>
    /// Do Move
    /// </summary>
    public virtual void BeginDoMove()
    {
        if (BoolDelayTime)
        {
            StartCoroutine(CoroutineDoMove());
        }
        else
        {
            _doMoveObject.DoMove();
        }
    }
    
    /// <summary>
    /// Do Local Move
    /// </summary>
    public virtual void BeginDoLocalMove()
    {
        //perlu di benerin scriptnya, buat if jika make delayed start atau tidak di method local dan move biasa (DONE !!!)
        if (BoolDelayTime)
        {
            StartCoroutine(CoroutineDoLocalMove());
        }
        else
        {
            _doMoveObject.DoLocalMove();
        }
    }
    
    /// <summary>
    /// Do Select Move
    /// </summary>
    public virtual void BeginDoSelectMove()
    {
        if (BoolDelayTime)
        {
            StartCoroutine(CoroutineDoSelectMove());
        }
        else
        {
            _doMoveObject.BeginDoSelectMove();
        }
    }
    
    /// <summary>
    /// Do Select Local Move
    /// </summary>
    public virtual void BeginDoSelectLocalMove()
    {
        if (BoolDelayTime)
        {
            StartCoroutine(CoroutineDoSelectLocalMove());
        }
        else
        {
            _doMoveObject.BeginDoSelectLocalMove();
        }
    }
    
    
    

    /// <summary>
    /// Do Move Initial Position
    /// </summary>
    public virtual void BeginDoMoveInitialPosition()
    {
        _doMoveObject.DoMoveBacktoInitPosition(MyTarget.GetComponent<InitialPosRotBehaviour>().GetPos);
    }
    
    /// <summary>
    /// Do Local Move Initial Position
    /// </summary>
    public virtual void BeginDoLocalMoveInitialPosition()
    {
        _doMoveObject.DoLocalMoveBacktoInitPosition(MyTarget.GetComponent<InitialPosRotBehaviour>().GetLocalPos);
    }
    
    /// <summary>
    /// On Complete Moving
    /// </summary>
    public void OnCompleteMoving()
    {
        OnCompleteMove.Invoke();
    }

    
    IEnumerator CoroutineDoMove()
    {
        yield return new WaitForSeconds(DelayedTime);
        
        _doMoveObject.DoMove();
    }
    
    IEnumerator CoroutineDoSelectMove()
    {
        yield return new WaitForSeconds(DelayedTime);
        
        _doMoveObject.BeginDoSelectMove();
    }
    
    IEnumerator CoroutineDoLocalMove()
    {
        yield return new WaitForSeconds(DelayedTime);
        
        _doMoveObject.DoLocalMove();
    }
    
    IEnumerator CoroutineDoSelectLocalMove()
    {
        yield return new WaitForSeconds(DelayedTime);
        
        _doMoveObject.BeginDoSelectLocalMove();
    }
    
}
