using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearRepoOnAppQuit : MonoBehaviour
{
    [SerializeField] private RepositoryLoginData _repositoryLoginData;
    [SerializeField] private RepositoryContentArea _repositoryContentArea;
    
    private void OnDisable()
    {
        _repositoryLoginData.token = "";
        _repositoryLoginData.Header.Clear();
        Debug.Log("Repo Login Data Cleared!");
       
        _repositoryContentArea.Items.Clear();
        Debug.Log("Repo Content Area Cleared!");
    }
}
