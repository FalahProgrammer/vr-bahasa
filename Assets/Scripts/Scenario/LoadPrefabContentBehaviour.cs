using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Leap.Unity.Infix;
using UnityEngine;
using UnityEngine.Events;

public class LoadPrefabContentBehaviour : MonoBehaviour
{
    [SerializeField] private bool _loadAtStart;
    
    [SerializeField] private RepositoryContentArea _repositoryContentArea;

    [SerializeField] private UnityEvent OnFinishedLoadPrefabs;
    private void Start()
    {
        if (_loadAtStart)
            StartCoroutine(WaitCoroutine(0.1f, GetPrefabContent));
    }

    public void GetPrefabContent()
    {
        StartCoroutine(LoadPrefabContent(FinishedLoadPrefabs));
    }
    
    private IEnumerator WaitCoroutine(float duration, Action then)
    {
        yield return new WaitForSeconds(duration);
        then?.Invoke();
    }
    IEnumerator LoadPrefabContent(Action FinishedLoadPrefabs)
    {
        for (int i = 0; i < _repositoryContentArea.Items.Count; i++)
        {
            if(!Directory.Exists("Assets/Resources/" + _repositoryContentArea.Items[i].path_area_prefab))
            {
                Directory.CreateDirectory("Assets/Resources/" + _repositoryContentArea.Items[i].path_area_prefab);
                
                Debug.Log("Directory doesn't exist!!, creating folder..... \n Directory Location : " + "Assets/Resources/" + _repositoryContentArea.Items[i].path_area_prefab.GetDirectoryName());
                
            }
            else
            {
                if (File.Exists("Assets/Resources/" + _repositoryContentArea.Items[i].path_area_prefab+"/"+_repositoryContentArea.Items[i].area_name+".prefab"))
                {

                    if (_repositoryContentArea.Items[i].AreaPrefab == null)
                    {
                        _repositoryContentArea.Items[i].AreaPrefab = Resources.Load<GameObject>(_repositoryContentArea.Items[i].path_area_prefab+"/"+_repositoryContentArea.Items[i].area_name);
            
                        Debug.Log("Data Found !! \n Data Name :" + _repositoryContentArea.Items[i].AreaPrefab + "\n Data attached from iteration : " + i);
                    }
                    
                }
                else
                {
                    //Debug.Log("Data not found \n Data name : "+ _repositoryContentArea.Items[i].area_name + "\n Data Location : " + "Assets/Resources/" + _repositoryContentArea.Items[i].path_area_prefab.GetDirectoryName());
                }
               
                   
                
            }
            
        }

        yield return new WaitForSeconds(1f);
        
        FinishedLoadPrefabs.Invoke();
    }

    void FinishedLoadPrefabs()
    {
        OnFinishedLoadPrefabs.Invoke();
    }
}
