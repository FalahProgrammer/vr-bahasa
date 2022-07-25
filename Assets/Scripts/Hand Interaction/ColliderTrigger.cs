using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider),typeof(Rigidbody))]
public class ColliderTrigger : MonoBehaviour
{
    public Transform CurrentCollider;

    public UnityEvent TriggerEnter;
    
    public UnityEvent TriggerExit;

    private void OnEnable()
    {
        GetComponent<Collider>().isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Grabable Object")
        {
            Debug.Log("Triggered by Object");

            CurrentCollider = other.transform;
        }
        
        if (other.tag == "Reset Gesture")
        {
            CurrentCollider = other.transform;
            
            TriggerEnter.Invoke();
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        CurrentCollider = null;
        
        TriggerExit.Invoke();
    }
}
