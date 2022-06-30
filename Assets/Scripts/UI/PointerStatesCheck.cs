using System.Collections;
using System.Collections.Generic;
using Leap.Unity.InputModule;
using UnityEngine;
using UnityEngine.Events;

public class PointerStatesCheck : MonoBehaviour
{
    public PointerElement PointerElement;
    public PointerStates PointerStates;

    public UnityEvent OnCanvas;
    public UnityEvent OffCanvas;

    // Update is called once per frame
    void Update()
    {
        PointerStates = PointerElement.PointerState;
        if (PointerStates == PointerStates.OffCanvas)
        {
            OffCanvas.Invoke();
        }
        else
        {
            OnCanvas.Invoke();
        }
    }
}
