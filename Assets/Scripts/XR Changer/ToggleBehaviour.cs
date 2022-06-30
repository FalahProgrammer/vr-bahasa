using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ToggleBehaviour : MonoBehaviour,iResetable
{
    public bool Toogle;
    
    public UnityEvent IsOn;
    
    public UnityEvent IsOff;

    public void SetToggle() {
        if (Toogle)
        {
            Toogle = false;
            IsOn.Invoke();

        } else
        {
            Toogle = true;
            IsOff.Invoke();
        }
    }

    public void Reset()
    {
        Toogle = false;
        
        //IsOff.Invoke();
    }
}
