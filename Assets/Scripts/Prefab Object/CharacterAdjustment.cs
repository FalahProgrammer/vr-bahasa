using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CharacterAdjustment : MonoBehaviour
{
    public bool playerIsSitting;
    
    [Space(10)]
    //public bool adjustUIPosition;
    public Vector3 adjustmentUIPosition = new Vector3();

    [Space(5)]
    //public bool adjustPlayerPosition;
    public Vector3 adjustmentPlayerPosition = new Vector3();
    
    [Space(5)]
    //public bool adjustPlayerRotation;
    public Vector3 adjustmentPlayerRotation = new Vector3();
    
    [Space(10)]
    public Transform characterPivot;
    public Transform uiPivot;
}
