using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class DoScaleBehaviour : MonoBehaviour
{
    [HideInInspector]
    public Transform MyTransform;

    [HideInInspector]
    public float Speed = 1;

    [HideInInspector]
    public float ScaleValue = 2;
    
    [HideInInspector]
    public float InitValue = 1;

    [HideInInspector]
    public DoScaleObject.MyMode MyMode;

    [HideInInspector]
    [Tooltip("Select your move type in ease mode")]
    public Ease SelectEase;
    
    [HideInInspector]
    public UnityEvent OnCompleteScaling;
    
    public enum Type
    {
        Unset,
        SpesificTransform,
        XYZMode
    }
    [HideInInspector]
    public Type SelectType;

    [HideInInspector]
    public Texture2D Logo;
    
    [HideInInspector]
    [Tooltip("Start function with delayed")]
    public bool BoolDelayTime;
    
    [HideInInspector]
    [Tooltip("Start function with delayed")]
    public float DelayedTime = 2;
    
    private DoScaleObject _doScaleObject;
    
    
    private void Awake()
    {

        _doScaleObject = new DoScaleObject(MyTransform,Speed,InitValue,ScaleValue,SelectEase,MyMode,OnCompleteScaling);
    }

    public virtual void BeginDoScale()
    {
        if (BoolDelayTime)
        {
            StartCoroutine(CoroutineDoScale());
        }
        else
        {
            _doScaleObject.DoScale();
        }
    }

    public virtual void BeginDoSelectScale()
    {
        if (BoolDelayTime)
        {
            StartCoroutine(CoroutineDoSelectScale());
        }
        else
        {
            _doScaleObject.DoSelectScale();
        }
    }
    
    public virtual void ResetScale()
    {
        _doScaleObject.Reset();
    }
    
    
    
    
    
    IEnumerator CoroutineDoScale()
    {
        yield return new WaitForSeconds(DelayedTime);
        
        _doScaleObject.DoScale();
    }
    
    IEnumerator CoroutineDoSelectScale()
    {
        yield return new WaitForSeconds(DelayedTime);
        
        _doScaleObject.DoSelectScale();
    }
}
