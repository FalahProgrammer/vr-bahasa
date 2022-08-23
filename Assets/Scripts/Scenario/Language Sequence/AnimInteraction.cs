using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class AnimInteraction
{ 
    public string name;
    
    [SerializeField] public int QuestionID;
    
     public List<Animator> Animators = new List<Animator>();
    
     public List<string> AnimationState = new List<string>();
    
    [SerializeField] public AudioClip AudioClip;
    
    [HideInInspector] public float AudioDelay;
    
    public float AnimationDelay;
    
    // [SerializeField] public bool WaitForInteraction;
    // no longer used as the script now check if there is an audio clip in the file or not to set for wait for interaction
    
    [HideInInspector] public float Length;
    
    [HideInInspector] public UnityEvent OnPartialAnimationPlayed;
    
    [HideInInspector] public UnityEvent OnPartialAnimationFinished;
}

