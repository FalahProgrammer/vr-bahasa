using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddObjectsBehaviour : MonoBehaviour
{
    private AddObjects _addObjects;
    
    [SerializeField] private ScriptableListTransform scriptableListTransform;
    
    private void Awake()
    {
        _addObjects = new AddObjects(this.transform,scriptableListTransform);
    }

    private void OnEnable()
    {
        _addObjects.Add();
    }

    private void OnDisable()
    {
        _addObjects.Remove();
    }
}
