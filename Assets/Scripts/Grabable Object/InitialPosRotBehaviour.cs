using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialPosRotBehaviour : MonoBehaviour
{
    public Vector3 GetPos;
    
    public Vector3 GetLocalPos;
    
    public Quaternion GetRot;

    public Vector3 GetLocalScale;

    public Vector2 SizeDelta;

/*    public enum scaleobject{
        Big,
        Small
    }

    public scaleobject ScaleObject;*/

    public void Awake ()
    {
        if (transform.GetComponent<RectTransform>() != null)
        {
            SizeDelta = transform.GetComponent<RectTransform>().sizeDelta;
        }
        
        GetLocalScale = transform.localScale;
        
        GetPos = transform.position;

        GetLocalPos = transform.localPosition;
        
        GetRot = transform.rotation;
    }
}
