using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class Fading
{
    //blm dipakai
    private CanvasGroup _myCanvasGroup;
    
    private float _speed;
    
    private UnityEvent _onBeginFadingIn;
    
    private UnityEvent _onBeginFadingOut;
    
    private UnityEvent _onCompleteFadingIn;
    
    private UnityEvent _onCompleteFadingOut;

    public Fading(CanvasGroup myCanvasGroup, float _speed, UnityEvent onBeginFadingIn,UnityEvent onBeginFadingOut, UnityEvent onCompleteFadingIn, UnityEvent onCompleteFadingOut)
    {
        _myCanvasGroup = myCanvasGroup;
        
        this._speed = _speed;
        
        _onBeginFadingIn = onBeginFadingIn;
        
        _onBeginFadingOut = onBeginFadingOut;
        
        _onCompleteFadingIn = onCompleteFadingIn;
        
        _onCompleteFadingOut = onCompleteFadingOut;
    }

    
    public void DoMove()
    {
        throw new System.NotImplementedException();
    }

    public void DoLocalMove()
    {
        throw new System.NotImplementedException();
    }

    public void DoSelectMove(float myTarget)
    {
        throw new System.NotImplementedException();
    }

    public void DoSelectLocalMove(float myTarget)
    {
        throw new System.NotImplementedException();
    }

    public void DoLocalRotate()
    {
        throw new System.NotImplementedException();
    }

    public void DoLocalRotateQuaternion()
    {
        throw new System.NotImplementedException();
    }

    public void DoFade(float endValue)
    {
        throw new System.NotImplementedException();
    }
}
