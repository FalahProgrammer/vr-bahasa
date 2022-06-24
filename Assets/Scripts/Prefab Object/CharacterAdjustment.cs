using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CharacterAdjustment : MonoBehaviour
{
    public bool adjustUIPosition;
    public Vector3 adjustmentUIPosition = new Vector3(); 
    
    [Space(10)]
    public bool adjustPlayerPosition;
    public Vector3 adjustmentPlayerPosition = new Vector3();
    
    [Space(10)]
    public bool adjustPlayerRotation;
    public Vector3 adjustmentPlayerRotation = new Vector3();
}
