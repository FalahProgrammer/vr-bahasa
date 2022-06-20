using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ToggleController : MonoBehaviour
{
    [SerializeField] private Booleanbehaviour _booleanbehaviour;

    [SerializeField] private UnityEvent OnBegin;
    public void SetToggleController()
    {
        if (!_booleanbehaviour.isBoolean)
        {
            OnBegin.Invoke();
        }
    }
}
