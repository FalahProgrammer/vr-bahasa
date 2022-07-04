using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportAndGrabCheck : MonoBehaviour
{

    public ToggleBehaviour TeleportToggleBehaviour;
    public ToggleBehaviour GrabToggleBehaviour;

    public void LeftPinch()
    {
        if (TeleportToggleBehaviour.Toogle)
        {
            GrabToggleBehaviour.GetComponent<GraspBehaviour>().enabled = false;
            GrabToggleBehaviour.GetComponent<SteamVRLaserController>().enabled = false;
        }
    }

    public void DoneLeftPinch()
    {
        GrabToggleBehaviour.GetComponent<GraspBehaviour>().enabled = true;
        GrabToggleBehaviour.GetComponent<SteamVRLaserController>().enabled = true;
    }

    public void RightPinch()
    {
        if (GrabToggleBehaviour.Toogle)
        {
            TeleportToggleBehaviour.enabled = false;
        }
    }

    public void DoneRightPinch()
    {
        TeleportToggleBehaviour.enabled = true;
    }
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
