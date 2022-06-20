using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Scriptable GameObject Data Controller", menuName = "Object/GameObject Data Controller")]
public class ScriptableGameObjectDataController : ScriptableObject
{
    public GameObject Value;

    public GameObject ContentButton;

    public GameObject ChapterButton;
    
    public GameObject AnimationButton;
}