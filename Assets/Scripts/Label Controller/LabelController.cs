using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.Events;

public class LabelController : MonoBehaviour
{
    [SerializeField] private GraspBehaviour _rayGraspBehaviour;

    public UnityEvent OnSelectingGrabableObjectWithLabel;
    
    public void CheckForLabel()
    {
        if (_rayGraspBehaviour._myTarget.CompareTag("Grabable"))
        {
            OnSelectingGrabableObjectWithLabel?.Invoke();
        }
    }
}
