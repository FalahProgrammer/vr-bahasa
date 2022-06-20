using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GenerateEnvirontmentBehaviour : MonoBehaviour
{
    [SerializeField] private ScriptableTransform _scriptableTransform;
    
    [SerializeField] private ScriptableGameObjectDataController _scriptableGameObjectDataController;
    
    [SerializeField] private Transform _groupButtonEnvi;
    
    public UnityEvent OnFinishLoadAsset;
    
    private void Awake()
    {
        _scriptableTransform = Resources.Load<ScriptableTransform>("ScriptableObjects/Transform/Prefab Area");
        
        _scriptableGameObjectDataController = Resources.Load<ScriptableGameObjectDataController>("ScriptableObjects/Game Object/Scriptable GameObject Data Controller");
    }
    
    private void Init()
    {
        foreach(Transform child in _groupButtonEnvi)
        {
            Destroy(child.gameObject);
        }
    }
    
    public void GenerateAreaPrefab()
    {
        StartCoroutine(CoroutineGeneratePrefab(OnFinishedLoadAsset));

    }

    IEnumerator CoroutineGeneratePrefab(Action onFinishedLoadAsset)
    {
        if (_scriptableTransform.MyTransform != null)
        {
            Init();
            
            _scriptableGameObjectDataController.ContentButton = Instantiate(_scriptableTransform.MyTransform.gameObject, _groupButtonEnvi, true);

            /*_scriptableGameObjectDataController.ContentButton.transform.localScale = new Vector3(1, 1, 1);
            
            var localPosition = _scriptableGameObjectDataController.ContentButton.transform.localPosition;
            
            localPosition = new Vector3(localPosition.x, localPosition.y, 0);
            
            _scriptableGameObjectDataController.ContentButton.transform.localPosition = localPosition;
            
            _scriptableGameObjectDataController.ContentButton.transform.localRotation = new Quaternion(0 , 0 , 0 , 0);*/

            yield return _scriptableTransform.MyTransform.gameObject;
            
            Debug.Log("Prefab ''"+ _scriptableTransform.MyTransform.gameObject.name + "'' successfully loaded !!!");
            
            onFinishedLoadAsset?.Invoke();
        }
        else
        {
            Debug.Log("Prefab is failed to load !!!");
        }
    }
    
    private void OnFinishedLoadAsset()
    {
        //_commandSequenceManager.CallContinueCommand();
        //_scenarioEventBehaviour.ScenarioSubmiter();
        
        OnFinishLoadAsset.Invoke();
    }
}
