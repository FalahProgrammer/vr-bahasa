using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportController : MonoBehaviour, iResetable
{
    private GraspBehaviour _graspBehaviour;

    private TeleportBehaviour _teleportBehaviour;

    private ToggleBehaviour _toggleBehaviour;

    private Booleanbehaviour _booleanbehaviour;
    
    private void Start()
    {
        _graspBehaviour = GetComponent<GraspBehaviour>();

        _teleportBehaviour = GetComponent<TeleportBehaviour>();
        
        _toggleBehaviour = GetComponent<ToggleBehaviour>();

        _booleanbehaviour = GetComponent<Booleanbehaviour>();
    }


    public void Reset()
    {
        _graspBehaviour.DeacitvateMyRaycast();
        
        _teleportBehaviour.Reset();
        
        _toggleBehaviour.Reset();
        
        _booleanbehaviour.SetOffBoolean();
    }
}
