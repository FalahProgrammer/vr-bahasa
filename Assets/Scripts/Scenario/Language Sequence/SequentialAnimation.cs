using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class SequentialAnimation : MonoBehaviour
{
    public int _currentIteration;
    public int scenario_id;
    public int _id;
    [SerializeField] private IntegerVariable _integerVariable;
    [SerializeField] public List<AnimInteraction> AnimationList = new List<AnimInteraction>();
    public AudioSource AudioSource;
    public Action AnimationFinished;
    public Action AnimationStarted;
    //public UnityEvent OnAnimationFinished;
    
    private Coroutine _playingAnimation;
    private Coroutine _continueAnimation;
    private AnimInteraction _buffer;
    
    public bool CanBeInterrupted;
    public bool IsPlaying;


    
    private void OnValidate()
    {
        for (int i = 0; i < AnimationList.Count; i++)
        {
            //AnimationList[i].Length = 0.5f;
            
            //AnimationList[i].Animators.Clear();
        }
    }

    public void PlayAnimation()
    {
        if (IsPlaying && !CanBeInterrupted)
            return;
        
        if (_id == _integerVariable.IntegerValue || _id == 0)
        {
            IsPlaying = true;
            _playingAnimation = StartCoroutine(PlayAnimationCoroutine());
        }
    }
    
    IEnumerator PlayAnimationCoroutine()
    {
        if(AnimationStarted!=null)
            AnimationStarted.Invoke();
        
        for (int i = 0; i < AnimationList.Count; i++)
        {
            var a = AnimationList[i].Animators;
            
            if(AnimationList[i].OnPartialAnimationPlayed!=null)
                AnimationList[i].OnPartialAnimationPlayed.Invoke();
            
            for (int j = 0; j < AnimationList[i].Animators.Count; j++)
            {
                a[j].enabled = true;
                a[j].speed = 1;
                
                Debug.Log("Jumlah Iterasi Play: " + AnimationList[i].Animators.Count);
                Debug.Log("NPC: " + AnimationList[i].Animators[0].transform.parent.name);
                
                if(!IsPlaying)
                {
                    yield break;
                    //StopCoroutine(PlayAnimationCoroutine());
                }
                //if it is first time, we shouldn't use CrossFade as to prevent anim overlap
                if (i == 0)
                {
                    yield return new WaitForSeconds(AnimationList[i].AnimationDelay);
                    
                    //a[j].Play(AnimationList[i].AnimationState[j], 0);
                    a[j].CrossFade(AnimationList[i].AnimationState[j], 0.1f);
                }
                else
                {
                    yield return new WaitForSeconds(AnimationList[i].AnimationDelay);
                    
                    a[j].CrossFade(AnimationList[i].AnimationState[j], 0.1f);
                }

                if (AudioSource != null)
                {
                    AudioSource.clip = AnimationList[i].AudioClip;

                    //There's problem that PlayDelayed can't be stopped via AudioSource.Stop()
                    //Therefore we need to make do
                    if(IsPlaying)
                    {
                        AudioSource.PlayDelayed(AnimationList[i].AudioDelay);
                    }
                    else
                    {
                        AudioSource.Play();
                    }
                    
                    
                }
            }

            //if (AnimationList[i].WaitForInteraction)
            if (AnimationList[i].AudioClip != null)
            {
                _currentIteration = AnimationList.IndexOf(AnimationList[i]);
                
                Debug.Log("Anim Lenght : " + AnimationList[i].AudioClip.length);

                //yield return new WaitForSeconds(AnimationList[i].Length);
                yield return new WaitForSeconds(AnimationList[i].AudioClip.length);
                
                if(AnimationList[i].OnPartialAnimationFinished!=null)
                    AnimationList[i].OnPartialAnimationFinished.Invoke();
                
                yield break;
            }
            else
            {
                Debug.Log("Anim Lenght : " + AnimationList[i].AudioClip.length);
                
                yield return new WaitForSeconds(AnimationList[i].AudioClip.length);
                
                if(AnimationList[i].OnPartialAnimationFinished!=null)
                    AnimationList[i].OnPartialAnimationFinished.Invoke();
            }

        }
 
        IsPlaying = false;
        
        for (int i = 0; i < AnimationList.Count; i++)
        {
            if(AnimationList[i].OnPartialAnimationFinished!=null)
                AnimationList[i].OnPartialAnimationFinished.Invoke();
        }

        if(AnimationFinished!=null)
            AnimationFinished.Invoke();
    }

    public void ContinueAnimation()
    {
        if (_id == _integerVariable.IntegerValue || _id == 0)
        {
            _continueAnimation = StartCoroutine(ContinueAnimationCoroutine());
        }
    }
    
    IEnumerator ContinueAnimationCoroutine()
    {
        //Filtering the list from finished sequences
        var l = new List<AnimInteraction>();
        
        for (int i = 0; i < AnimationList.Count; i++)
        {
            if (AnimationList.IndexOf(AnimationList[i]) > _currentIteration)
                l.Add(AnimationList[i]);
        }

        for (int i = 0; i < l.Count; i++)
        {
            Debug.Log("Jumlah Iterasi Continue : " + AnimationList[i].Length);
            
            if(l[i].OnPartialAnimationPlayed!=null)
                l[i].OnPartialAnimationPlayed.Invoke();
            
            var a = l[i].Animators;
            for (int j = 0; j < l[i].Animators.Count; j++)
            {
                a[j].enabled = true;
                a[j].speed = 1;
                if(!IsPlaying)
                {
                    StopCoroutine(ContinueAnimationCoroutine());
                }
                
                a[j].CrossFade(l[i].AnimationState[j],0.1f);

                if (AudioSource != null)
                {
                    AudioSource.clip = l[i].AudioClip;
                    if(IsPlaying)
                        AudioSource.PlayDelayed(l[i].AudioDelay);
                }
            }

            //if (l[i].WaitForInteraction)
            if (l[i].AudioClip != null)
            {
                _currentIteration = AnimationList.IndexOf(l[i]);
                
                Debug.Log("Anim Lenght Continue : " + AnimationList[_currentIteration].AudioClip.length);
                
                Debug.Log("Anim Lenght Continue Name : " + AnimationList[_currentIteration].AudioClip.name);
                
                yield return new WaitForSeconds(AnimationList[_currentIteration].AudioClip.length + 0.5f);
                
                
                if(l[i].OnPartialAnimationFinished!=null)
                    l[i].OnPartialAnimationFinished.Invoke();
                
                yield break;
            }
            else
            {
                yield return new WaitForSeconds(0.5f);

                if (l[i].OnPartialAnimationFinished != null)
                {
                    l[i].OnPartialAnimationFinished.Invoke();
                }
                
            }
 
        }

        IsPlaying = false;

        if(AnimationFinished!=null)
            AnimationFinished.Invoke();
 }

    public void Reset()
    {
        StopCoroutine();
        IsPlaying = false;
        _currentIteration = 0;
        if(_buffer!=null)
        {
            AnimationList.Add(_buffer);
            _buffer = null;
        }
    }

    void Stop()
    {
        for (int i = AnimationList.Count-1; i >=  0; i--)
        {
            var a = AnimationList[i].Animators;
            
            if (a==null) continue;
            
            for (int j = a.Count-1; j >= 0; j--)
            {
                if (a[j].gameObject.scene.name == null) continue;
                if (a[j]==null) continue;

                a[j].Play("IDLE",0,0.0f);
                //a[j].speed = 0;
                if (AudioSource != null)
                {
                    AudioSource.clip = AnimationList[i].AudioClip;
                    AudioSource.Stop();
                }
            }
        }
        
    }
    public void StopCoroutine()
    {
        Stop();
        StopAllCoroutines();
    }

    public void DeleteLastAnim()
    {
        var i = AnimationList.Count - 1;
        _buffer = AnimationList[i];
        AnimationList.RemoveAt(i);
    }

    /*private void OnDestroy()
    {
        Reset();
        StopCoroutine();
    }*/

    public IntegerVariable AssignIntegerVariable
    {
        get { return _integerVariable; }
        set { _integerVariable = value; }
    }
}
