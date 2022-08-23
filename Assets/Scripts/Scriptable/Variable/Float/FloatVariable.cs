using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Float Variable", menuName = "Variable/Float Data Variable")]
public class FloatVariable : ScriptableObject
{
    public float floatValue;

    public void IntValue(float value)
    {
        Debug.Log("Int Value: " + value);
        floatValue = value;
    }
}