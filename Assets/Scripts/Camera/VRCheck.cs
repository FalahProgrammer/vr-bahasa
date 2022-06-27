using UnityEngine;
using UnityEngine.XR.Management;

public class VRCheck : MonoBehaviour
{
    private void Awake()
    {
        if (XRGeneralSettings.Instance.Manager.activeLoader != null) return;
        
        Debug.LogWarning("Initializing XR Failed. Check Editor or Player log for details.");

        var t = transform;
        Vector3 tempPos = t.localPosition;
        tempPos.y += 1;

        t.localPosition = tempPos;
    }
}
