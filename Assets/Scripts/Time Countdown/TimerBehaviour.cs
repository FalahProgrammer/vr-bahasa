using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TimerBehaviour : MonoBehaviour, iResetable
{
    private bool IsPlaying;

    [HideInInspector]
    public bool _useCountdown;
     
    [HideInInspector]
    public float _initialDuration;
    
    [HideInInspector]
    public float _currentDuration;
    
    [HideInInspector]
    public UnityEvent onTimerStart;
    
    [HideInInspector]
    public UnityEvent onTickEvent;
    
    [HideInInspector]
    public UnityEvent onTimerEnd;

    [HideInInspector] public Texture2D Logo;

    private string time;
    public enum Type
    {
        Unset,
        Countdown,
        Counting
    }
    [HideInInspector]
    public Type SelectType;
    
    private Text _percentage;

    private void Start()
    {
        _percentage = GetComponentInChildren<Text>();
    }

    private void OnEnable()
    {
        onTimerStart.Invoke();

        if (_useCountdown)
        {
            _currentDuration = _initialDuration;
        }
        else
        {
            _currentDuration = 0;
        }
    }
    
    public string TheTime()
    {
        float theTime = Mathf.Round(_currentDuration);
        TimeSpan ts = TimeSpan.FromSeconds(theTime);

        return ts.ToString();
    }

    public string GetTime() => TheTime();
    
    public void StartCounting()
    {
        IsPlaying = true;
    }
    
    public void StopCounting()
    {
        IsPlaying = false;
    }

    
    private void Update()
    {
        if (IsPlaying)
        {
            if (_useCountdown)
            {
                //Debug.Log(_currentDuration);
                
                _currentDuration -= Time.deltaTime;

                
                
                //_percentage.text = (GetComponent<Image>().fillAmount * 100f).ToString(("N0")) + " %";;
                
                onTickEvent.Invoke();

                if (_currentDuration <= 0)
                {
                    _currentDuration = 0;
            
                    //   
                    //onTimerEnd.Invoke();
                    
                    IsPlaying = false;
                    
                    
            
                    //enabled = false;

                    //Any Action Object Here
                    
                   
                }
            }
            else
            {
                
                _currentDuration += Time.deltaTime;
                
                //_percentage.text = (GetComponent<Image>().fillAmount * 100f).ToString(("N0")) + " %";;
                
                onTickEvent.Invoke();
        
                if (_initialDuration <= _currentDuration)
                {
                    _currentDuration = _initialDuration;
            
                    onTimerEnd.Invoke();
                    
                    

                    IsPlaying = false;
                }
            }
        }
    }

    public void Reset()
    {
        if (_useCountdown)
        {
            _currentDuration = _initialDuration;
        }
        else
        {
            _currentDuration = 0;
        }
        
        
        enabled = true;
        
        IsPlaying = false;
    }

    public void DisableTimer()
    {
        _currentDuration = _initialDuration;
        
        enabled = false;
    }
}