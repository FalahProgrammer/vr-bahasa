using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ToggleButton : MonoBehaviour
{
    public UnityEvent IsOn;
    public UnityEvent IsOff;
    
    public void SetToggle(Toggle toggle) {
        if (toggle.isOn) {

            IsOn.Invoke();

        } else {
            IsOff.Invoke();
        }
    }
}
