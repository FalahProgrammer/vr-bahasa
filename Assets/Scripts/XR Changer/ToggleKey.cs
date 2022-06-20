using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ToggleBehaviour))]
public class ToggleKey : MonoBehaviour
{
    public GameObject TransformA;
    
    public GameObject TransformB;

    private ToggleBehaviour _toggleBehaviour;

    private void Awake()
    {
        _toggleBehaviour = GetComponent<ToggleBehaviour>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _toggleBehaviour.SetToggle();
        }
    }
    
    public void ActivateTransformA()
    {
        TransformA.SetActive(true);
        
        TransformB.SetActive(false);
    }
    
    public void ActivateTransformB()
    {
        TransformB.SetActive(true);
        
        TransformA.SetActive(false);
    }
}
