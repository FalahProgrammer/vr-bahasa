using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Integer Variable", menuName = "Variable/Integer Data Variable")]
public class IntegerVariable : ScriptableObject
{
    public int IntegerValue;

    public void IntValue(int intvalue)
    {
        IntegerValue = intvalue;
    }
}