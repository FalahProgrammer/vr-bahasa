using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data Scene", menuName = "Data Scene/Data Scene Management")]
public class DataSceneManagement : ScriptableObject
{
    public RepositoryLocation RepositoryLocation;

    private void Awake()
    {
        RepositoryLocation = Resources.Load<RepositoryLocation>("ScriptableObjects/Repository/Repository Location");
    }
}
