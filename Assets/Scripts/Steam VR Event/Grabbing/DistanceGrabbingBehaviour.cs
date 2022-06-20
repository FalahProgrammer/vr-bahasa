using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class DistanceGrabbingBehaviour : MonoBehaviour, IDistanceGrabbing
{
    public GraspBehaviour _graspBehaviour;
    public Hand MyHand;
    public float Range = 0.25f;
    public float _distance;
    public bool _isDistanceGrabbing;

    private void Update()
    {
        if (MyHand)
        {
            if (_graspBehaviour._myTarget)
            {
                //_distanceCalculator.BeginCalculation(MyHand.transform,_graspBehaviour._myTarget);
                _distance = Vector3.Distance(MyHand.transform.position, _graspBehaviour._myTarget.position);

                if (_distance > Range)
                {
                    _isDistanceGrabbing = true;
                }
                else
                {
                    _isDistanceGrabbing = false;
                }
            }
        }
    }
    
    public void BeginDistanceGrabbing()
    {
        DistanceGrabbing(MyHand);
    }

    public void DistanceGrabbing(Hand hand)
    {
        if (_isDistanceGrabbing)
        {
            if (_graspBehaviour._myTarget.GetComponent<IDistanceGrabbingBehaviour>())
            {
                _graspBehaviour._myTarget.GetComponent<IDistanceGrabbingBehaviour>().DistanceGrabbing(hand);
            }
        }
    }
}
