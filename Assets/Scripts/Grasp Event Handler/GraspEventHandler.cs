using System;
using System.Collections;
using System.Collections.Generic;
using Leap.Unity.Interaction;
using UnityEngine;
using UnityEngine.Events;

public class GraspEventHandler : MonoBehaviour
{
    public UnityEvent OnGraspBegin;
    
    public UnityEvent OnGraspStay;
    
    public UnityEvent OnGraspEnd;

    private void OnEnable()
    {
        GetComponent<InteractionBehaviour>()
            .OnGraspBegin += () => BeginGrasping();
        
        GetComponent<InteractionBehaviour>()
            .OnGraspStay += ()=> GraspingStay();
        
        GetComponent<InteractionBehaviour>()
            .OnGraspEnd += ()=> GraspingEnd();
    }

    private void OnDisable()
    {
        GetComponent<InteractionBehaviour>()
            .OnGraspBegin = null;
        
        GetComponent<InteractionBehaviour>()
            .OnGraspStay = null;
        
        GetComponent<InteractionBehaviour>()
            .OnGraspEnd = null;
    }

    void BeginGrasping()
    {
        OnGraspBegin.Invoke();
    }
    
    void GraspingStay()
    {
        OnGraspStay.Invoke();
    }
    
    void GraspingEnd()
    {
        OnGraspEnd.Invoke();
    }
}
