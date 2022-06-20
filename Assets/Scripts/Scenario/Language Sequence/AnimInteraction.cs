using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class AnimInteraction
{
    [SerializeField] public int QuestionID;
    
    [SerializeField] public List<Animator> Animators = new List<Animator>();
    
    [SerializeField] public List<string> AnimationState = new List<string>();
    
    [SerializeField] public AudioClip AudioClip;
    
    public float AudioDelay;
    
    public float AnimationDelay;
    
    [SerializeField] public bool WaitForInteraction;
    
    [SerializeField] public float Length;
    
    [SerializeField] public UnityEvent OnPartialAnimationPlayed;
    
    [SerializeField] public UnityEvent OnPartialAnimationFinished;
}

