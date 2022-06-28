using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class FadingBehaviour : MonoBehaviour, iResetable
{
    [HideInInspector] public CanvasGroup _myCanvasGroup;
    
    [HideInInspector] public Image _myClockPanel;
    
    [HideInInspector] public Transform EyeController;

    [HideInInspector] public float _speed;
    
    public enum Type
    {
        Unset,
        Normal,
        Gradient,
        Eye,
        Clock
    }
    [HideInInspector] public MyMode Mode;
    
    public enum MyMode
    {
        X,
        Y,
        Z
    }
    
    [HideInInspector] public float _targetValue;
    
    [HideInInspector] public Ease _selectEase;
    
    [HideInInspector] public bool InOut;
    
    [HideInInspector] public bool OnStart;
    
    [HideInInspector] public bool FadingIn;
    
    [HideInInspector] public bool FadingOut;
        
    [HideInInspector] public Type FadingType;

    [HideInInspector] public UnityEvent OnBeginFadingIn;
    
    [HideInInspector] public UnityEvent OnBeginFadingOut;
    
    [HideInInspector] public UnityEvent OnCompleteFadingIn;
    
    [HideInInspector] public UnityEvent OnCompleteFadingOut;
    
    [HideInInspector] public Texture2D Logo = null;
    
    /*private Fading _fading;
    private void Start()
    {
        _fading = new Fading(_myCanvasGroup,_speed,OnBeginFadingIn,OnBeginFadingOut,OnCompleteFadingIn,OnCompleteFadingOut);
    }

    public void BeginFadingIn()
    {
        _fading.BeginFadingIn();
    }
    
    public void BeginFadingOut()
    {
        _fading.BeginFadingOut();
    }*/

    private void Awake()
    {
        if (OnStart)
        {
            
            if (FadingIn)
            {
                BeginFadingIn();
            }

            if (FadingOut)
            {
                BeginFadingOut();
            }
        }
        switch (FadingType)
        {
            case Type.Normal:
                
                _myCanvasGroup.gameObject.AddComponent<InitialPosRotBehaviour>();
                
                break;
            
            case Type.Gradient:
                
                _myCanvasGroup.gameObject.AddComponent<InitialPosRotBehaviour>();
                
                break;
            
            case Type.Eye:
                
                break;
            
            case Type.Clock:
                
                break;
        }
    }

    public void BeginFadingIn()
    {
        switch (FadingType)
        {
            case Type.Normal:
                
                //Reset();
                
                OnBeginFadingIn.Invoke();

                _myCanvasGroup.alpha = 0;
                
                //_myCanvasGroup.transform.localPosition = new Vector3(_targetValue,0,0);
                
                _myCanvasGroup.DOFade(1, _speed).SetId("Fade In Normal").OnComplete(OnCompleteFadeIn);
                

                break;
            
            case Type.Gradient:
                
                //Reset();
                
                OnBeginFadingIn.Invoke();
                
                _myCanvasGroup.alpha = 1;
                
                _myCanvasGroup.transform.localPosition = _myCanvasGroup.GetComponent<InitialPosRotBehaviour>().GetLocalPos;
                
                switch (Mode)
                {
                    case MyMode.X:
                        _myCanvasGroup.transform.DOLocalMoveX(_targetValue, _speed)
                            .SetEase(_selectEase).SetId("Fade In Gradient").OnComplete(OnCompleteFadeIn);
                        break;
                    case MyMode.Y:
                        _myCanvasGroup.transform.DOLocalMoveY(_targetValue, _speed)
                            .SetEase(_selectEase).SetId("Fade In Gradient").OnComplete(OnCompleteFadeIn);
                        break;
                    case MyMode.Z:
                        _myCanvasGroup.transform.DOLocalMoveZ(_targetValue, _speed)
                            .SetEase(_selectEase).SetId("Fade In Gradient").OnComplete(OnCompleteFadeIn);
                        break;
                }
                
                break;
            
            case Type.Eye:
                
                OnBeginFadingIn.Invoke();
                
                EyeController.DOScaleY(0, _speed)
                    .SetEase(_selectEase).SetId("Fade In Eye").OnComplete(OnCompleteFadeIn);
                break;
            
            case Type.Clock:
                
                OnBeginFadingIn.Invoke();

                _myClockPanel.DOFillAmount(1, _speed).SetEase(_selectEase).SetId("Fade In Clock").OnComplete(OnCompleteFadeIn);
                 
                break;
        }
        
    }
    
    public void BeginFadingOut()
    {
        
        switch (FadingType)
        {
            case Type.Normal:

                OnBeginFadingOut.Invoke();
        
                _myCanvasGroup.DOFade(0, _speed).SetId("Fade In Normal").OnComplete(OnCompleteFadeOut);
                
                
                break;
            
            case Type.Gradient:
                
                OnBeginFadingOut.Invoke();

                _myCanvasGroup.transform.DOLocalMove(_myCanvasGroup.GetComponent<InitialPosRotBehaviour>().GetLocalPos, _speed)
                    .SetEase(_selectEase).SetId("Fade Out Gradient").OnComplete(OnCompleteFadeOut);;
                
                break;
            
            case Type.Eye:
                
                OnBeginFadingOut.Invoke();
                
                EyeController.DOScaleY(0.5f, _speed)
                    .SetEase(_selectEase).SetId("Fade Out Eye").OnComplete(OnCompleteFadeOut);
                
                break;
            
            case Type.Clock:
                
                OnBeginFadingOut.Invoke();

                _myClockPanel.DOFillAmount(0, _speed).SetEase(_selectEase).SetId("Fade Out Clock").OnComplete(OnCompleteFadeIn);
                 
                break;
            
        }
        
        
    }
    
    private void OnCompleteFadeOut()
    {
        OnCompleteFadingOut.Invoke();
        
        _myCanvasGroup.interactable = false;
            
        _myCanvasGroup.blocksRaycasts = false;
    }

    public void OnCompleteFadeIn()
    {
        if (InOut)
        {
            BeginFadingOut();
        }
        else
        {
            OnCompleteFadingIn.Invoke();
            
            _myCanvasGroup.interactable = true;
            
            _myCanvasGroup.blocksRaycasts = true;
        }
        
    }

    public void Reset()
    {
        _myCanvasGroup.GetComponent<Image>().type = Image.Type.Simple;

        _myCanvasGroup.transform.localScale =
            _myCanvasGroup.transform.GetComponent<InitialPosRotBehaviour>().GetLocalScale;
                
        _myCanvasGroup.transform.GetComponent<RectTransform>().sizeDelta =
            _myCanvasGroup.transform.GetComponent<InitialPosRotBehaviour>().SizeDelta;
    }
}
