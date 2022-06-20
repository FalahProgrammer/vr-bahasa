using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HandleDirectionController : MonoBehaviour
{
    [SerializeField] private Transform _handle;
    [SerializeField] [Range(0, 1)] float _XAngle = 0.05f;
    [SerializeField] [Range(0, 1)] float _ZAngle = 0.05f;
    [SerializeField] [Range(0, 1)] float _maxPivotXAngle = 0.5f;
    [SerializeField] [Range(0, 1)] float _maxPivotZAngle = 0.5f;
    [Range(0, 1)] float _maxXAngle = 1;
    [Range(0, 1)] float _maxZAngle = 1;
    
    private float _xPercentage;
    private float _yPercentage;

    private void Start()
    {
        _handle = GetComponent<Transform>();
    }
    
    private void Update()
    {
        _XAngle = _handle.localPosition.x;
        if (_XAngle > _maxPivotXAngle)
            _XAngle = _maxPivotXAngle;
        
        if (_XAngle < -_maxPivotXAngle)
            _XAngle = -_maxPivotXAngle;

        _ZAngle = _handle.localPosition.z;
        if (_ZAngle > _maxPivotZAngle)
            _ZAngle = _maxPivotZAngle;
        if (_ZAngle < -_maxPivotZAngle)
            _ZAngle = -_maxPivotZAngle;

        _xPercentage = _XAngle / _maxXAngle;
        _yPercentage = _ZAngle / _maxZAngle;


        _handle.localPosition = new Vector3(_xPercentage,0f, _yPercentage);
        
        _handle.localRotation = Quaternion.identity;
    }
    

   
}
