﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class ToggleBehaviour : MonoBehaviour,iResetable
{
    public bool Toogle;
    
    public UnityEvent IsOn;
    
    public UnityEvent IsOff;

    public void SetToggle() {
        if (Toogle)
        {
            Toogle =! Toogle;
            IsOn.Invoke();

        } else
        {
            Toogle =! Toogle;
            IsOff.Invoke();
        }
    }

    public void Reset()
    {
        Toogle = false;
        
        //IsOff.Invoke();
    }
}
