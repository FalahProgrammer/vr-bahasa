using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeechStep : MonoBehaviour, ICommandValues
{
    public AudioSource _source;
    public AudioClip _clip;
    public Animator _anim;
    public string _animStateName;
    public float _waitBefore;
    public float _waitAfter;
    private CustomTimer _timer;
    public StepType Type { get; set; }
    public bool IsWaitForUserInteraction { get; set; }

    public void Initialize()
    {
        Type = StepType.SPEECH;
        IsWaitForUserInteraction = false;
        
        if (!_timer) _timer = FindObjectOfType<CustomTimer>();
    }

    public void Execute(Action onFinished)
    {
        _timer.Wait(_waitBefore,()=>Play(onFinished));
    }

    private void Play(Action onFinished)
    {
        //_anim.Play(_animStateName);
        _anim.CrossFade(_animStateName,0.5f);
        _source.PlayOneShot(_clip);
        _timer.Wait(_clip.length+_waitAfter,onFinished);
    }
}

