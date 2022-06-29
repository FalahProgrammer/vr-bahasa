using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class UserPanelLeftHand : MonoBehaviour
{
    [SerializeField] private Transform _userPanel;
    [SerializeField] private Transform _leftHand;

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

    private void Update()
    {
        if (!_activeStatus)
        {
            if (!_fadeStatus) return;
            
            OnDisableEvent.Invoke();
            _fadeStatus = false;

            return; 
        }
        
        var angle = Mathf.RoundToInt(_leftHand.transform.rotation.eulerAngles.z);

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

        var temp = _leftHand.position;
        temp.y += 0.25f;
        _userPanel.position = temp;
        
        //debug
        tmpDebug.text = "Angle: " + angle.ToString() + "<br>Fade Status: " + _fadeStatus;
    }
}
