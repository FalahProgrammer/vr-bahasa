using System;
using System.Collections;
using System.Collections.Generic;

using System.IO;
using System.Linq;
using Leap.Unity.Infix;
using UnityEngine;
using UnityEngine.Events;

public class LoadPrefabContentBehaviour : MonoBehaviour
{
    [SerializeField] private bool debugMode;
    [Space(10)] 
    [SerializeField] private bool _loadAtStart;
    
    [SerializeField] private RepositoryContentArea _repositoryContentArea;
    [SerializeField] private IntegerVariable _integerVariable;

    [SerializeField] private UnityEvent OnFinishedLoadPrefabs;

    [SerializeField] private GameObject[] areaPrefabsArray;
    /*private void Start()
    {
        if (_loadAtStart)
            StartCoroutine(WaitCoroutine(0.1f, GetPrefabContent));
    }*/

    public void GetPrefabContent()
    {
        Debug.Log("Load prefab content!");
        StartCoroutine(LoadPrefabContent(FinishedLoadPrefabs));
        
        /*for (int i = 0; i < _repositoryContentArea.Items.Count; i++)
        {
            tempPrefab = Resources.Load<GameObject>(_repositoryContentArea.Items[i].path_area_prefab + "/" +
                                                    _repositoryContentArea.Items[i].area_name);
            
            _repositoryContentArea.Items[i].AreaPrefab = tempPrefab;
        }
        
        Debug.Log("Finished Loading File");
        
        FinishedLoadPrefabs();*/
    }
    
    private IEnumerator WaitCoroutine(float duration, Action then)
    {
        yield return new WaitForSeconds(duration);
        then?.Invoke();
    }

    public GameObject tempPrefab;
    
    private bool dataLoaded;
    
    IEnumerator LoadPrefabContent(Action FinishedLoadPrefabs)
    {
        dataLoaded = false;
        if (debugMode) Debug.Log("Start Loading File");

        for (int i = 0; i < _repositoryContentArea.Items.Count; i++)
        {
            #if UNITY_EDITOR
            if(!Directory.Exists("Assets/Resources/" + _repositoryContentArea.Items[i].path_area_prefab/* + "/" + _repositoryContentArea.Items[i].npc[p].npc_name*/))
            {
                //Debug.Log("Directory doesn't exist!!, creating folder..... \n Directory Location : " + "Assets/Resources/" + _repositoryContentArea.Items[i].path_area_prefab + "/" + _repositoryContentArea.Items[i].npc_name.GetDirectoryName());
                Directory.CreateDirectory("Assets/Resources/" + _repositoryContentArea.Items[i].path_area_prefab/* + "/" + _repositoryContentArea.Items[i].npc[p].npc_name*/);
            }
            /*for (int p = 0; p < _repositoryContentArea.Items[i].npc.Length; p++)
            {
                
            }*/
            #endif
            
            
            tempPrefab = Resources.Load<GameObject>(_repositoryContentArea.Items[i].path_area_prefab + "/" + _repositoryContentArea.Items[i].area_name);
            
            _repositoryContentArea.Items[i].AreaPrefab = tempPrefab;

            if (tempPrefab != null)
            {
                if (debugMode)
                {
                    Debug.LogWarning("Data Loaded: " + _repositoryContentArea.Items[i].path_area_prefab + "/" + _repositoryContentArea.Items[i].area_name);
                }
                dataLoaded = true;
                yield return new WaitForSeconds(0);
            }
        }
        
        //yield return new WaitForSeconds(0);

        if (debugMode && !dataLoaded)
        {
            Debug.LogError("Data Not Loaded Properly");
        }
        
        //yield return new WaitForSeconds(1f);

        if (debugMode)
        {
            Debug.Log("Finished Loading File");
        }

        FinishedLoadPrefabs.Invoke();

        #region Obsolete
        /*for (int i = 0; i < _repositoryContentArea.Items.Count; i++)
        {
            // if directory doesn't exist
            if(!Directory.Exists("Assets/Resources/" + _repositoryContentArea.Items[i].path_area_prefab))
            {
                Directory.CreateDirectory("Assets/Resources/" + _repositoryContentArea.Items[i].path_area_prefab);
                
                Debug.Log("Directory doesn't exist!!, creating folder..... \n Directory Location : " + "Assets/Resources/" + _repositoryContentArea.Items[i].path_area_prefab.GetDirectoryName());
                
                /*if (_repositoryContentArea.Items[i].AreaPrefab == null)
                {
                    _repositoryContentArea.Items[i].AreaPrefab = Resources.Load<GameObject>(_repositoryContentArea.Items[i].path_area_prefab+"/"+_repositoryContentArea.Items[i].area_name);
            
                    Debug.Log("Data Found !! \n Data Name :" + _repositoryContentArea.Items[i].AreaPrefab + "\n Data attached from iteration : " + i);
                }#1#
            }

            // if directory exist
            /*else
            {
                Debug.Log("File Search: " +"Assets/Resources/" + _repositoryContentArea.Items[i].path_area_prefab+"/"+_repositoryContentArea.Items[i].area_name+".prefab");
                
                if (File.Exists("Assets/Resources/" + _repositoryContentArea.Items[i].path_area_prefab+"/"+_repositoryContentArea.Items[i].area_name+".prefab"))
                {
                    Debug.Log("Folder Exist");
                    
                    if (_repositoryContentArea.Items[i].AreaPrefab == null)
                    {
                        Debug.Log(Resources.Load<GameObject>(_repositoryContentArea.Items[i].path_area_prefab+"/"+_repositoryContentArea.Items[i].area_name));

                        if (Resources.Load<GameObject>(_repositoryContentArea.Items[i].path_area_prefab + "/" +
                                                       _repositoryContentArea.Items[i].area_name) != null)
                        {
                            _repositoryContentArea.Items[i].AreaPrefab = Resources.Load<GameObject>(_repositoryContentArea.Items[i].path_area_prefab+"/"+_repositoryContentArea.Items[i].area_name);
                        }
            
                        Debug.Log("Data Found !! \n Data Name :" + _repositoryContentArea.Items[i].AreaPrefab + "\n Data attached from iteration : " + i);
                    }
                }
                else
                {
                    Debug.Log("Folder Doesn't Exist");
                }
                
                /*if (_repositoryContentArea.Items[i].AreaPrefab == null)
                {
                    _repositoryContentArea.Items[i].AreaPrefab = Resources.Load<GameObject>(_repositoryContentArea.Items[i].path_area_prefab+"/"+_repositoryContentArea.Items[i].area_name);
            
                    Debug.Log("Data Found !! \n Data Name :" + _repositoryContentArea.Items[i].AreaPrefab + "\n Data attached from iteration : " + i);
                }#2#
            }#1#
        }*/
        
        /*LoadFile();

        //yield return new WaitForSeconds(1f);
        
        yield return new WaitUntil(() => dataLoaded);
        Debug.Log("Finished Loading File");
        
        FinishedLoadPrefabs.Invoke();*/
        
        #endregion
    }

    void FinishedLoadPrefabs()
    {
        Debug.Log("Data Validated");
        OnFinishedLoadPrefabs.Invoke();
    }
}
