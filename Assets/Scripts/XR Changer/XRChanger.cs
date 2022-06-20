using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class XRChanger : MonoBehaviour
{
    
    void Awake()
    {
        //StartCoroutine(LoadDevice("OpenXR", true));
    }

    IEnumerator LoadDevice(string newDevice, bool enable)
    {
        XRSettings.LoadDeviceByName(newDevice);
        
        yield return null;
        
        XRSettings.enabled = enable;
    }

    void DisableVR()
    {
        StartCoroutine(LoadDevice("", false));
    }
}
