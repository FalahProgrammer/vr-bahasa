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
    [SerializeField] private Vector3 uiOffset = Vector3.zero;
    
    public UnityEvent OnEnableEvent;
    public UnityEvent OnDisableEvent;

    public bool _activeStatus;
    public bool _fadeStatus;

    public TextMeshProUGUI tmpDebug;

    private void OnEnable()
    {
        _fadeStatus = false;
        _activeStatus = true;
    }
    
    private void OnDisable()
    {
        _activeStatus = false;
        
        if (!_fadeStatus) return;
        
        OnDisableEvent.Invoke();
        _fadeStatus = false;
    }

    public void ForceDisable()
    {
        
    }

    private void Update()
    {
        if (!_activeStatus)
        {
            if (!_fadeStatus) return;
            
            OnDisableEvent.Invoke();
            _fadeStatus = false;

            return; 
        }
        
        var angle = Mathf.RoundToInt(_rotationPivot.transform.rotation.eulerAngles.z);

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

        var temp = _targetPivot.position;
        temp.x += uiOffset.x;
        temp.y += uiOffset.y;
        temp.z += uiOffset.z;
        _userPanel.position = temp;
        
        //debug
        tmpDebug.text = "Angle: " + angle + "<br>Fade Status: " + _fadeStatus;
    }
}
