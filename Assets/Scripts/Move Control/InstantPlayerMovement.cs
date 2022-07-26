using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantPlayerMovement : MonoBehaviour
{
    [SerializeField] private Transform _cameraRig;
    [SerializeField] private Transform _target;
    [SerializeField] private ListInteractor _listInteractor;

    public void movePlayer()
    {
        _target.position = _listInteractor.homeTransform.position;
        _target.rotation = _listInteractor.homeTransform.rotation;
        _cameraRig.position = _target.position;
        _cameraRig.rotation = _target.rotation;
    }
}
