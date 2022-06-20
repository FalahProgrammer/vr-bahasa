using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayController : MonoBehaviour, iResetable
{
    private GraspBehaviour _graspBehaviour;

    private ToggleBehaviour _toggleBehaviour;

    private Booleanbehaviour _booleanbehaviour;

    private void Start()
    {
        _graspBehaviour = GetComponent<GraspBehaviour>();

        _toggleBehaviour = GetComponent<ToggleBehaviour>();

        _booleanbehaviour = GetComponent<Booleanbehaviour>();
    }


    public void Reset()
    {
        _graspBehaviour.DeacitvateMyRaycast();
        
        _toggleBehaviour.Reset();
        
        _booleanbehaviour.SetOffBoolean();
    }
}
