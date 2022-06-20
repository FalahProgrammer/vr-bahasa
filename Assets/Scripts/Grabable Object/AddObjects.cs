using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddObjects : IAddRemoveObject
{
    private Transform _myTransform;
    
    private ScriptableListTransform _scriptableListTransform;

    public AddObjects(Transform myTransform, ScriptableListTransform scriptableListTransform)
    {
        _myTransform = myTransform;
        
        _scriptableListTransform = scriptableListTransform;
    }

    public void Add()
    {
        if (!_scriptableListTransform.MyTransforms.Contains(_myTransform))
            _scriptableListTransform.MyTransforms.Add(_myTransform);
    }

    public void Remove()
    {
        if (_scriptableListTransform.MyTransforms.Contains(_myTransform))
            _scriptableListTransform.MyTransforms.Remove(_myTransform);
    }
}
