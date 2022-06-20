using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeapMotionEventBehaviour : MonoBehaviour
{
    [SerializeField] private Transform MyHand;
    
    public delegate void LeapEventsHandler(Transform transform);

    public LeapMotionEvent LeapMotionEvent;
    private void Update()
    {

            LeapMotionEvent.HandEnabled(MyHand);
        
            LeapMotionEvent.HandDisabled(MyHand);

        
        

    }

    private void OnEnable()
    {
        HandEnabled += LeapMotionEvent.HandEnabled;
        
        HandDisabled += LeapMotionEvent.HandDisabled;
    }

    private void OnDisable()
    {
        HandEnabled -= LeapMotionEvent.HandEnabled;
        
        HandDisabled -= LeapMotionEvent.HandDisabled;
    }

    /// <summary>
    /// When hand is activated / deactivated in hierarchy
    /// </summary>
    public event LeapEventsHandler HandEnabled;
    
    
    /// <summary>
    /// When hand is activated / deactivated in hierarchy
    /// </summary>
    public event LeapEventsHandler HandDisabled;
}
