using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleAnimationInRangeChecker : MonoBehaviour
{
    private Animator _animator;
    private readonly int _inRange = Animator.StringToHash("InRange");

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    
    bool AnimatedIdleExist()
    {
        foreach (AnimatorControllerParameter param in _animator.parameters)
        {
            if (param.nameHash == _inRange) return true;
        }
        return false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.LogWarning("Trigger Enter (" + transform.name + ") Collider: " + other.name);
            
            if (AnimatedIdleExist())
            {
                _animator.SetBool(_inRange, true);
            }
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.LogWarning("Trigger Enter (" + transform.name + ") Collider: " + other.name);
            
            if (AnimatedIdleExist())
            {
                _animator.SetBool(_inRange, false);
            }
        }
    }
}
