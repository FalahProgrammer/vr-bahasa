using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "List Interactor", menuName = "List/Interactor")]
public class ListInteractor : ScriptableObject
{
   public Transform homeTransform;
   public Vector3 homeVector = Vector3.zero;
   
   [Space(10)]
   public List<Transform> listInteractors = new List<Transform>();
   public List<Vector3> ListUIPosition = new List<Vector3>();
   public List<Vector3> listCharacterPosition = new List<Vector3>();
   public List<Vector3> listCharacterRotation = new List<Vector3>();
}
