using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class IDistanceGrabbingBehaviour : MonoBehaviour, IDistanceGrabbing
{
    [SerializeField] private Interactable _interactable;
    
    [SerializeField] private float _speed = 1;

    [SerializeField] private Ease EaseType = Ease.InQuart;

    private void Start()
    {
        _interactable = GetComponent<Interactable>();
    }

    public void DistanceGrabbing(Hand hand)
    {
        if (transform.GetComponent<InitializeGrab>()._canGrab)
        {
            GrabTypes startingGrabType = hand.GetGrabStarting();

            if (_interactable.attachedToHand == null && startingGrabType != GrabTypes.None)
            {
                gameObject.transform.DOMove(hand.transform.position, _speed).SetEase(EaseType).OnComplete(() => OnCompleteDistanceGrabbing(hand, startingGrabType));
            }
        }
        else
        {
            Debug.LogWarning($"Distance Grabbing can't be execute, please checklist your Initialize Grab (Can Grab bool)");
        }
    }

    private void OnCompleteDistanceGrabbing(Hand hand, GrabTypes grabTypes)
    {
        hand.AttachObject(gameObject, grabTypes);
    }
}
