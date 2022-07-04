using System;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class UserPanelLeftHand : MonoBehaviour
{
    [SerializeField] private Transform _userPanel;
    [SerializeField] private Transform _rotationPivot;
    [SerializeField] private Transform _targetPivot;
    
    [Space(10)]
    [SerializeField] private Vector3 uiOffset = Vector3.zero;
    
    [Space(10)]
    public UnityEvent OnEnableEvent;
    public UnityEvent OnDisableEvent;

    [SerializeField] private bool _leftHandActive;
    [SerializeField] private bool _fadeStatus;
    [SerializeField] private bool _panelActive;

    private void OnEnable()
    {
        _fadeStatus = false;
        _leftHandActive = true;
    }
    
    private void OnDisable()
    {
        _leftHandActive = false;
        
        if (!_fadeStatus) return;
        
        OnDisableEvent.Invoke();
        _fadeStatus = false;
    }

    public bool Teleporting { get; set; }

    public void ForceDisable()
    {
        _leftHandActive = false;
        
        if (!_fadeStatus) return;
        
        OnDisableEvent.Invoke();
        _fadeStatus = false;
    }

    private void Update()
    {
        if (!_leftHandActive)
        {
            if (!_fadeStatus) return;
            
            OnDisableEvent.Invoke();
            _fadeStatus = false;

            return; 
        }
        
        var angle = Mathf.RoundToInt(_rotationPivot.transform.rotation.eulerAngles.z);

        if (Teleporting)
        {
            OnDisableEvent.Invoke();
            _fadeStatus = false;
        }
        else
        {
            if (angle <= 40 && !_fadeStatus || 
                angle >= 320 && !_fadeStatus)
            {
                _fadeStatus = true;
                OnEnableEvent.Invoke();
            }
            else if (angle > 40 && angle < 320 && _fadeStatus)
            {
                OnDisableEvent.Invoke();
                _fadeStatus = false;
            }
        }
        
        

        var temp = _targetPivot.position;
        temp.x += uiOffset.x;
        temp.y += uiOffset.y;
        temp.z += uiOffset.z;
        _userPanel.position = temp;
    }
}
