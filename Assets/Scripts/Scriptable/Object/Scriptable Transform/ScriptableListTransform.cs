using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Transforms List", menuName = "List/Transforms")]
public class ScriptableListTransform : ScriptableObject
{
    public List<Transform> MyTransforms = new List<Transform>();
}
