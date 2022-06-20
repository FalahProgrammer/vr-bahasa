using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class LeapMotionEvent
{
    [Serializable] public sealed class LeapEvents : UnityEvent { }
    
    public LeapEvents OnHandEnable = new LeapEvents();
    
    public LeapEvents OnHandDisable = new LeapEvents();
    
    private bool oneTimeCall;
    
    public void HandEnabled(Transform transform)
    {
        oneTimeCall = false;
        
        if (!oneTimeCall)
        {
            if (transform.gameObject.activeInHierarchy)
            {
                OnHandEnable.Invoke();
                //Debug.Log("Hand Enabled");

                oneTimeCall = true;
            }
        }
    }
    
    public void HandDisabled(Transform transform)
    {
        if (!oneTimeCall)
        {
            if (!transform.gameObject.activeInHierarchy)
            {
                OnHandDisable.Invoke();
                //Debug.Log("Hand Disabled");

                oneTimeCall = true;
            }
        }
    }
}
